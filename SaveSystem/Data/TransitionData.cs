using System;
using UnityEngine;

namespace Assets.Scripts.SaveSystem.Data
{
    [Serializable] internal struct TransitionData
    {
        public TransformData StartPosition;
        public TransformData EndPosition;
        public Color BlackgroundColor;
    }
}