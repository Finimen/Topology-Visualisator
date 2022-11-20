using System;
using UnityEngine;

namespace Assets.Scripts.SaveSystem.Data
{
    [Serializable] internal class TransitionData
    {
        public Transform StartPosition;
        public Transform EndPosition;
        public Color BlackgroundColor;
    }
}
