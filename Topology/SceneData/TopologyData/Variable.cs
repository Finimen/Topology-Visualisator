using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable] public class Variable : TopologyData
    {
        [SerializeField] protected string name;

        [SerializeField] protected string type;

        [SerializeField] private ProtectType protectType;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }
        }

        public ProtectType ProtectType
        {
            get
            {
                return protectType;
            }
        }

        public Variable(string name, string type, ProtectType protect)
        {
            this.name = name;
            this.type = type;
            this.protectType = protect;
        }
    }
}