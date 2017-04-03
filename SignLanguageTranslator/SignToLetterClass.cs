using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SignLanguageTranslator
{
    public class SignToLetterClass
    {

        public SignToLetterClass(string selectedFotoPath)
        {
            pathToSelectedFoto = selectedFotoPath;
        }

        public static string pathToFolderWithXmls = "D:\\dokumenty\\Visual Studio 2015\\Projects\\SignLanguageTranslator\\SignLanguageTranslator\\bin\\x64\\Debug\\baseData";
        public string[] basePathsToXlms = Directory.GetFileSystemEntries(pathToFolderWithXmls);
        public static int resizeXInPixels = 10;
        public static int resizeYInPixels = 13;
        public static string pathToSelectedFoto;

        public static int lastPathTaken = 0;
        public static double bestMatchProcent = 0;
        public static string nameOfBestMatch = "";
        public static int maxRotation = 50;
        public static double maxZoom = 0.4; // range to 0-0.9 // jest to max zakres od 1
        public static int maxReach = 32;//(int)(resizeXInPixels * resizeYInPixels * 0.01);
        public static int amountOfThreads = 0;
        public static int howManyThreadDoIWant = 4;
        //public event MyEventClass Testy;


        public List<CreateDataBase.AdvantedList> gettingDataFromXml(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<CreateDataBase.AdvantedList>));
            List<CreateDataBase.AdvantedList> buffAL;

            FileStream fs = new FileStream(path, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);

            buffAL = (List<CreateDataBase.AdvantedList>)serializer.Deserialize(reader);
            reader.Dispose();
            fs.Close();
            return buffAL;
        }

        Image<Bgr, Byte> LoadImage(string pathTaken)
        {
            return new Image<Bgr, Byte>(pathTaken);
        }

        Image<Gray, Byte> LoadImageGray(string pathTaken)
        {
            return new Image<Gray, Byte>(pathTaken);
        }

        public void CheckItAll()
        {
            bestMatchProcent = 0;
            nameOfBestMatch = "";

            while (basePathsToXlms.Length > lastPathTaken)
            {
                if (amountOfThreads <= howManyThreadDoIWant)
                {
                    new Thread(CheckPictures).Start();
                    amountOfThreads++;
                }
            }
        }

        public void CheckPictures()
        {
            string myPath = "";
            if (lastPathTaken < basePathsToXlms.Length)
            {
                myPath = basePathsToXlms[lastPathTaken++];
            }

            if (myPath != "")
            {
                lastPathTaken++;
                List<CreateDataBase.AdvantedList> buffAdvantedList;
                buffAdvantedList = gettingDataFromXml(myPath);
                Image<Bgr, Byte> buffImgBgr = LoadImage(pathToSelectedFoto);
                byte[] buffForArray;
                Image<Gray, Byte> buffImgGray;
                byte[] buffForXmlArray;
                buffImgGray = UseFilters(buffImgBgr);
                buffImgGray = buffImgGray.Resize(resizeXInPixels, resizeYInPixels, Inter.Cubic);
                buffImgBgr.Dispose();
                Image<Gray, Byte> buffImgGrayInside = new Image<Gray, byte>(buffImgGray.Width, buffImgGray.Height, new Gray(255));
                Image<Gray, Byte> buffImgGrayRotated = new Image<Gray, byte>(buffImgGray.Width, buffImgGray.Height, new Gray(255));
                Image<Gray, Byte> buffImgGrayZoomed = new Image<Gray, byte>(buffImgGray.Width, buffImgGray.Height, new Gray(255));

                for (int indexTypes = 0; indexTypes < buffAdvantedList.Count; indexTypes++)
                {
                    buffImgGrayInside = buffImgGray;
                    buffForXmlArray = buffAdvantedList[indexTypes].allTypesOfSign;

                    for (int indexRotation = -maxRotation; indexRotation <= maxRotation; indexRotation++)
                    {

                        buffImgGrayRotated = RotateGray(buffImgGray, indexRotation);

                        for (double indexZoom = (1 - maxZoom); indexZoom <= (1 + maxZoom); indexZoom += 0.02)
                        {
                            buffImgGrayZoomed = buffImgGrayRotated;

                            buffImgGrayZoomed = ZoomGray(buffImgGrayZoomed, indexZoom);

                            //buffImgGrayInside = buffImgGrayInside.Resize(resizeXInPixels, resizeYInPixels, Inter.Cubic);

                            buffForArray = makBinaryFromByte(buffImgGrayInside.Bytes);

                            arraysOfPrabability(buffForArray, buffForXmlArray, maxReach, myPath);
                            
                            
                        }
                    }
                }
                buffImgBgr.Dispose();
                
            }
            amountOfThreads--;
            
        }

        /* Nieużywana
         Image<Bgr, Byte> RotateBgr(Image<Bgr, Byte> img, double angle)
        {
            
            img = img.Rotate(angle, new Bgr(255, 255, 255), false);

            return img;
        }*/

        Image<Gray, Byte> RotateGray(Image<Gray, Byte> img, double angle)
        {
            img = img.Rotate(angle, new Gray(255), false);
            return img;
        }



        Image<Gray, Byte> ZoomGray(Image<Gray, Byte> img, double zoom)
        {
            int sizeXatStart = 0;
            sizeXatStart = img.Width;

            int sizeYatStart = 0;
            sizeYatStart = img.Height;


            Image<Gray, Byte> buffImage = new Image<Gray, byte>(sizeXatStart, sizeYatStart, new Gray(255));
            Image<Gray, Byte> buffImage2 = new Image<Gray, byte>(sizeXatStart, sizeYatStart, new Gray(255));

            buffImage = img;
            buffImage2 = img;

            if (zoom >= 1)
            {
                int resizeX = (int)(sizeXatStart * zoom);
                int resizeY = (int)(sizeYatStart * zoom);
                buffImage = buffImage.Resize(resizeX, resizeY, Inter.Cubic);
                buffImage.ROI = new System.Drawing.Rectangle(((int)((resizeX - img.Width) / 2)), ((int)((resizeY - img.Height) / 2)), img.Width, img.Height);
                img.CopyTo(buffImage);
                buffImage.ROI = new System.Drawing.Rectangle(0, 0, resizeX, resizeY);
                buffImage = buffImage.Resize(img.Width, img.Height, Inter.Cubic);
            }
            if (zoom < 1)
            {
                int resizeX = (int)(sizeXatStart * zoom);
                int resizeY = (int)(sizeYatStart * zoom);
                buffImage = buffImage.Resize(resizeX, resizeY, Inter.Cubic);
                buffImage2.ROI = new System.Drawing.Rectangle(((int)((img.Width - resizeX) / 2)), ((int)((img.Height - resizeY)) / 2), resizeX, resizeY);
                buffImage2.CopyTo(buffImage);
                img.ROI = new Rectangle(0, 0, sizeXatStart, sizeYatStart);
                buffImage = buffImage.Resize(img.Width, img.Height, Inter.Cubic);
            }
            
            return buffImage;
        }
         /* dopisać to
        Image<Gray, Byte> ZoomGrayXY(Image<Gray, Byte> img, double zoomX ,double zoomY)
        {
            int sizeXatStart = 0;
            sizeXatStart = img.Width;

            int sizeYatStart = 0;
            sizeYatStart = img.Height;


            Image<Gray, Byte> buffImage = new Image<Gray, byte>(sizeXatStart, sizeYatStart, new Gray(255));
            Image<Gray, Byte> buffImage2 = new Image<Gray, byte>(sizeXatStart, sizeYatStart, new Gray(255));

            buffImage = img;
            buffImage2 = img;
            int resizeX = (int)(sizeXatStart * zoomX);
            int resizeY = (int)(sizeYatStart * zoomY);
            buffImage = buffImage.Resize(resizeX, resizeY, Inter.Cubic);

            if (zoomX >= 1 && zoomY >= 1)
            {
                buffImage.ROI = new System.Drawing.Rectangle(((int)(Math.Abs(resizeX - img.Width) / 2)), ((int)(Math.Abs(resizeY - img.Height) / 2)), img.Width, img.Height);
                img.CopyTo(buffImage);
                buffImage.ROI = new System.Drawing.Rectangle(0, 0, resizeX, resizeY);
            }
            if (zoomX < 1 && zoomY < 1)
            {
                buffImage2.ROI = new System.Drawing.Rectangle(((int)(Math.Abs(resizeX - img.Width) / 2)), ((int)(Math.Abs(resizeY - img.Height) / 2)), resizeX, resizeY);
                buffImage2.CopyTo(buffImage);
                img.ROI = new Rectangle(0, 0, sizeXatStart, sizeYatStart); 
            }
            if (zoomX >= 1 && zoomY < 1)
            {
                buffImage2.ROI = new System.Drawing.Rectangle(((int)(Math.Abs(resizeX - img.Width) / 2)), ((int)(Math.Abs(resizeY - img.Height) / 2)), img.Width, resizeY);
                buffImage2.CopyTo(buffImage);
                img.ROI = new Rectangle(0, 0, sizeXatStart, sizeYatStart);
            }
            buffImage = buffImage.Resize(img.Width, img.Height, Inter.Cubic);
            return buffImage;
        }*/


        /* nieużywana
         Image<Bgr, Byte> ZoomBgr(Image<Bgr, Byte> img, double zoom)
        {
            int sizeXatStart = new int();
            sizeXatStart = img.Width;

            int sizeYatStart = new int();
            sizeYatStart = img.Height;


            Image<Bgr, Byte> buffImage;
            buffImage = img;
            

            if (zoom >= 1)
            {
                int resizeX = (int)(sizeXatStart * zoom);
                int resizeY = (int)(sizeYatStart * zoom);
                buffImage.ROI = new System.Drawing.Rectangle(((int)((resizeX - img.Width) / 2)), ((int)((resizeY - img.Height) / 2)), img.Width, img.Height);
                img.CopyTo(buffImage);
                buffImage = buffImage.Resize(sizeXatStart, sizeYatStart, Inter.Cubic);
            }
            if (zoom < 1)
            {
                int resizeX = (int)(sizeXatStart * (2 - zoom));
                int resizeY = (int)(sizeYatStart * (2 - zoom));
                buffImage = buffImage.Resize(resizeX, resizeY, Inter.Cubic);
                buffImage.ROI = new System.Drawing.Rectangle(((int)(resizeX - img.Width) / 2), ((int)(resizeY - img.Height) / 2), img.Width, img.Height);
                img.CopyTo(buffImage);
                buffImage.ROI = new Rectangle(0, 0, resizeX, resizeY);
                buffImage = buffImage.Resize(sizeXatStart, sizeYatStart, Inter.Cubic);
            }
            img.Dispose();
            return buffImage;
        }*/

        void arraysOfPrabability(byte[] firstDoubleArray, byte[] secondDoubleArray, int reach, string myPath)
        {
            //wstawka że jeśli firsDobleArray.Lenght = 0 to wywala błąd

            for (int indexOutside = -reach; indexOutside <= reach; indexOutside++)
            {
                double allProbabilitiesSummed = 0;
                int howManyHandBytes = 0;

                for (int index = 0; index < firstDoubleArray.Length; index++)
                {

                    if ((index + indexOutside) >= 0 && (index + indexOutside) < firstDoubleArray.Length)
                    {

                        if (firstDoubleArray[index] <= secondDoubleArray[index + indexOutside] && secondDoubleArray[index + indexOutside] != 0)
                        {
                            allProbabilitiesSummed += (firstDoubleArray[index] / secondDoubleArray[index + indexOutside]);
                            howManyHandBytes++;
                        }
                        if (firstDoubleArray[index] > secondDoubleArray[index + indexOutside] && firstDoubleArray[index] != 0)
                        {
                            allProbabilitiesSummed += (secondDoubleArray[index + indexOutside] / firstDoubleArray[index]);
                            howManyHandBytes++;
                        }
                    }
                }
                if ((allProbabilitiesSummed/howManyHandBytes) > bestMatchProcent)
                {
                    lock (this)
                    {
                        bestMatchProcent = allProbabilitiesSummed / howManyHandBytes;
                        nameOfBestMatch = myPath[myPath.Length - 5].ToString();
                    }
                }
            }
        }




        Image<Gray, Byte> UseFilters(Image<Bgr, Byte> _img)
        {
            int Blue_threshold = 100;
            int Green_threshold = 0;
            int Red_threshold = 100;
            
            Image<Gray, Byte> imgGray = new Image<Gray, byte>(_img.Width, _img.Height, new Gray(255));
            
           

                _img = _img.ThresholdBinary(new Bgr(Blue_threshold, Green_threshold, Red_threshold), new Bgr(255, 255, 255));
                CvInvoke.CvtColor(_img, imgGray, ColorConversion.Bgr2Gray);
                imgGray = imgGray.ThresholdBinary(new Gray(200), new Gray(255));
                //imgGray = imgGray.Resize(resizeXInPixels, resizeYInPixels, Inter.Cubic);

            _img.Dispose();
            
            return imgGray;
        }

        byte[] makBinaryFromByte(byte[] _buffBytes)
        {
            byte[] buffByte = new byte[_buffBytes.Length];

            for (int indexOfInternalLoop = 0; indexOfInternalLoop < _buffBytes.Length; indexOfInternalLoop++)
            {
                buffByte[indexOfInternalLoop] = (byte)(_buffBytes[indexOfInternalLoop] / 255);
            }
            return buffByte;
        }
    }
}
