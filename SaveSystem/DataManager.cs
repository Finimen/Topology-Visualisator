using Assets.Scripts.SaveSystem.Data;
using Assets.Scripts.Topology;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Assets.Scripts.SaveSystem
{
    public class DataManager : MonoBehaviour
    {
        [SerializeField] private Class classPrefab;

        public void SavePosition(Vector3 position)
        {
            UnityEngine.Debug.Log(Application.dataPath + "/PositionData.json");

            string json = JsonUtility.ToJson(position, true);
            File.WriteAllText(Application.dataPath + "/PositionData.json", json);
        }

        public Vector3 LoadPosition() 
        {
            string json = File.ReadAllText(Application.dataPath + "/PositionData.json");
            return JsonUtility.FromJson<Vector3>(json);
        }

        public void SaveClass(Class classToSave)
        {
            UnityEngine.Debug.Log(Application.dataPath + "/PositionData.json");

            ClassData classData = new ClassData()
            {
                //Methods = classToSave.Methods,
                //Variables = classToSave.Variables,
                Name = classToSave.Name,
                Position = classToSave.transform.position,
            };

            string json = JsonUtility.ToJson(classData, true);
            File.WriteAllText(Application.dataPath + "/PositionData.json", json);
        }

        public Class LoadClass()
        {
            string json = File.ReadAllText(Application.dataPath + "/PositionData.json");
            ClassData classData = JsonUtility.FromJson<ClassData>(json);

            Class classClone = Instantiate(classPrefab, classData.Position, Quaternion.identity);

            //classClone.Variables = classData.Variables;

            //classClone.Methods = classData.Methods;

            classClone.Rename(classData.Name);

            return null;
        }
    }
}