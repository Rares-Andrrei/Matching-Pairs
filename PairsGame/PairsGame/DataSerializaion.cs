 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PairsGame
{
    internal class DataSerializaion
    {
        public static void BinarySerialization(object data, string filePath)
        {
            FileStream fileStream;
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                using(fileStream = File.Create(filePath))
                {
                    bf.Serialize(fileStream, data);
                }
            }
        }

        public static object BinaryDeserialization(string filePath)
        {
            object obj = null;
            FileStream fileStream;
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(filePath))
            {              
                using (fileStream = File.OpenRead(filePath))
                {
                    obj = bf.Deserialize(fileStream); // SerializationException
                }
            }
            else
            {
                fileStream = File.Create(filePath);
                fileStream.Close();
            }
            return obj;
        }    
    }
}
