using System;
using System.Collections.Generic;

namespace Assets.Scripts.SaveSystem.Data
{
    [Serializable] internal class SceneData
    {
        public List<ClassData> ClassData;
        public List<TransitionData> TransactionData;
    }
}