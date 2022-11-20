using System.Collections.Generic;

namespace Assets.Scripts.Topology
{
    internal interface ISaveProvider
    {
        public List<VariableContainer> Variables { get; set; }
        public List<MethodContainer> Methods { get; set; }
    }
}