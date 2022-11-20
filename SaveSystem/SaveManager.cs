using Assets.Scripts.InputSystem;
using Assets.Scripts.SaveSystem.Data;
using Assets.Scripts.Topology;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    internal class SaveManager : MonoBehaviour
    {
        [SerializeField] private Class classPrefab;
        [SerializeField] private Transition transitionPrefab;

        [SerializeField] private Transform canvasObject;
        [SerializeField] private Transform canvasTransition;

        [SerializeField] private string saveName;

        [SerializeField] private string[] saveFiles;

        public void GetLoadFiles()
        {
            if(!Directory.Exists(Application.persistentDataPath + "/saves"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/saves");
            }

            saveFiles = Directory.GetFiles(Application.persistentDataPath + "/saves");
        }

        [ContextMenu("Save")]
        public void OnSave(List<Class> classes, List<Transition> transitions)
        {
            SceneData sceneData = new SceneData();

            List<ClassData> classData = new List<ClassData>();
            List<TransitionData> transactionData = new List<TransitionData>();

            foreach (Class classToSave in classes)
            {
                ClassData data = new ClassData()
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
                    UnityEngine.Debug.LogError("Erro setup TransformData");
                }

                transactionData.Add(data);
            }

            sceneData.ClassData = classData;
            sceneData.TransactionData = transactionData;

            SerializationManager.Save(saveName, sceneData);
        }

        [ContextMenu("Load")]
        public void OnLoad(List<Class> a, List<Transition> b)
        {
            List<Class> classes = new List<Class>();
            List<Transition> transitions = new List<Transition>();

            SceneData sceneData = (SceneData)SerializationManager.Load(Application.persistentDataPath + "/saves" + saveName + ".save");

            foreach (ClassData data in sceneData.ClassData)
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
            b = transitions;
        }
    }
}
