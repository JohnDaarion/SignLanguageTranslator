using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignLanguageTranslator
{
    public class CommonMethods
    {
         public Image<Gray, Byte> UseFilters(Image<Bgr, Byte> _img)
        {
            Image<Bgr, Byte> imgBgr = new Image<Bgr, byte>(_img.Width, _img.Height, new Bgr(255,255,255));
            imgBgr = _img;
            Image<Gray, Byte> imgGray = new Image<Gray, byte>(_img.Width, _img.Height, new Gray(0));
            imgBgr = imgBgr.ThresholdBinary(new Bgr(StaticDataBase.Blue_threshold, StaticDataBase.Green_threshold, StaticDataBase.Red_threshold), new Bgr(StaticDataBase.maxBlueThreshold, StaticDataBase.maxGreenThreshold, StaticDataBase.maxRedThreshold));
            CvInvoke.CvtColor(imgBgr, imgGray, ColorConversion.Bgr2Gray);
            
            imgGray = imgGray.ThresholdToZero( new Gray(150));
            imgGray = imgGray.ThresholdBinary(new Gray(StaticDataBase.grayThreshold), new Gray(StaticDataBase.maxGrayThreshold));
            imgGray = imgGray.Resize(StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels, Inter.Cubic);

            //imgBgr.Dispose();

            return imgGray;
        }

        public Image<Bgr, Byte> UseFiltersBgr(Image<Bgr, Byte> _img)
        {
           
            Image<Bgr, Byte> imgBgr = new Image<Bgr, byte>(_img.Width, _img.Height, new Bgr(255,255,255));
            imgBgr = _img.ThresholdBinary(new Bgr(StaticDataBase.Blue_threshold, StaticDataBase.Green_threshold, StaticDataBase.Red_threshold), new Bgr(StaticDataBase.maxBlueThreshold, StaticDataBase.maxGreenThreshold, StaticDataBase.maxRedThreshold));
            imgBgr = imgBgr.Resize(StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels, Inter.Cubic);

            _img.Dispose();

            return imgBgr;
        }

        public Image<Bgr, Byte> LoadImage(string pathTaken)
        {
            return new Image<Bgr, Byte>(pathTaken);
        }

        public Image<Gray, Byte> DropZeros(Image<Gray, Byte> _img)
        {
            // lack of south loop

            
            byte[] buffArray = new byte[_img.Bytes.Length];
            buffArray = _img.Bytes;

            int howManyZeros = 0;
            Boolean wasThereAnyOne = false;

            int numberToCutNorth = 0;
            int numberToCutSouth = 0;
            int numberToCutWest = 0;
            int numberToCutEast = 0;

            // north loop

            for (int index = 0; index < (StaticDataBase.resizeYInPixels*StaticDataBase.resizeXInPixels); index+= StaticDataBase.resizeXInPixels)
            {
                for (int indexInternal = 0; indexInternal < StaticDataBase.resizeXInPixels; indexInternal++)
                {
                        if (buffArray[index+indexInternal] == 255)
                        {
                            howManyZeros++;
                        }

                        if (buffArray[index+indexInternal] == 0)
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

            // west loop
            howManyZeros = 0;
            wasThereAnyOne = false;

            for (int index = 0; index < StaticDataBase.resizeXInPixels; index++)
            {
                for (int indexInternal = 0; indexInternal < (StaticDataBase.resizeXInPixels*StaticDataBase.resizeYInPixels); indexInternal+=StaticDataBase.resizeXInPixels)
                {
                    if (buffArray[index + indexInternal] == 0)
                    {
                        howManyZeros++;
                    }

                    if (buffArray[index + indexInternal] == 255)
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
            // east loop
            howManyZeros = 0;
            wasThereAnyOne = false;

            for (int index = 1; index <= StaticDataBase.resizeXInPixels; index++)
            {
                for (int indexInternal = (StaticDataBase.resizeXInPixels * StaticDataBase.resizeYInPixels); indexInternal >0 ; indexInternal -= StaticDataBase.resizeXInPixels)
                {
                    if (buffArray[indexInternal - index] == 0)
                    {
                        howManyZeros++;
                    }

                    if (buffArray[indexInternal - index] == 255)
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
            // to keep ratio X to Y
            
            double ratioX = 0;
            double ratioY = 0;

            Image<Gray, Byte> buffImg = new Image<Gray, byte>(StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels, new Gray(0));


            ratioX = ((StaticDataBase.resizeXInPixels - (numberToCutWest + numberToCutEast)));
            ratioY = ((StaticDataBase.resizeYInPixels - (numberToCutNorth + numberToCutSouth)));
            

            if ((double)(ratioX / ratioY) < (double)((double)StaticDataBase.resizeXInPixels / (double)StaticDataBase.resizeYInPixels))
            {
                ratioX = (int)(((double)ratioY * (double)StaticDataBase.resizeXInPixels) / (double)(StaticDataBase.resizeYInPixels)) + 1;
                buffImg = buffImg.Resize((int)ratioX,(int)ratioY , Inter.Cubic);
            }
            else
            {
                ratioY = (int)(((double)ratioX * (double)StaticDataBase.resizeYInPixels) / (double)(StaticDataBase.resizeXInPixels)) + 1;
                buffImg = buffImg.Resize((int)ratioX, (int)ratioY, Inter.Cubic);
                
            }
                
            _img.ROI = new System.Drawing.Rectangle(numberToCutWest, numberToCutNorth, (StaticDataBase.resizeXInPixels - (numberToCutWest + numberToCutEast)), (StaticDataBase.resizeYInPixels - (numberToCutNorth + numberToCutSouth)));
            buffImg.ROI = new System.Drawing.Rectangle(0,0, (StaticDataBase.resizeXInPixels - (numberToCutWest + numberToCutEast)), (StaticDataBase.resizeYInPixels- (numberToCutNorth + numberToCutSouth)));
            _img.CopyTo(buffImg);
            buffImg.ROI = new System.Drawing.Rectangle(0, 0,(int)ratioX,(int)ratioY);
            buffImg = buffImg.Resize(StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels, Inter.Cubic);
            _img.ROI = new System.Drawing.Rectangle(0, 0, StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels);

            return buffImg;
        }


    }
}
