using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignLanguageTranslator
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            StaticDataBase.DataChanged += OnDataChanged;
            Control.CheckForIllegalCrossThreadCalls = false;
            
        }


        private void openAndExeButton_Click(object sender, EventArgs e)
        {
            DialogResult originalFile;
            originalFile = openFileDialog.ShowDialog();

            if (originalFile != DialogResult.OK)
            {
                dialogLabel.Text = "no file";
                return;
            }

            SignToLetterClass sC = new SignToLetterClass(openFileDialog.FileName);

            /*Mat originalPicture;
           
            try
            {
                originalPicture = new Mat(openFileDialog.FileName, LoadImageType.Color);
                dialogLabel.Text = "All good";
            }
            catch (Exception ex)
            {
                dialogLabel.Text = "unable to open image, error: " + ex.Message;
                return;
            }

            if (originalPicture == null)
            {
                dialogLabel.Text = "unable to open image";
                return;
            }
            */
            //loadedPictureBox.Image = originalPicture;

            //Mat imgGrayscale = new Mat(originalPicture.Size, DepthType.Cv8U, 1);
            //CvInvoke.CvtColor(originalPicture, imgGrayscale, ColorConversion.Bgr2HsvFull);



            Image<Bgr, Byte> img_colour = new Image<Bgr, Byte>(openFileDialog.FileName);
            Image<Gray, Byte> img_grey = new Image<Gray, byte>(img_colour.Width, img_colour.Height, new Gray(255));
            loadedPictureBox.Image = img_colour;

            img_grey = sC.UseFilters(img_colour);


            //img_colour = img_colour.ThresholdBinary(new Bgr(Blue_threshold, Green_threshold, Red_threshold), new Bgr(0,255,255));
            //resultImageBox.Image = img_colour;
            //CvInvoke.CvtColor(img_colour, img_grey, ColorConversion.Bgr2Gray);
            //img_colour.Dispose();

            /*float[,] k =   { {-1, 0, 1},
                             {-2, 0, 2},
                             {-1, 0, 1}};
            ConvolutionKernelF kernel = new ConvolutionKernelF(k);

            img_grey.SmoothBlur(10, 10);*/


            //resultImageBox.Image = img_grey;//.Canny(new Gray(40).Intensity, new Gray(30).Intensity);//img_grey.Convolution(kernel);

            //img_grey = img_grey.ThresholdBinary(new Gray(50), new Gray(255));

            //img_grey = ZoomGray(img_grey, 0.8);
            //double resizeWalue = 0.10;
            //img_grey = img_grey.Rotate(180, new Gray(0), false); rotation
            //resizeTest = img_grey.Resize(1+resizeWalue, Inter.Cubic);
            //resizeTest.ROI = new Rectangle((int)(resizeTest.Width*(resizeWalue*0.5)), (int)(resizeTest.Width * (resizeWalue * 1.5)), 50, 50);
            //img_grey = img_grey.Resize(StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels, Inter.Cubic);
            img_grey = sC.DropZeros(img_grey);

            resultImageBox.Image = img_grey;

            //resizeTest = ZoomGray(resizeTest, 1.2);
            //resizeTest.ROI = new Rectangle(0, 0, 50, 50);
            //1resultImageBox.Image = img_grey;
            //pictureBitsInfo = img_grey.Bytes;


            sC.CheckItAllByte();




            //new Thread(refreshThread).Start();
            img_colour.Dispose();
            //public StaticDataBase sDB = new StaticDataBase();
            //StaticDataBase.DataChanged += OnDataChanged;


        }
        /*
        void refreshThread()
        {
            Thread.Sleep(1000);
            while (SignToLetterClass.amountOfThreads != 0)
            {
                labelForProcents.Text = SignToLetterClass.bestMatchProcent.ToString();
                labelForrResult.Text = SignToLetterClass.nameOfBestMatch;
                Thread.Sleep(1000);
            }
        }
        */


        private void openFolderButton_Click(object sender, EventArgs e)
        {
            dialogLabel.Text = "Started";
            FolderBrowserDialog fdb = new FolderBrowserDialog();
            if (fdb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

            }

            CreateDataBase createDataBase = new CreateDataBase(fdb.SelectedPath);
            createDataBase.UseClassMethods();
            dialogLabel.Text = "Done";


        }


        private void button1_Click(object sender, EventArgs e)
        {
            //SignToLetterClass sC = new SignToLetterClass(openFileDialog.FileName);
            StaticDataBase sDB = new StaticDataBase();


            labelForProcents.Text = StaticDataBase.BestMatchProcent.ToString();
            labelForrResult.Text = sDB.NameOfBestMatch;
            numericUpDown1.Value = StaticDataBase.Blue_threshold;
            numericUpDown2.Value = StaticDataBase.Green_threshold;
            numericUpDown3.Value = StaticDataBase.Red_threshold;
            numericUpDown4.Value = StaticDataBase.maxBlueThreshold;
            numericUpDown5.Value = StaticDataBase.maxGreenThreshold;
            numericUpDown6.Value = StaticDataBase.maxRedThreshold;

        }

        public void useCamera()
        {
            ImageViewer viewer = new ImageViewer();
            Capture capture = new Capture();
            int c = 0;
            int d = 0;
            

            for (int i = 0; i < 1000; i++)
            {

                CommonMethods Cm = new CommonMethods();
                SignToLetterClass sC = new SignToLetterClass();
                StaticDataBase sDB = new StaticDataBase();

                loadedPictureBox.Image = capture.QueryFrame();
                Image<Bgr,Byte> buffImage = capture.QueryFrame().ToImage<Bgr,Byte>();
                buffImage.ROI = new System.Drawing.Rectangle(StaticDataBase.mouseStartX, StaticDataBase.mouseStartY, Math.Abs(StaticDataBase.mouseStopX - StaticDataBase.mouseStartX), Math.Abs(StaticDataBase.mouseStopY - StaticDataBase.mouseStartY));
                
                
                    resultImageBox.Image = Cm.UseFilters(buffImage);
                    StaticDataBase.pictureFromCamera = buffImage;
                

                if (c == 10)
                {
                    lock (StaticDataBase.pictureFromCamera)
                    {
                        sC.CheckItAllCamera();
                        c = 0;
                    }
                    
                }
                c++;

                if (StaticDataBase.galleryModeOn == true)
                {
                    
                    buffImage.Save(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\cameraImg\\" + i.ToString() + ".jpg");
                }

                Thread.Sleep(100);

                if (d == 100)
                {

                    sDB.NameOfBestMatch = "";
                    StaticDataBase.BestMatchProcent = 0;
                    d = 0;

                }
                d++;

               

                buffImage.Dispose();
            }
            
        }

        private void CameraButton_Click(object sender, EventArgs e)
        {
            new Thread(useCamera).Start();

        }

        private void loadedPictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(
                new Pen(Color.Red, 2f),
                StaticDataBase.mouseStartX, StaticDataBase.mouseStartY, Math.Abs(StaticDataBase.mouseStopX-StaticDataBase.mouseStartX), Math.Abs(StaticDataBase.mouseStopY - StaticDataBase.mouseStartY));
        }

        private void loadedPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
           StaticDataBase.mouseStartX = e.X;
           StaticDataBase.mouseStartY = e.Y;

        }

        private void loadedPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            StaticDataBase.mouseStopX = e.X;
            StaticDataBase.mouseStopY = e.Y;
        }

        private void loadedPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            StaticDataBase.Blue_threshold = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            StaticDataBase.Green_threshold = (int)numericUpDown2.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            StaticDataBase.Red_threshold = (int)numericUpDown3.Value;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            StaticDataBase.maxBlueThreshold = (int)numericUpDown4.Value;
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            StaticDataBase.maxGreenThreshold = (int)numericUpDown5.Value;
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            StaticDataBase.maxRedThreshold = (int)numericUpDown6.Value;
        }

        private void galleryButton_Click(object sender, EventArgs e)
        {
            StaticDataBase.galleryModeOn = true;
        }

        public void OnDataChanged(object source, EventArgs args)
        {
            StaticDataBase sDB = new StaticDataBase();
            labelForProcents.Text = StaticDataBase.BestMatchProcent.ToString();
            labelForrResult.Text = sDB.NameOfBestMatch;
            numericUpDown1.Value = StaticDataBase.Blue_threshold;
            numericUpDown2.Value = StaticDataBase.Green_threshold;
            numericUpDown3.Value = StaticDataBase.Red_threshold;
            numericUpDown4.Value = StaticDataBase.maxBlueThreshold;
            numericUpDown5.Value = StaticDataBase.maxGreenThreshold;
            numericUpDown6.Value = StaticDataBase.maxRedThreshold;
        }
    }
}
