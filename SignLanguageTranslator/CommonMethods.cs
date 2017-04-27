using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SignLanguageTranslator
{
    public class CommonMethods
    {
        protected T gettingDataFromXml<T>(string path)
        {
            T buffAL;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (XmlReader reader = XmlReader.Create(fs))
                {
                    buffAL = (T)serializer.Deserialize(reader);
                }
            }
            return buffAL;
        }

        public Image<Gray, Byte> UseFilters(Image<Bgr, Byte> _img, int resizeX, int resizeY)
        {
                Image<Bgr, Byte> imgBgr = new Image<Bgr, byte>(_img.Width, _img.Height, new Bgr(255, 255, 255));
                imgBgr = _img;
                Image<Gray, Byte> imgGray = new Image<Gray, byte>(_img.Width, _img.Height, new Gray(0));
                imgBgr = imgBgr.ThresholdBinary(new Bgr(StaticDataBase.Blue_threshold, StaticDataBase.Green_threshold, StaticDataBase.Red_threshold), new Bgr(StaticDataBase.maxBlueThreshold, StaticDataBase.maxGreenThreshold, StaticDataBase.maxRedThreshold));
                CvInvoke.CvtColor(imgBgr, imgGray, ColorConversion.Bgr2Gray);

                imgGray = imgGray.ThresholdToZero(new Gray(150));
                imgGray = imgGray.ThresholdBinary(new Gray(StaticDataBase.grayThreshold), new Gray(StaticDataBase.maxGrayThreshold));
                imgGray = imgGray.Resize(resizeX, resizeY, Inter.Cubic);

                return imgGray;
        }

        public Image<Bgr, Byte> LoadImage(string pathTaken)
        {
            return new Image<Bgr, Byte>(pathTaken);
        }

        protected byte[] makeBinaryFromByte(byte[] _buffBytes)
        {
            byte[] buffByte = new byte[_buffBytes.Length];

            for (int indexOfInternalLoop = 0; indexOfInternalLoop < _buffBytes.Length; indexOfInternalLoop++)
            {
                buffByte[indexOfInternalLoop] = (byte)(_buffBytes[indexOfInternalLoop] / 255);
            }
            return buffByte;
        }

        private int HowManyZerosDropNorth(byte[] buffArray)
        {
            int numberToCutNorth = 0;
            int howManyZeros = 0;
            Boolean wasThereAnyOne = false;

            for (int index = 0; index < (StaticDataBase.resizeYInPixels * StaticDataBase.resizeXInPixels); index += StaticDataBase.resizeXInPixels)
            {
                for (int indexInternal = 0; indexInternal < StaticDataBase.resizeXInPixels; indexInternal++)
                {
                    if (buffArray[index + indexInternal] == 255)
                    {
                        howManyZeros++;
                    }

                    if (buffArray[index + indexInternal] == 0)
                    {
                        wasThereAnyOne = true;
                        break;
                    }

                    if (howManyZeros == StaticDataBase.resizeXInPixels)
                    {
                        numberToCutNorth++;
                        howManyZeros = 0;
                        break;
                    }
                }

                if (wasThereAnyOne == true)
                {
                    break;
                }
            }
            return numberToCutNorth;


        }
        private int HowManyZerosDropWest(byte[] buffArray)
        {
            int numberToCutWest = 0;
            int howManyZeros = 0;
            Boolean wasThereAnyOne = false;
            for (int index = 0; index < StaticDataBase.resizeXInPixels; index++)
            {
                
                for (int indexInternal = 0; indexInternal < (StaticDataBase.resizeXInPixels * StaticDataBase.resizeYInPixels); indexInternal += StaticDataBase.resizeXInPixels)
                {
                    if (buffArray[index + indexInternal] == 255)
                    {
                        howManyZeros++;
                    }

                    if (buffArray[index + indexInternal] == 0)
                    {
                        wasThereAnyOne = true;
                        break;
                    }

                    if (howManyZeros == StaticDataBase.resizeYInPixels)
                    {
                        numberToCutWest++;
                        howManyZeros = 0;
                        break;
                    }
                }

                if (wasThereAnyOne == true)
                {
                    break;
                }
            }
            return numberToCutWest;
        }

        private int HowManyZerosDropEast(byte[] buffArray)
        {
            int numberToCutEast = 0;
            int howManyZeros = 0;
            Boolean wasThereAnyOne = false;
            for (int index = 1; index <= StaticDataBase.resizeXInPixels; index++)
            {
                for (int indexInternal = (StaticDataBase.resizeXInPixels * StaticDataBase.resizeYInPixels); indexInternal > 0; indexInternal -= StaticDataBase.resizeXInPixels)
                {
                    if (buffArray[indexInternal - index] == 255)
                    {
                        howManyZeros++;
                    }

                    if (buffArray[indexInternal - index] == 0)
                    {
                        wasThereAnyOne = true;
                        break;
                    }

                    if (howManyZeros == StaticDataBase.resizeYInPixels)
                    {
                        numberToCutEast++;
                        howManyZeros = 0;
                        break;
                    }
                }

                if (wasThereAnyOne == true)
                {
                    break;
                }
            }

            return numberToCutEast;
        }

        public Image<Gray, Byte> DropZeros(Image<Gray, Byte> _img)
        {
            byte[] buffArray = new byte[_img.Bytes.Length];
            buffArray = _img.Bytes;

            int numberToCutNorth = HowManyZerosDropNorth(_img.Bytes);
            int numberToCutSouth = 0;
            int numberToCutWest = HowManyZerosDropWest(_img.Bytes);
            int numberToCutEast = HowManyZerosDropEast(_img.Bytes);
            
            // to keep ratio X to Y
            
            double ratioX = 0;
            double ratioY = 0;

            Image<Gray, Byte> buffImg = new Image<Gray, byte>(StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels, new Gray(255));

            ratioX = ((StaticDataBase.resizeXInPixels - (numberToCutWest + numberToCutEast)));
            ratioY = ((StaticDataBase.resizeYInPixels - (numberToCutNorth + numberToCutSouth)));

            if (areRatiosCorrect(ratioX, ratioY))
            {
                if ((double)(ratioX / ratioY) < (double)((double)StaticDataBase.resizeXInPixels / (double)StaticDataBase.resizeYInPixels))
                {
                    ratioX = (int)(((double)ratioY * (double)StaticDataBase.resizeXInPixels) / (double)(StaticDataBase.resizeYInPixels));
                    buffImg = buffImg.Resize((int)ratioX, (int)ratioY, Inter.Cubic);
                }
                else
                {
                    ratioY = (int)(((double)ratioX * (double)StaticDataBase.resizeYInPixels) / (double)(StaticDataBase.resizeXInPixels));
                    buffImg = buffImg.Resize((int)ratioX, (int)ratioY, Inter.Cubic);
                }
            }
            double newRatioX = ((StaticDataBase.resizeXInPixels - (numberToCutWest + numberToCutEast)));
            double newRatioY = ((StaticDataBase.resizeYInPixels - (numberToCutNorth + numberToCutSouth)));

            if ((newRatioX >= 0 && newRatioY >= 0 && numberToCutWest < _img.Width && numberToCutNorth < _img.Height && numberToCutWest  >= 0 && numberToCutNorth >= 0))
            {
                _img.ROI = new System.Drawing.Rectangle(numberToCutWest, numberToCutNorth, (int)newRatioX, (int)newRatioY);
                buffImg.ROI = new System.Drawing.Rectangle(0, 0, (int)newRatioX, (int)newRatioY);
                _img.CopyTo(buffImg);
                buffImg.ROI = new System.Drawing.Rectangle(0, 0, (int)ratioX, (int)ratioY);
                buffImg = buffImg.Resize(StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels, Inter.Cubic);
                _img.ROI = new System.Drawing.Rectangle(0, 0, StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels);
                return buffImg;
            }
            else
            {
                return _img;
            }
            
        }
        protected bool areRatiosCorrect(double ratioX, double ratioY)
        {
            return ratioX != 0 && ratioY != 0;
        }

        protected double arraysOfPrabability(byte[] firstDoubleArray, byte[] secondDoubleArray)
        {
            if (firstDoubleArray.Length == 0)
            {
                return 0;
            }

            double allProbabilitiesSummed = 0;

            for (int index = 0; index < firstDoubleArray.Length; index++)
            {
                if (firstDoubleArray[index] == secondDoubleArray[index])
                {
                    allProbabilitiesSummed++;
                }
            }
            return ((allProbabilitiesSummed / firstDoubleArray.Length));
        }

    }
}
