using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignLanguageTranslator
{
    public static class StaticDataBase
    {
        public static string pathToFolderWithXmls = "D:\\dokumenty\\Visual Studio 2015\\Projects\\SignLanguageTranslator\\SignLanguageTranslator\\bin\\x64\\Debug\\baseData";
        public static int resizeXInPixels = 100;
        public static int resizeYInPixels = 125;
        public static string pathToSelectedFoto;
        public static int lastPathTaken = 0;
        public static double bestMatchProcent = 0;
        public static string nameOfBestMatch = "";
        public static int maxRotation = 20;
        public static double maxZoom = 0.1; // range to 0-0.9 // jest to max zakres od 1
        public static int maxReach = 100;//(int)(resizeXInPixels * resizeYInPixels * 0.01);
        public static int amountOfThreads = 0;
        public static int howManyThreadDoIWant = 4;
        public static int howManyRadiansPerLoop = 10;
        public static double howMuchZoomPerLoop = 0.05;
        public static int howMuchReachForLoop = 1;
        

    }
}
