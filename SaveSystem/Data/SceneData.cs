using System;
using System.Collections.Generic;

namespace Assets.Scripts.SaveSystem.Data
{
    [Serializable] internal class SceneData
    {
        public List<TopologyObjetcData> ClassData;
        public List<TopologyObjetcData> InterfaceData;
        public List<TransitionData> TransactionData;
    }
}