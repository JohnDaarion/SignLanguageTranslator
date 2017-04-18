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
        public static string pathToFolderWithXmls = System.IO.Path.GetDirectoryName(Application.ExecutablePath)+"\\baseData";
        public static int resizeXInPixels = 200;
        public static int resizeYInPixels = 250;
        public static string pathToSelectedFoto;
        public static int lastPathTaken = 0;
        private static double bestMatchProcent = 0.0000001;
        private static string nameOfBestMatch = "";
        public static double maxRotation = 0.1;
        public static double maxZoom = 0.1; // range to 0-0.9 // jest to max zakres od 1
        public static int maxReach = 1;//(int)(resizeXInPixels * resizeYInPixels * 0.01);
        public static int amountOfThreads = 0;
        public static int howManyThreadDoIWant = 2; 
        public static double howManyRadiansPerLoop = 0.1;
        public static double howMuchZoomPerLoop = 0.5;
        public static int howMuchReachForLoop = 1;

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

        public static Image<Bgr, Byte> pictureFromCamera;

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

        public delegate void DataChangedEventHandler(object source, EventArgs args);
        public static event DataChangedEventHandler DataChanged;

        protected virtual void OnDataChanged()
        {
            if (DataChanged != null)
                DataChanged(this, EventArgs.Empty);
        }


    }
}
