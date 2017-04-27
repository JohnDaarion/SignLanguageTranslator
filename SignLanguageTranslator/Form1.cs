using AForge.Neuro;
using AForge.Neuro.Learning;
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
            Image<Bgr, Byte> img_colour = new Image<Bgr, Byte>(openFileDialog.FileName);
            Image<Gray, Byte> img_grey = new Image<Gray, byte>(img_colour.Width, img_colour.Height, new Gray(255));
            loadedPictureBox.Image = img_colour;
            img_grey = sC.UseFilters(img_colour, StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels);
            img_grey = sC.DropZeros(img_grey);
            resultImageBox.Image = img_grey;
            sC.CheckItAllCamera();
            img_colour.Dispose();
        }

        private void openFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdb = new FolderBrowserDialog();

            if (fdb.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dialogLabel.Text = "Started";
            }

            CreateDataBase createDataBase = new CreateDataBase(fdb.SelectedPath);
            createDataBase.UseClassMethods();
            dialogLabel.Text = "Done";
        }

        private void useCamera()
        {
            if (!StaticDataBase.isCameraWorking)
            {
                StaticDataBase.isCameraWorking = true;
                CameraButton.Text = "Stop Camera";
            }
            else
            {
                StaticDataBase.isCameraWorking = false;
                CameraButton.Text = "Start Camera";
            }
            ImageViewer viewer = new ImageViewer();
            Capture capture = new Capture();
            double counter = 0;

            while(StaticDataBase.isCameraWorking)
            {

                CommonMethods Cm = new CommonMethods();
                SignToLetterClass sC = new SignToLetterClass();
                StaticDataBase sDB = new StaticDataBase();

                loadedPictureBox.Image = capture.QueryFrame();
                Image<Bgr,Byte> buffImage = capture.QueryFrame().ToImage<Bgr,Byte>();
                buffImage.ROI = new System.Drawing.Rectangle(StaticDataBase.mouseStartX, StaticDataBase.mouseStartY, Math.Abs(StaticDataBase.mouseStopX - StaticDataBase.mouseStartX), Math.Abs(StaticDataBase.mouseStopY - StaticDataBase.mouseStartY));
                
                resultImageBox.Image = (Cm.DropZeros(Cm.UseFilters(buffImage, StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels)));
                StaticDataBase.PictureFromCamera = buffImage;
                
                if ((double)counter%10==0)
                {
                    lock (StaticDataBase.PictureFromCamera)
                    {
                        sC.CheckItAllCamera();
                    }
                }

                if (StaticDataBase.galleryModeOn)
                { 
                    buffImage.Save(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\cameraImg\\" + counter.ToString() + ".jpg");
                }

                Thread.Sleep(50);

                if ((double)counter%10 == 0)
                {
                    sDB.NameOfBestMatch = "";
                    StaticDataBase.BestMatchProcent = 0;
                }
                counter++;
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

        private void OnDataChanged(object source, EventArgs args)
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

        private void NNButton_Click(object sender, EventArgs e)
        {
            NeuronNetwork NN = new NeuronNetwork();
            NN.RunItMethod();
        }
    }
}
