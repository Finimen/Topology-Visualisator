using UnityEngine;

namespace Assets.Scripts.UI
{
    [System.Serializable] internal struct CanvasesData
    {
        [SerializeField] private Transform topologyObjects;
        [SerializeField] private Transform transitions;

        public Transform TopologyObjets
        {
            get => topologyObjects;
        }

        public Transform Transitions
        {
            get => transitions;
        }
    }
}