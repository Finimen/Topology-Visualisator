using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable] public class Method : TopologyData
    {
        [SerializeField] private ProtectType protectType;

        [SerializeField] private string name = "Method";

        [SerializeField] private string returnedType = "void";

        [SerializeField] private string arguments = "()";

        public ProtectType ProtectType
        {
            get
            {
                return protectType;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string ReturnedType
        {
            get
            {
                return returnedType;
            }
        }

        public string Arguments
        {
            get
            {
                return arguments;
            }
        }

        public Method(string name, string type, string arguments, ProtectType protect)
        {
            this.name = name;
            this.returnedType = type;
            this.protectType = protect;
            this.arguments = arguments;
        }
    }
}