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
   public class CreateDataBase : CommonMethods
    {
         
        private string[] filePaths;
        private string folderName = "";
        private double procentOfSimilarityOfPicturesInFraction = 1;
        private string pathForSavingXml = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\baseData";

        public CreateDataBase(string path)
        {
            filePaths = new string [Directory.GetFileSystemEntries(path).Length];
            filePaths = Directory.GetFileSystemEntries(path);
            folderName = path[path.Length - 1].ToString();
        }

        public void UseClassMethods()
        {
            XmlSerialization(MakeListOfBytedPictures());
        }

        private void XmlSerialization<T>(T _object)
        {
            XmlSerializer serializer = new XmlSerializer(_object.GetType());

            Stream fs = new FileStream(pathForSavingXml + "\\" + folderName + ".xml", FileMode.Create);
            XmlWriter writer = new XmlTextWriter(fs, Encoding.Unicode);

            serializer.Serialize(fs, _object);
            fs.Close();

        }

        private List<byte[]> MakeListOfBytedPictures()
        {
            List<byte[]> buffList = new List<byte[]>();
            List<byte[]> buffDoubleList = new List<byte[]>();
            Image<Gray, Byte> imgGray = new Image<Gray, byte>(StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels, new Gray(0));


            for (int index = 0; index < filePaths.Length; index++)
            {
                imgGray = UseFilters(LoadImage(filePaths[index]), StaticDataBase.resizeXInPixels, StaticDataBase.resizeYInPixels);
                imgGray = DropZeros(imgGray);
                buffDoubleList.Add(makeBinaryFromByte(imgGray.Bytes));
            }

            if (buffList.Count == 0)
            {
                buffList.Add(buffDoubleList[0]);
            }

            for (int indexInternal = 0; indexInternal < buffDoubleList.Count; indexInternal++)
            {
                bool anySimilar = false;

                for (int index = 0; index < buffList.Count; index++)
                {
                    bool similar = true;

                    similar = arraysOfPrabability(buffDoubleList[indexInternal], buffList[index]) > procentOfSimilarityOfPicturesInFraction;
                    if (similar)
                    {
                        anySimilar = true;
                    }
                }
                if (!anySimilar)
                {
                    buffList.Add(buffDoubleList[indexInternal]);
                }
            }
            return buffList;
        }
    }

}
