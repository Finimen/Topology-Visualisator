using Assets.Scripts.InputSystem;
using Assets.Scripts.SaveSystem.Data;
using Assets.Scripts.Topology;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    internal class SaveManager : MonoBehaviour
    {
        [SerializeField] private Class classPrefab;
        [SerializeField] private Interface interfacePrefab;
        [SerializeField] private Transition transitionPrefab;

        [SerializeField] private Transform canvasObject;
        [SerializeField] private Transform canvasTransition;

        [SerializeField] private string saveName;
        [SerializeField] private string folder = "/saves/";

        public void SetSaveName(string saveName)
        {
            this.saveName = saveName;
        }

        public void SetFolder(string folder)
        {
            this.folder = folder;
        }

        public void ClearFolder()
        {
            foreach(var file in GetLoadFiles())
            {
                File.Delete(file.Replace(@"\", @"/"));
            }
        }

        [ContextMenu(nameof(GetLoadFiles))]
        public string[] GetLoadFiles()
        {
            string savePath = Application.persistentDataPath + folder;

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            string[] saveFiles = Directory.GetFiles(savePath);

            return saveFiles;
        }

        [ContextMenu("Save")]
        public void OnSave(List<Class> classes,List<Interface> interfaces, List<Transition> transitions)
        {
            SceneData sceneData = new SceneData();

            List<TopologyObjetcData> classData = new List<TopologyObjetcData>();
            List<TopologyObjetcData> interfaceData = new List<TopologyObjetcData>();
            List<TransitionData> transactionData = new List<TransitionData>();

            foreach (Class classToSave in classes)
            {
                TopologyObjetcData data = new TopologyObjetcData()
                {
                    Methods = classToSave.Methods,
                    Name = classToSave.Name,
                    Position = classToSave.transform.position,
                    Rotation = classToSave.transform.rotation,
                    Variables = classToSave.Variables,
                    BlackgroundColor = classToSave.BlackgroundColor,
                };

                classData.Add(data);
            }

            foreach (Interface interfaceToSave in interfaces)
            {
                TopologyObjetcData data = new TopologyObjetcData()
                {
                    Methods = interfaceToSave.Methods,
                    Name = interfaceToSave.Name,
                    Position = interfaceToSave.transform.position,
                    Rotation = interfaceToSave.transform.rotation,
                    Variables = interfaceToSave.Variables,
                    BlackgroundColor = interfaceToSave.BlackgroundColor,
                };

                interfaceData.Add(data);
            }

            foreach (Transition transitionToSave in transitions)
            {
                TransitionData data = new TransitionData()
                {
                    BlackgroundColor = transitionToSave.BlackgroundColor,
                };

                try
                {
                    data.StartPosition = new TransformData(transitionToSave.StartPosition.position, transitionToSave.StartPosition.rotation, transitionToSave.transform.localScale);
                    data.EndPosition = new TransformData(transitionToSave.EndPosition.position, transitionToSave.EndPosition.rotation, transitionToSave.transform.localScale);
                }
                catch
                {
                    UnityEngine.Debug.LogError("Error setup TransformData");
                }

                transactionData.Add(data);
            }

            sceneData.ClassData = classData;
            sceneData.InterfaceData = interfaceData;
            sceneData.TransactionData = transactionData;

            SerializationManager.Save(saveName, folder, sceneData);
        }

        [ContextMenu("Load")]
        public void OnLoad(List<Class> a, List<Interface> b, List<Transition> c)
        {
            List<Class> classes = new List<Class>();
            List<Interface> interfaces = new List<Interface>();
            List<Transition> transitions = new List<Transition>();

            UnityEngine.Debug.Log(Application.persistentDataPath + folder + saveName + ".save");

            SceneData sceneData = (SceneData)SerializationManager.Load(Application.persistentDataPath + folder + saveName + ".save");

            foreach (TopologyObjetcData data in sceneData.ClassData)
            {
                Class classClone = Instantiate(classPrefab, canvasObject);

                classClone.Rename(data.Name);

                classClone.transform.position = data.Position;
                classClone.transform.rotation = data.Rotation;

                classClone.Variables = data.Variables;
                classClone.Methods = data.Methods;

                classClone.BlackgroundColor = data.BlackgroundColor;

                classClone.GetComponent<MoveableObject>().Setup(FindObjectOfType<InputServise>());

                classes.Add(classClone);
            }

            foreach (TopologyObjetcData data in sceneData.InterfaceData)
            {
                Interface interfaceClone = Instantiate(interfacePrefab, canvasObject);

                interfaceClone.Rename(data.Name);

                interfaceClone.transform.position = data.Position;
                interfaceClone.transform.rotation = data.Rotation;

                interfaceClone.Variables = data.Variables;
                interfaceClone.Methods = data.Methods;

                interfaceClone.BlackgroundColor = data.BlackgroundColor;

                interfaceClone.GetComponent<MoveableObject>().Setup(FindObjectOfType<InputServise>());

                interfaces.Add(interfaceClone);
            }

            try
            {
                foreach (TransitionData data in sceneData.TransactionData)
                {
                    Transition transitionClone = Instantiate(transitionPrefab, canvasTransition);

                    transitionClone.StartPosition.position = data.StartPosition.Position;
                    transitionClone.StartPosition.rotation = data.StartPosition.Rotation;

                    transitionClone.EndPosition.position = data.EndPosition.Position;
                    transitionClone.EndPosition.rotation = data.EndPosition.Rotation;

                    transitionClone.BlackgroundColor = data.BlackgroundColor;

                    transitions.Add(transitionClone);
                }
            }
            catch
            {
                UnityEngine.Debug.LogError("Error load Transition");
            }

            a = classes;
            b = interfaces;
            c = transitions;
        }
    }
}