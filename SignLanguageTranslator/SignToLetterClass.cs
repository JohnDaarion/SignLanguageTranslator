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
    public class SignToLetterClass : CommonMethods
    {

        public SignToLetterClass(string selectedFotoPath)
        {
            StaticDataBase.pathToSelectedFoto = selectedFotoPath;
            
        }

        public SignToLetterClass()
        {}


        public string[] basePathsToXlms = Directory.GetFileSystemEntries(StaticDataBase.pathToFolderWithXmls);


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

        public CreateDataBase.BaseList gettingDataFromXmlDouble(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CreateDataBase.BaseList));
            CreateDataBase.BaseList buffAL;


            FileStream fs = new FileStream(path, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);

            buffAL = (CreateDataBase.BaseList)serializer.Deserialize(reader);
            reader.Dispose();
            fs.Close();
            return buffAL;
        }


        public void CheckItAll()
        {
            StaticDataBase sDB = new StaticDataBase();
            StaticDataBase.BestMatchProcent = 0;
            sDB.NameOfBestMatch = "";
            

            while (basePathsToXlms.Length > StaticDataBase.lastPathTaken)
            {
                if (StaticDataBase.amountOfThreads <= StaticDataBase.howManyThreadDoIWant)
                {
                    new Thread(CheckPictures).Start();
                    StaticDataBase.amountOfThreads++;
                }
                if (StaticDataBase.amountOfThreads == StaticDataBase.howManyThreadDoIWant)
                {
                    Thread.Sleep(1000);
                }
            }
            StaticDataBase.lastPathTaken = 0;
            
        }

        public void CheckItAllCamera()
        {
            StaticDataBase sDB = new StaticDataBase();
            StaticDataBase.BestMatchProcent = 0;
            sDB.NameOfBestMatch = "";

            while (basePathsToXlms.Length > StaticDataBase.lastPathTaken)
            {
                if (StaticDataBase.amountOfThreads < StaticDataBase.howManyThreadDoIWant)
                {
                    StaticDataBase.amountOfThreads++;
                    new Thread(CheckPicturesFromCamera).Start();
                    
                }
                if (StaticDataBase.amountOfThreads == StaticDataBase.howManyThreadDoIWant)
                {
                    Thread.Sleep(1000);
                }
            }
            StaticDataBase.lastPathTaken = 0;

        }

        public void CheckItAllByte()
        {
            StaticDataBase sDB = new StaticDataBase();
            StaticDataBase.BestMatchProcent = 0;
            sDB.NameOfBestMatch = "";

            while (basePathsToXlms.Length > StaticDataBase.lastPathTaken)
            {
                if (StaticDataBase.amountOfThreads <= StaticDataBase.howManyThreadDoIWant)
                {
                    new Thread(CheckPicturesByte).Start();
                    StaticDataBase.amountOfThreads++;
                }
            }
            StaticDataBase.lastPathTaken = 0;

        }

        public void CheckPictures()
        {
            string myPath = "";
            if (StaticDataBase.lastPathTaken < basePathsToXlms.Length)
            {
                myPath = basePathsToXlms[StaticDataBase.lastPathTaken++];
            }

            if (myPath != "")
            {
                //StaticDataBase.lastPathTaken++;
                CreateDataBase.BaseList buffAdvantedList;
                buffAdvantedList = gettingDataFromXmlDouble(myPath);
                Image<Bgr, Byte> buffImgBgr = LoadImage(StaticDataBase.pathToSelectedFoto);
                byte[] buffForArray;
                Image<Gray, Byte> buffImgGray;
                double[] buffForXmlArray;
                buffImgGray = UseFilters(buffImgBgr);
                buffImgGray = buffImgGray.Resize(StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels, Inter.Cubic);
                buffImgGray = DropZeros(buffImgGray);
                
                buffImgBgr.Dispose();
                Image<Gray, Byte> buffImgGrayInside = new Image<Gray, byte>(buffImgGray.Width, buffImgGray.Height, new Gray(255));
                Image<Gray, Byte> buffImgGrayRotated = new Image<Gray, byte>(buffImgGray.Width, buffImgGray.Height, new Gray(255));
                Image<Gray, Byte> buffImgGrayZoomed = new Image<Gray, byte>(buffImgGray.Width, buffImgGray.Height, new Gray(255));
                

                for (int indexTypes = 0; indexTypes < 1/*11buffAdvantedList.Count*/; indexTypes++)
                {
                    buffImgGrayInside = buffImgGray;
                    buffForXmlArray = buffAdvantedList.allTypesOfSign; //11buffAdvantedList[indexTypes].allTypesOfSign;

                    for (double indexRotation = -StaticDataBase.maxRotation; indexRotation <= StaticDataBase.maxRotation; indexRotation+=StaticDataBase.howManyRadiansPerLoop)
                    {

                        buffImgGrayRotated = RotateGray(buffImgGray, indexRotation);

                        for (double indexZoom = (1 - StaticDataBase.maxZoom); indexZoom <= (1 + StaticDataBase.maxZoom); indexZoom += StaticDataBase.howMuchZoomPerLoop)
                        {
                            buffImgGrayZoomed = buffImgGrayRotated;

                            buffImgGrayZoomed = ZoomGray(buffImgGrayZoomed, indexZoom);

                            //buffImgGrayInside = buffImgGrayInside.Resize(resizeXInPixels, resizeYInPixels, Inter.Cubic);

                            buffForArray = makBinaryFromByte(buffImgGrayInside.Bytes);

                            arraysOfPrababilityDouble(buffForArray, buffForXmlArray, StaticDataBase.maxReach, myPath);
                            
                            
                        }
                    }
                }
                buffImgBgr.Dispose();
                
            }
            StaticDataBase.amountOfThreads--;
            
        }

        public void CheckPicturesFromCamera()
        {
            string myPath = "";
            if (StaticDataBase.lastPathTaken < basePathsToXlms.Length)
            {
                myPath = basePathsToXlms[StaticDataBase.lastPathTaken++];
            }

            if (myPath != "")
            {

                //lock (StaticDataBase.pictureFromCamera)
                {
                    Image<Bgr, Byte> buffImgBgr = new Image<Bgr, byte>(StaticDataBase.pictureFromCamera.Width, StaticDataBase.pictureFromCamera.Height);
                    //buffImgBgr = buffImgBgr.Resize(StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels, Inter.Cubic);
                    buffImgBgr = StaticDataBase.pictureFromCamera;
                    List<CreateDataBase.AdvantedList> buffAdvantedList;
                    buffAdvantedList = gettingDataFromXml(myPath);
                    byte[] buffForArray;
                    Image<Gray, Byte> buffImgGray;
                    byte[] buffForXmlArray;


                    buffImgGray = UseFilters(buffImgBgr);
                    buffImgGray = buffImgGray.Resize(StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels, Inter.Cubic);
                    buffImgGray = DropZeros(buffImgGray);
                
                //buffImgBgr.Dispose();

                //buffImgBgr.Dispose();
                Image<Gray, Byte> buffImgGrayInside = new Image<Gray, byte>(buffImgGray.Width, buffImgGray.Height, new Gray(255));
                Image<Gray, Byte> buffImgGrayRotated = new Image<Gray, byte>(buffImgGray.Width, buffImgGray.Height, new Gray(255));
                Image<Gray, Byte> buffImgGrayZoomed = new Image<Gray, byte>(buffImgGray.Width, buffImgGray.Height, new Gray(255));



                    for (int indexTypes = 0; indexTypes < buffAdvantedList.Count; indexTypes++)
                    {
                        buffImgGrayInside = buffImgGray;
                        buffForXmlArray = buffAdvantedList[indexTypes].allTypesOfSign;

                        for (double indexRotation = -StaticDataBase.maxRotation; indexRotation <= StaticDataBase.maxRotation; indexRotation += StaticDataBase.howManyRadiansPerLoop)
                        {

                            buffImgGrayRotated = RotateGray(buffImgGray, indexRotation);

                            for (double indexZoom = (1 - StaticDataBase.maxZoom); indexZoom <= (1 + StaticDataBase.maxZoom); indexZoom += StaticDataBase.howMuchZoomPerLoop)
                            {
                                buffImgGrayZoomed = buffImgGrayRotated;

                                buffImgGrayZoomed = ZoomGray(buffImgGrayZoomed, indexZoom);

                                //buffImgGrayInside = buffImgGrayInside.Resize(resizeXInPixels, resizeYInPixels, Inter.Cubic);

                                buffForArray = makBinaryFromByte(buffImgGrayInside.Bytes);

                                arraysOfPrabability(buffForArray, buffForXmlArray, StaticDataBase.maxReach, myPath);


                            }
                        }


                    }
            }
            

            }
            StaticDataBase.amountOfThreads--;

        }



        public void CheckPicturesByte()
        {
            string myPath = "";

            if (StaticDataBase.lastPathTaken < basePathsToXlms.Length)
            {
                myPath = basePathsToXlms[StaticDataBase.lastPathTaken++];
            }

            if (myPath != "")
            {
                //StaticDataBase.lastPathTaken++;
                List<CreateDataBase.AdvantedList> buffAdvantedList;
                buffAdvantedList = gettingDataFromXml(myPath);
                Image<Bgr, Byte> buffImgBgr = LoadImage(StaticDataBase.pathToSelectedFoto);
                buffImgBgr = buffImgBgr.Resize(StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels, Inter.Cubic);
                byte[] buffForArray;
                Image<Gray, Byte> buffImgGray;
                byte[] buffForXmlArray;

                buffImgGray = UseFilters(buffImgBgr);
                buffImgGray = buffImgGray.Resize(StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels, Inter.Cubic);
                buffImgGray = DropZeros(buffImgGray);

                buffImgBgr.Dispose();
                Image<Gray, Byte> buffImgGrayInside = new Image<Gray, byte>(buffImgGray.Width, buffImgGray.Height, new Gray(255));
                Image<Gray, Byte> buffImgGrayRotated = new Image<Gray, byte>(buffImgGray.Width, buffImgGray.Height, new Gray(255));
                Image<Gray, Byte> buffImgGrayZoomed = new Image<Gray, byte>(buffImgGray.Width, buffImgGray.Height, new Gray(255));


                for (int indexTypes = 0; indexTypes < buffAdvantedList.Count; indexTypes++)
                {
                    buffImgGrayInside = buffImgGray;
                    buffForXmlArray = buffAdvantedList[indexTypes].allTypesOfSign;

                    for (double indexRotation = -StaticDataBase.maxRotation; indexRotation <= StaticDataBase.maxRotation; indexRotation += StaticDataBase.howManyRadiansPerLoop)
                    {

                        buffImgGrayRotated = RotateGray(buffImgGray, indexRotation);

                        for (double indexZoom = (1 - StaticDataBase.maxZoom); indexZoom <= (1 + StaticDataBase.maxZoom); indexZoom += StaticDataBase.howMuchZoomPerLoop)
                        {
                            buffImgGrayZoomed = buffImgGrayRotated;

                            buffImgGrayZoomed = ZoomGray(buffImgGrayZoomed, indexZoom);

                            //buffImgGrayInside = buffImgGrayInside.Resize(resizeXInPixels, resizeYInPixels, Inter.Cubic);

                            buffForArray = makBinaryFromByte(buffImgGrayInside.Bytes);

                            arraysOfPrabability(buffForArray, buffForXmlArray, StaticDataBase.maxReach, myPath);


                        }
                    }
                }
                buffImgBgr.Dispose();

            }
            StaticDataBase.amountOfThreads--;

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

            for (int indexOutside = -reach; indexOutside <= reach; indexOutside+=StaticDataBase.howMuchReachForLoop)
            {
                double allProbabilitiesSummed = 0;
                int howManyHandBytes = 0;

                for (int index = 0; index < firstDoubleArray.Length; index++)
                {

                    if ((index + indexOutside) >= 0 && (index + indexOutside) < firstDoubleArray.Length)
                    {

                        if (firstDoubleArray[index] == secondDoubleArray[index + indexOutside]) //&& ((secondDoubleArray[index + indexOutside] != 0) && (firstDoubleArray[index])!=0))
                        {
                            //2 allProbabilitiesSummed += (firstDoubleArray[index] / secondDoubleArray[index + indexOutside]);
                            allProbabilitiesSummed++;
                            howManyHandBytes++;
                        }
                        else
                        {
                            howManyHandBytes++;
                        }
                        /*if (firstDoubleArray[index] > secondDoubleArray[index + indexOutside] )
                        {
                            //2 allProbabilitiesSummed += (secondDoubleArray[index + indexOutside] / firstDoubleArray[index]);
                            //2allProbabilitiesSummed--;
                            howManyHandBytes++;
                        }*/
                    }
                }
                if ((allProbabilitiesSummed/howManyHandBytes) > StaticDataBase.BestMatchProcent)
                {
                    lock (this)
                    {
                        StaticDataBase sDB = new StaticDataBase();
                        StaticDataBase.BestMatchProcent = allProbabilitiesSummed / howManyHandBytes;
                        sDB.NameOfBestMatch = myPath[myPath.Length - 5].ToString();
                    }
                }
            }
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

        void arraysOfPrababilityDouble(byte[] firstDoubleArray, double[] secondDoubleArray, int reach, string myPath)
        {
            //wstawka że jeśli firsDobleArray.Lenght = 0 to wywala błąd

            for (int indexOutside = -reach; indexOutside <= reach; indexOutside += StaticDataBase.howMuchReachForLoop)
            {
                double allProbabilitiesSummed = 0;
                int howManyHandBytes = 0;

                for (int index = 0; index < firstDoubleArray.Length; index++)
                {

                    if ((index + indexOutside) >= 0 && (index + indexOutside) < firstDoubleArray.Length)
                    {

                        if (firstDoubleArray[index] == 1) //&& ((secondDoubleArray[index + indexOutside] != 0) && (firstDoubleArray[index])!=0))
                        {
                            //2 allProbabilitiesSummed += (firstDoubleArray[index] / secondDoubleArray[index + indexOutside]);
                            allProbabilitiesSummed+= secondDoubleArray[index];
                            howManyHandBytes++;
                        }
                        else
                        {
                            howManyHandBytes++;
                        }
                        /*if (firstDoubleArray[index] > secondDoubleArray[index + indexOutside] )
                        {
                            //2 allProbabilitiesSummed += (secondDoubleArray[index + indexOutside] / firstDoubleArray[index]);
                            //2allProbabilitiesSummed--;
                            howManyHandBytes++;
                        }*/
                    }
                }
                if ((allProbabilitiesSummed / howManyHandBytes) > StaticDataBase.BestMatchProcent)
                {
                    lock (this)
                    {
                        StaticDataBase sDB = new StaticDataBase();
                        StaticDataBase.BestMatchProcent = allProbabilitiesSummed / howManyHandBytes;
                        sDB.NameOfBestMatch = myPath[myPath.Length - 5].ToString();
                    }
                }
            }
        }
    }
}
