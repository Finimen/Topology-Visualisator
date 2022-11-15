using System;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    [Serializable] public class SceneScaler
    {
        private Transform scene;

        [SerializeField] private float coeff;

        private Vector3 newScale = Vector3.one;

        internal void Initialize(Transform scene)
        {
            this.scene = scene;
        }

        internal void Update()
        {
            newScale += Vector3.one * Input.mouseScrollDelta.y * coeff;

            scene.localScale = Vector3.Lerp(scene.localScale,newScale, .02f);
        }
    }
}