using UnityEngine;

namespace Assets.Scripts.Topology
{
    public interface ITransitionProvider
    {
        public Vector3 startScaleWidth { get; set; }
        public Vector3 startScaleHeight { get; set; }
    }
}