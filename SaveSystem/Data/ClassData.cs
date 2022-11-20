using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SaveSystem.Data
{
    [Serializable]
    public class ClassData
    {
        public List<Variable> Variables;
        public List<Method> Methods;

        public Vector3 Position;
        public Quaternion Rotation;

        [Space(20)]
        public string Name;
    }
}