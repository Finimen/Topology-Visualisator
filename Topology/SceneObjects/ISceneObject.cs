using UnityEngine;

namespace Assets.Scripts.Topology
{
    public interface ISceneObject
    {
        public Color BlackgroundColor { get; set; }

        public void Destroy();

        public void SetScale(float scale);
    }
}