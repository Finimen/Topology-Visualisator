using System.Collections.Generic;
using UnityEditor;

namespace Assets.Scripts.Topology
{
    public class ObjectsLibary
    {
        public List<Class> Classes;
        public List<Transition> Transitions;
        public List<Interface> Interfaces;

        private List<TopologyObject> topologyObjects;
        private List<Transition> transitions;

        public void Initialize(int count)
        {
            topologyObjects = new List<TopologyObject>(count);
            transitions = new List<Transition>(count);

            Classes = new List<Class>(count);
            Transitions = new List<Transition>(count);
            Interfaces = new List<Interface>(count);
        }

        public void Add(TopologyObject topologyObject)
        {
            topologyObjects.Add(topologyObject);

            if(topologyObject is Class)
            {
                Classes.Add((Class)topologyObject);
            }

            if(topologyObject is Interface)
            {
                Interfaces.Add((Interface)topologyObject);
            }
        }

        public void Remove(TopologyObject topologyObject)
        {
            topologyObjects.Remove(topologyObject);
        }

        public void Add(Transition transition)
        {
            transitions.Add(transition);
            Transitions.Add(transition);
        }

        public void Remove(Transition transition)
        {
            transitions.Remove(transition);
        }
    }
}