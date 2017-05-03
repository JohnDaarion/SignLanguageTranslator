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
        public SignToLetterClass() { }

        public SignToLetterClass(string selectedFotoPath)
        {
            StaticDataBase.pathToSelectedFoto = selectedFotoPath;
        }

        public void CheckItAllCamera()
        {
            string[] basePathsToXlms = Directory.GetFileSystemEntries(StaticDataBase.pathToFolderWithXmls);
            StaticDataBase sDB = new StaticDataBase();
            StaticDataBase.BestMatchProcent = 0;
            sDB.NameOfBestMatch = "";
            for (int index = 0; index < basePathsToXlms.Length; index++)
            {    
                ThreadPool.QueueUserWorkItem(CheckPicturesFromCamera, basePathsToXlms[index]);
            }
        }

        private void CheckPicturesFromCamera(object state)
        {
            object pathe = state as object;
            string myPath = pathe.ToString();

            if (myPath != null)
            {
                {
                    Image<Bgr, Byte> buffImgBgr = new Image<Bgr, byte>(StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels);
                    if (StaticDataBase.isCameraWorking)
                        {
                            buffImgBgr = StaticDataBase.PictureFromCamera;
                        }
                    else
                        {
                            buffImgBgr = LoadImage(StaticDataBase.pathToSelectedFoto);
                        }
                    List<byte[]> buffAdvantedList;
                    buffAdvantedList = gettingDataFromXml<List<byte[]>>(myPath);
                    byte[] buffForArray;
                    Image<Gray, Byte> buffImgGray;
                    byte[] buffForXmlArray;

                    buffImgGray = UseFilters(buffImgBgr, StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels);
                    buffImgGray = DropZeros(buffImgGray);

                    Image<Gray, Byte> buffImgGrayInside = new Image<Gray, byte>(buffImgGray.Width, buffImgGray.Height, new Gray(255));
                    Image<Gray, Byte> buffImgGrayRotated = new Image<Gray, byte>(buffImgGray.Width, buffImgGray.Height, new Gray(255));
                    Image<Gray, Byte> buffImgGrayZoomed = new Image<Gray, byte>(buffImgGray.Width, buffImgGray.Height, new Gray(255));

                    for (int indexTypes = 0; indexTypes < buffAdvantedList.Count; indexTypes++)
                    {
                        buffImgGrayInside = buffImgGray;
                        buffForXmlArray = buffAdvantedList[indexTypes];
                        buffImgGrayRotated = buffImgGray;

                            for (double indexZoom = (1 - StaticDataBase.maxZoom); indexZoom <= (1 + StaticDataBase.maxZoom); indexZoom += StaticDataBase.howMuchZoomPerLoop)
                            {
                                buffImgGrayZoomed = buffImgGrayRotated;
                                buffImgGrayZoomed = ZoomGray(buffImgGrayZoomed, indexZoom);
                                buffForArray = makeBinaryFromByte(buffImgGrayInside.Bytes);
                                arraysOfPrabability(buffForArray, buffForXmlArray, StaticDataBase.maxReach, myPath);
                            }
                    }
                }
            }
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
         
        void arraysOfPrabability(byte[] firstDoubleArray, byte[] secondDoubleArray, int reach, string myPath)
        {
            for (int indexOutside = -reach; indexOutside <= reach; indexOutside+=StaticDataBase.howMuchReachForLoop)
            {
                if (arraysOfPrabability(firstDoubleArray, secondDoubleArray) > StaticDataBase.BestMatchProcent)
                {
                        StaticDataBase sDB = new StaticDataBase();
                        StaticDataBase.BestMatchProcent = arraysOfPrabability(firstDoubleArray, secondDoubleArray);
                        sDB.NameOfBestMatch = myPath[myPath.Length - 5].ToString();
                }
            }
        }
    }
}
