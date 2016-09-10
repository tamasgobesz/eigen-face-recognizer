using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace FaceDetection.Core
{
    public class CustomEigenRecognizer : IRecognizer
    {
        private List<Image<Gray, Single>> eigenImages;
        private List<Matrix<float>> eigenValues;

        private Image<Gray, Single> averageImage;
        private float eigenThreshold;

        private List<string> labels;
        private MCvTermCriteria termCrit;

        private IntPtr[] eigenInputPtrs;

        public CustomEigenRecognizer(
            List<Image<Gray, Byte>> trainingImages,
            List<string> labels)
        {
            eigenThreshold = 5000;

            int width = trainingImages.First().Width;
            int height = trainingImages.First().Height;

            eigenImages = new List<Image<Gray, Single>>();
            eigenValues = new List<Matrix<float>>();

            averageImage = new Image<Gray, float>(width, height);
            termCrit = new MCvTermCriteria(trainingImages.Count, 0.001);

            this.labels = labels;

            IntPtr[] trainingObjects = new IntPtr[termCrit.max_iter];
            IntPtr[] eigenObjects = new IntPtr[termCrit.max_iter];

            for (int i = 0; i < termCrit.max_iter; i++)
            {
                trainingObjects[i] = trainingImages[i].Ptr;

                eigenImages.Add(new Image<Gray, Single>(width, height));
                eigenObjects[i] = eigenImages[i].Ptr;
            }

            CvInvoke.cvCalcEigenObjects(trainingObjects, ref termCrit, eigenObjects, null, averageImage.Ptr);

            averageImage.SerializationCompressionRatio = 9;

            foreach (Image<Gray, Single> image in eigenImages)
            {
                image.SerializationCompressionRatio = 9;
            }

            eigenInputPtrs = new IntPtr[eigenImages.Count];

            for (int i = 0; i < eigenImages.Count; i++)
            {
                eigenInputPtrs[i] = eigenImages[i].Ptr;
            }

            for (int i = 0; i < trainingImages.Count; i++)
            {
                eigenValues.Add(new Matrix<float>(CvInvoke.cvEigenDecomposite(
                    trainingImages[i],
                    eigenInputPtrs,
                    averageImage.Ptr)));
            }
        }

        public string Recognize(Image<Emgu.CV.Structure.Gray, byte> img)
        {
            string label = "";

            float[] distances = new float[eigenValues.Count];

            Matrix<float> eigenValue = new Matrix<float>(CvInvoke.cvEigenDecomposite(
                img.Ptr,
                eigenInputPtrs,
                averageImage.Ptr));

            for (int i = 0; i < eigenValues.Count; i++)
            {
                distances[i] = (float)CvInvoke.cvNorm(
                    eigenValue.Ptr,
                    eigenValues[i].Ptr,
                    Emgu.CV.CvEnum.NORM_TYPE.CV_L2,
                    IntPtr.Zero);
            }

            float minDistance = distances[0];

            for (int i = 1; i < distances.Length; i++)
            {
                if (distances[i] < minDistance)
                {
                    label = labels[i];
                    minDistance = distances[i];
                }
            }

            return (minDistance < eigenThreshold) ? label : String.Empty;
        }

        public IImage GetAverageImage()
        {
            return averageImage;
        }

        public IList<IImage> GetEigenImages()
        {
            return eigenImages.ToList<IImage>();
        }
    }
}
