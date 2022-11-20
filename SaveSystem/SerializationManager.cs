using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    internal class SerializationManager
    {
        public static bool Save(string saveName, object saveData)
        {
            BinaryFormatter formatter = GetBinaryFormatter();

            if(!Directory.Exists(Application.persistentDataPath + "/saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            }

            string path = Application.persistentDataPath + "/saves" + saveName + ".save";

            UnityEngine.Debug.Log(path);

            FileStream fileStream = File.Create(path);

            formatter.Serialize(fileStream, saveData);

            fileStream.Close();

            //JsonConvert.SerializeObject()

            return true;
        }

        public static object Load(string path)
        {
            UnityEngine.Debug.Log(path);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            BinaryFormatter formatter = GetBinaryFormatter();

            FileStream fileStream = File.Open(path, FileMode.Open);

            try
            {
                object save = formatter.Deserialize(fileStream);
                fileStream.Close();
                return save;
            }
            catch
            {
                UnityEngine.Debug.LogErrorFormat ("Faild to load file at {0}", path);
                fileStream.Close();
                throw new FileNotFoundException();
            }
        }

        private static BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            SurrogateSelector surrogateSelector = new SurrogateSelector();

            Vector3SerializationSurrogate vector3Surrogate = new Vector3SerializationSurrogate();
            QuaternionSerializationSurrogate quaternionSurrogate = new QuaternionSerializationSurrogate();
            ColorSerializationSurrogate colorSerializationSurrogate = new ColorSerializationSurrogate();

            surrogateSelector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All), vector3Surrogate);
            surrogateSelector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All), quaternionSurrogate);
            surrogateSelector.AddSurrogate(typeof(Color), new StreamingContext(StreamingContextStates.All), colorSerializationSurrogate);

            formatter.SurrogateSelector = surrogateSelector;

            return formatter;
        }
    }
}
