using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    public class SceneScaler : MonoBehaviour
    {
        [SerializeField] private Transform scene;

        [SerializeField] private float coeff;

        private Vector3 newScale = Vector3.one;

        private void Update()
        {
            newScale += Vector3.one * Input.mouseScrollDelta.y * coeff;

            scene.localScale = Vector3.Lerp(scene.localScale,newScale, .02f);
        }
    }
}