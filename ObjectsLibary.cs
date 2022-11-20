using System.Collections.Generic;

namespace Assets.Scripts.Topology
{
    public class ObjectsLibary
    {
        private List<TopologyObject> topologyObjects;
        private List<Transition> transitions;
        public List<Class> Classes;
        public List<Transition> Transitions;

        public void Initialize(int count)
        {
            topologyObjects = new List<TopologyObject>(count);
            transitions = new List<Transition>(count);
            Classes = new List<Class>(count);
        }

        public void Add(TopologyObject topologyObject)
        {
            topologyObjects.Add(topologyObject);

            if(topologyObject is Class)
            {
                Classes.Add((Class)topologyObject);
            }
        }

        public void Remove(TopologyObject topologyObject)
        {
            topologyObjects.Remove(topologyObject);
        }

        public void Add(Transition transition)
        {
            transitions.Add(transition);
        }

        public void Remove(Transition transition)
        {
            transitions.Remove(transition);
        }
    }
}