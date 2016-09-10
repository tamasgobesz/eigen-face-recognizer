using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.Util;
using System.IO;
using FaceDetection.Core;

namespace FaceDetection
{
    public partial class MainWindow : Form
    {
        private readonly int faceTrainLimit = 2;
        private List<Image<Gray, Byte>> trainingImages;
        private List<Image<Gray, Single>> eigenImages;
        private List<string> labels;
        private Capture capture;
        private HaarCascade haar;
        private bool faceDetect;
        private bool faceTrain;
        private int faceCount;

        private IRecognizer rcg;

        private string currentDir;
        private string path;
        private string currentName;
        private string currentLabel = "";

        private MCvFont font;
        private int eigenIndex;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            faceDetect = false;
            faceTrain = false;

            currentDir = Directory.GetCurrentDirectory();

            try
            {
                capture = new Capture();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not access webcam:" + Environment.NewLine + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                Application.Exit();
            }

            try
            {
                haar = new HaarCascade(currentDir + @"\resources\haarcascade_frontalface_alt_tree.xml");
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(
                    ex.Message + Environment.NewLine + "haarcascade_frontalface_alt_tree.xml",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
                Application.Exit();
            }

            eigenImages = new List<Image<Gray, float>>();
            trainingImages = new List<Image<Gray, Byte>>();

            labels = new List<string>();
            font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_COMPLEX, 1.0, 1.0);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            using (Image<Bgr, Byte> frame = capture.QueryFrame())
            {
                if (frame != null)
                {
                    String lb;
                    if (faceDetect)
                    {
                        Image<Gray, Byte> grayFrame = frame.Convert<Gray, Byte>();

                        var faces = grayFrame.DetectHaarCascade(haar, 1.2, 3, HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                            new Size(frame.Width / 8, frame.Height / 8))[0];

                        foreach (var face in faces)
                        {
                            frame.Draw(face.rect, new Bgr(255, 0, 0), 3);

                            Image<Gray, Byte> img = grayFrame.GetSubRect(face.rect);
                            img = img.Resize(200, 200, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);

                            if (faceTrain)
                            {
                                if (faceCount < faceTrainLimit)
                                {
                                    string fileName = Path.GetRandomFileName() + ".png";
                                    string newPath = Path.Combine(path, fileName);

                                    trainingImages.Add(img);
                                    labels.Add(currentName);
                                    img.Save(newPath);
                                    faceCount++;
                                }
                                else
                                {
                                    refreshTrainingImages();
                                    faceTrain = false;
                                    faceDetect = false;
                                    btTrain.Text = "Start Training";
                                }
                            }
                            else
                            {
                                faceCount = 0;
                                lb = "";
                                if (rcg != null)
                                {
                                    lb = rcg.Recognize(img);
                                }
                                frame.Draw(lb, ref font, new Point(face.rect.X, face.rect.Y), new Bgr(0, 0, 255));
                            }
                        }
                    }
                    pictureBox.Image = frame.ToBitmap();
                }
            }
        }

        private void btFaceDetect_Click(object sender, EventArgs e)
        {
            if (faceDetect)
            {
                faceDetect = false;
                btFaceDetect.Text = "Detect Faces";
            }
            else
            {
                faceDetect = true;
                btFaceDetect.Text = "Stop Detection";
            }
        }

        private void btTrain_Click(object sender, EventArgs e)
        {
            if (faceTrain)
            {
                faceTrain = false;
                faceDetect = false;
                btTrain.Text = "Start Training";
                btFaceDetect.Text = "Detect Faces";
                refreshTrainingImages();
            }
            else
            {
                faceCount = 0;
                TrainDialog dlg = new TrainDialog(this);
                dlg.ShowDialog();
            }
        }

        public void createNewSubject(String subjectName)
        {
            if (faceTrain)
            {
                faceTrain = false;
                faceDetect = false;
                btTrain.Text = "Start Training";
                btFaceDetect.Text = "Detect Faces";

            }
            else
            {
                faceTrain = true;
                faceDetect = true;
                btTrain.Text = "Stop Training...";
                this.currentName = subjectName;
                path = Path.Combine(currentDir + @"\resources\database\", subjectName);
                Directory.CreateDirectory(path);
            }
        }

        private void refreshTrainingImages()
        {
            rcg = new CustomEigenRecognizer(trainingImages, labels);

            Image<Gray, Single> img = (Image<Gray, Single>)rcg.GetAverageImage();

            lbAvgImg.Visible = true;
            lbEigenFaces.Visible = true;
            btNext.Visible = true;
            btPrev.Visible = true;

            eigenIndex = 0;
            IList<IImage> tempList = rcg.GetEigenImages();

            eigenImages.Clear();

            foreach (IImage imgTemp in tempList)
            {
                eigenImages.Add((Image<Gray, Single>)imgTemp);
            }

            eigenFacesBox.Image = eigenImages.First().Resize(140, 140, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).ToBitmap();
            avgImg.Image = img.Resize(140, 140, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).ToBitmap();
        }

        private void loadImageDatabase()
        {
            processFolders(currentDir + @"\resources\database\", 0);
        }

        private void processFolders(string src, int lvl)
        {
            if (lvl <= 2)
            {
                string[] files = Directory.GetFiles(src);
                foreach (string fname in files)
                {
                    Image<Gray, Byte> temp = new Image<Gray, byte>(fname);
                    trainingImages.Add(temp);
                    labels.Add(currentLabel);
                }

                string[] subDirs = Directory.GetDirectories(src);
                foreach (string sub in subDirs)
                {
                    if ((File.GetAttributes(sub) & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
                    {
                        int index = sub.LastIndexOf("\\");
                        currentLabel = sub.Substring(index + 1);
                        processFolders(sub, lvl + 1);
                    }
                }
            }
        }

        private void tbLoad_Click(object sender, EventArgs e)
        {
            loadImageDatabase();
            refreshTrainingImages();
        }

        private void btPrev_Click(object sender, EventArgs e)
        {
            if (eigenIndex > 0)
            {
                eigenFacesBox.Image = eigenImages[--eigenIndex].Resize(140, 140, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).ToBitmap();
            }
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            if (eigenIndex < eigenImages.Count - 1)
            {
                eigenFacesBox.Image = eigenImages[++eigenIndex].Resize(140, 140, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).ToBitmap();
            }
        }
    }
}