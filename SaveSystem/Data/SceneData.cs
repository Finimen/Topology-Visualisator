using System;
using System.Collections.Generic;

namespace Assets.Scripts.SaveSystem.Data
{
    [Serializable] internal struct SceneData
    {
        public List<TopologyObjetcData> ClassData;
        public List<TopologyObjetcData> InterfaceData;
        public List<TransitionData> TransactionData;
    }
}