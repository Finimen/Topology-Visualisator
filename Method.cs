using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable] public class Method
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
    }
}