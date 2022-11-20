using Assets.Scripts.Topology;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SaveSystem
{
    internal class SaveBehaviour : MonoBehaviour
    {
        [SerializeField] private SaveManager saveManager;

        [SerializeField] private ObjectFactory objectFactory;

        public void Save()
        {
            saveManager.OnSave(objectFactory.ObjectsLibary.Classes, objectFactory.ObjectsLibary.Interfaces, objectFactory.ObjectsLibary.Transitions);
        }

        public void Load()
        {
            foreach(Class obj in objectFactory.ObjectsLibary.Classes)
            {
                if(obj != null)
                {
                    Destroy(obj.gameObject);
                }
            }

            foreach(Interface obj in objectFactory.ObjectsLibary.Interfaces)
            {
                if (obj != null)
                {
                    Destroy(obj.gameObject);
                }
            }

            foreach(Transition obj in objectFactory.ObjectsLibary.Transitions)
            {
                if (obj != null)
                {
                    Destroy(obj.gameObject);
                }
            }

            objectFactory.ObjectsLibary.Classes = new List<Class>();
            objectFactory.ObjectsLibary.Interfaces = new List<Interface>();
            objectFactory.ObjectsLibary.Transitions = new List<Transition>();

            saveManager.OnLoad(objectFactory.ObjectsLibary.Classes, objectFactory.ObjectsLibary.Interfaces, objectFactory.ObjectsLibary.Transitions);
        }
    }
}