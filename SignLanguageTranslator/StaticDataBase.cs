using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignLanguageTranslator
{
    public class StaticDataBase
    {
        public static string pathToFolderWithXmls = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\baseData";
        public static int resizeXInPixels = 200;
        public static int resizeYInPixels = 250;

        public static string pathToSelectedFoto;
        private static double bestMatchProcent = 0.0000001;
        private static string nameOfBestMatch = "";
        public static double maxZoom = 0.1; 
        public static int maxReach = 1;
        public static double howMuchZoomPerLoop = 0.5;
        public static int howMuchReachForLoop = 1;
        public static bool isCameraWorking = false;

        //filters
        public static int Blue_threshold = 140;
        public static int Green_threshold = 130;
        public static int Red_threshold = 105;
        public static int maxBlueThreshold = 175;
        public static int maxGreenThreshold = 160;
        public static int maxRedThreshold = 140;

        public static int grayThreshold = 20;
        public static int maxGrayThreshold = 255;

        //Mouse Position 
        public static int mouseStartX = 0;
        public static int mouseStartY = 0;

        public static int mouseStopX = 2000;
        public static int mouseStopY = 2500;

        private static Image<Bgr, Byte> pictureFromCamera = new Image<Bgr, byte>(100,100);

        public static bool galleryModeOn = false;

        public static string signToLearn = "x";

        public string NameOfBestMatch
        {
            get
            {
                return nameOfBestMatch;
            }

            set
            {
                nameOfBestMatch = value;
                OnDataChanged();
            }
        }

        public static double BestMatchProcent
        {
            get
            {
                return bestMatchProcent;
            }

            set
            {
                bestMatchProcent = value;
            }
        }
        
        public static Image<Bgr, byte> PictureFromCamera
        {
            get
            {
                return pictureFromCamera;
            }

            set
            {
                pictureFromCamera = value;
            }
        }
    
        public delegate void DataChangedEventHandler(object source, EventArgs args);
        public static event DataChangedEventHandler DataChanged;

        protected virtual void OnDataChanged()
        {
            if (DataChanged != null)
                DataChanged(this, EventArgs.Empty);
        }
    }
}
