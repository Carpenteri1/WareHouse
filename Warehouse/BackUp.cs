using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Warehouse
{
    public class BackUp
    {
        private static string backUpPath = @"..\..\..\..\";
        private static string backUpFile = "backup.bin";
        private static string serializationFile = Path.Combine(backUpPath, backUpFile);
        //Serialize and creates file.
        public static void SaveDataToBin(WareHouse warehouse)
        {
            using (Stream stream = File.Open(serializationFile, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, warehouse);
                
                stream.Close();
            }
        }

        //deserialize file from binary format
        public static WareHouse LoadDataFromBin()
        {
            WareHouse newWareHouse = new WareHouse();
            try
            {
                using (Stream stream = File.Open(serializationFile, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    newWareHouse = (WareHouse)formatter.Deserialize(stream);
                    stream.Close();
                }
            }
            catch (FileNotFoundException)//if backup file doesnt excist, call the serialize method do create one
            {
                SaveDataToBin(newWareHouse);
            }
            return newWareHouse;
        }

    }
}
