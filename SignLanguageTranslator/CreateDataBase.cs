using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace SignLanguageTranslator
{
   public class CreateDataBase
    {
         public class AdvantedList
        {
            public byte[] allTypesOfSign;
            public int numberOfPhotosMerged = 0;
            

            public AdvantedList(byte[] _allTypesOfSign, int _numberOfPhotosMerged)
            {
                allTypesOfSign = _allTypesOfSign;
                numberOfPhotosMerged = _numberOfPhotosMerged;
            }
            public AdvantedList() { }
        }
        
        string[] filePaths;
        string folderName = "";
        double procentOfSimilarityOfPicturesInFraction = 0.95;
        public string pathForSavingXml = "D:\\dokumenty\\Visual Studio 2015\\Projects\\SignLanguageTranslator\\SignLanguageTranslator\\bin\\x64\\Debug\\baseData";

        public CreateDataBase(string path)
        {
            filePaths = new string [Directory.GetFileSystemEntries(path).Length];
            filePaths = Directory.GetFileSystemEntries(path);
            folderName = path[path.Length - 1].ToString();
        }
        


        Image<Bgr, Byte> LoadImage(string pathTaken)
        {
            return new Image<Bgr, Byte>(pathTaken);
        }

      

        Image<Gray, Byte> useFilters(Image<Bgr, Byte> _img)
        {
            int Blue_threshold = 100;
            int Green_threshold = 0;
            int Red_threshold = 100;
            Image<Gray, Byte> imgBuff = new Image<Gray, byte>(_img.Width, _img.Height, new Gray(255));
            Image<Bgr, Byte> img; 
            img = _img;


            
                img = img.ThresholdBinary(new Bgr(Blue_threshold, Green_threshold, Red_threshold), new Bgr(255, 255, 255));

                CvInvoke.CvtColor(img, imgBuff, ColorConversion.Bgr2Gray);

                imgBuff = imgBuff.ThresholdBinary(new Gray(200), new Gray(255));
                imgBuff = imgBuff.Resize(StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels, Inter.Cubic);

            
           
                _img.Dispose();
                img.Dispose();
            
            return imgBuff;
        }




        double arraysOfPrabability(byte[] firstDoubleArray, byte[] secondDoubleArray)
        {
            //wstawka że jeśli firsDobleArray.Lenght = 0 to wywala błąd

            double allProbabilitiesSummed = 0;

            for (int index = 0; index < firstDoubleArray.Length; index++)
            {
                if (firstDoubleArray[index] <= secondDoubleArray[index] && secondDoubleArray[index] != 0)
                { allProbabilitiesSummed += (firstDoubleArray[index] / secondDoubleArray[index]); }
                if (firstDoubleArray[index] > secondDoubleArray[index] && firstDoubleArray[index] != 0)
                { allProbabilitiesSummed += (secondDoubleArray[index] / firstDoubleArray[index]); }
                if (secondDoubleArray[index] == 0 && firstDoubleArray[index] == 0)
                { allProbabilitiesSummed++; }
            }
            return ((allProbabilitiesSummed / firstDoubleArray.Length));
        }

        byte[] makeDoubleFromBytes(Image<Gray, Byte> _img)
        {
            byte[] buffForBytes;
            byte[] buffForProcents = new byte[_img[0].Bytes.Length];

                buffForBytes = _img.Bytes;

                for (int indexOfInternalLoop = 0; indexOfInternalLoop < buffForBytes.Length; indexOfInternalLoop++)
                {
                    buffForProcents[indexOfInternalLoop] = (byte)(buffForBytes[indexOfInternalLoop]/255);
                }
            

            return buffForProcents;
        }

        byte[] countProcentApperanceOfBits(Image<Gray, Byte>[] _img)
        {
            byte[] buffForBytes;
            byte[] buffForProcents = new byte[_img[0].Bytes.Length]; 

            for (int index = 0; index < _img.Length; index++)
            {
                buffForBytes = _img[index].Bytes;
                

                for (int indexOfInternalLoop = 0; indexOfInternalLoop < buffForBytes.Length; indexOfInternalLoop++)
                {
                    buffForProcents[indexOfInternalLoop] += buffForBytes[indexOfInternalLoop];
                }
            }

            for (int index = 0; index < _img[0].Bytes.Length; index++)
            {
                buffForProcents[index] = (byte)(1 - ((buffForProcents[index]/_img.Length)/255));
            }
            return buffForProcents;
        }

        public List<AdvantedList> MakeListOfBytedPictures()
        {
            List <AdvantedList> buffList = new List <AdvantedList>();
            List<byte[]> buffDoubleList = new List<byte[]>();


            for (int index = 0; index < filePaths.Length; index++)
            {
                buffDoubleList.Add(makeDoubleFromBytes(useFilters(LoadImage(filePaths[index]))));
            }

            if (buffList.Count == 0)
            {
                buffList.Add(new AdvantedList(buffDoubleList[0], 1));
            }


            for (int indexInternal = 0; indexInternal < buffDoubleList.Count; indexInternal++)
            {
                
                bool anySimilar = false;
                for (int index = 0; index < buffList.Count; index++)
                {
                    bool similar = true;
                    similar = arraysOfPrabability(buffDoubleList[indexInternal], buffList[index].allTypesOfSign)> procentOfSimilarityOfPicturesInFraction;
                    if (similar)
                    {
                        anySimilar = true;
                    }
                }
                if (!anySimilar)
                {
                    buffList.Add(new AdvantedList(buffDoubleList[indexInternal], 1));
                }

            }
            
            
            return buffList;
        }
        

        void XmlSerialization(List<AdvantedList> _object)
        {
            XmlSerializer serializer = new XmlSerializer(_object.GetType());

            Stream fs = new FileStream(pathForSavingXml+"\\"+folderName + ".xml", FileMode.Create);
            XmlWriter writer = new XmlTextWriter(fs, Encoding.Unicode);

            serializer.Serialize(fs,_object);
            fs.Close();
            
        }
        
        public void UseClassMethods()
        {

            XmlSerialization(MakeListOfBytedPictures());
        }

    }
}
