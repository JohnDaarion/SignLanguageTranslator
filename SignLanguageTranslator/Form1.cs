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



            int Blue_threshold = 100; 
            int Green_threshold = 0; 
            int Red_threshold = 100;
            byte[] pictureBitsInfo; 
            
            Image<Bgr, Byte> img_colour = new Image<Bgr, Byte>(openFileDialog.FileName);
            Image<Gray, Byte> img_grey = new Image<Gray, byte>(img_colour.Width,img_colour.Height,new Gray(255)); 
            loadedPictureBox.Image = img_colour;
            

            img_colour = img_colour.ThresholdBinary(new Bgr(Blue_threshold, Green_threshold, Red_threshold), new Bgr(255, 255, 255));

            CvInvoke.CvtColor(img_colour, img_grey, ColorConversion.Bgr2Gray);
            img_colour.Dispose();
            img_grey = img_grey.ThresholdBinary(new Gray(200), new Gray(255));

            //img_grey = ZoomGray(img_grey, 0.8);
            //double resizeWalue = 0.10;
            //img_grey = img_grey.Rotate(180, new Gray(0), false); rotation
            //resizeTest = img_grey.Resize(1+resizeWalue, Inter.Cubic);
            //resizeTest.ROI = new Rectangle((int)(resizeTest.Width*(resizeWalue*0.5)), (int)(resizeTest.Width * (resizeWalue * 1.5)), 50, 50);
            img_grey = img_grey.Resize(10, 13, Inter.Cubic);

            //resizeTest = ZoomGray(resizeTest, 1.2);
            //resizeTest.ROI = new Rectangle(0, 0, 50, 50);
            resultImageBox.Image = img_grey;
            pictureBitsInfo = img_grey.Bytes;

            SignToLetterClass sC = new SignToLetterClass(openFileDialog.FileName);
            sC.CheckItAll();
            //new Thread(refreshThread).Start();
           

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
            labelForProcents.Text = SignToLetterClass.bestMatchProcent.ToString();
            labelForrResult.Text = SignToLetterClass.nameOfBestMatch;
        }
    }
}
