using Assets.Scripts.Topology;
using System.Collections.Generic;

namespace Assets.Scripts
{
    public class ObjectsLibary
    {
        private List<TopologyObject> topologyObjects;

        public void Initialize(int count)
        {
            topologyObjects = new List<TopologyObject>(count);
        }

        public void Add(TopologyObject topologyObject)
        {
            topologyObjects.Add(topologyObject);
        }

        public void Remove(TopologyObject topologyObject)
        {
            topologyObjects.Remove(topologyObject);
        }
    }
}