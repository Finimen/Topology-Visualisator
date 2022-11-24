using Assets.Scripts.SaveSystem;
using Assets.Scripts.Topology;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;

namespace Assets.Scripts.ReturnSystem
{
    using static MonoBehaviour;
    internal static class Undo
    { 
        private static LinkedList<string> names = new LinkedList<string>();

        private static string folder = "/TemporaryStorages/";

        public static void Return()
        {
            Load();

            names.RemoveLast();
        }

        public static void Unreturn()
        {

        }

        public static void ApplicationExit()
        {
            SaveManager saveManager = MonoBehaviour.FindObjectOfType<SaveManager>();

            saveManager.SetFolder(folder);
            saveManager.ClearFolder();
        }

        public static void Record(string name, ObjectsLibary objectsLibary)
        {
            Save(objectsLibary.Classes, objectsLibary.Interfaces, objectsLibary.Transitions);
        }

        private static void Save(List<Class> classes, List<Interface> interfaces, List<Transition> transitions)
        {
            SaveManager saveManager = MonoBehaviour.FindObjectOfType<SaveManager>();

            names.AddLast("TemporaryStorage" + names.Count);

            saveManager.SetSaveName(names.Last.Value);
            saveManager.SetFolder(folder);

            saveManager.OnSave(classes, interfaces, transitions);
        }

        private static void Load()
        {
            SaveManager saveManager = MonoBehaviour.FindObjectOfType<SaveManager>();
            ObjectFactory objectFactory = MonoBehaviour.FindObjectOfType<ObjectFactory>();

            saveManager.SetSaveName(names.Last.Value);
            saveManager.SetFolder(folder);

            foreach (Class obj in objectFactory.ObjectsLibary.Classes)
            {
                if (obj != null)
                {
                    MonoBehaviour.Destroy(obj.gameObject);
                }
            }

            foreach (Interface obj in objectFactory.ObjectsLibary.Interfaces)
            {
                if (obj != null)
                {
                    MonoBehaviour.Destroy(obj.gameObject);
                }
            }

            foreach (Transition obj in objectFactory.ObjectsLibary.Transitions)
            {
                if (obj != null)
                {
                    MonoBehaviour.Destroy(obj.gameObject);
                }
            }

            objectFactory.ObjectsLibary.Classes = new List<Class>();
            objectFactory.ObjectsLibary.Interfaces = new List<Interface>();
            objectFactory.ObjectsLibary.Transitions = new List<Transition>();

            saveManager.OnLoad(objectFactory.ObjectsLibary.Classes, objectFactory.ObjectsLibary.Interfaces, objectFactory.ObjectsLibary.Transitions);
        }
    }
}