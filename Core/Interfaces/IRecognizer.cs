using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace FaceDetection.Core
{
    public interface IRecognizer
    {
        string Recognize(Image<Gray, byte> img);
        IImage GetAverageImage();
        IList<IImage> GetEigenImages();
    }
}
