using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable] public class Variable
    {
        [SerializeField] protected string name;

        [SerializeField] private ProtectType protectType;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public ProtectType ProtectType
        {
            get
            {
                return protectType;
            }
        }
    }
}