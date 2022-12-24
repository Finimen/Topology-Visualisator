using UnityEngine;
using System.Collections;

namespace Assets.Scripts.SimpleAnimationSystem
{
    public class RotateAnimationController : MonoBehaviour
    {
        [Range(0,1)][SerializeField] private float timeLerp = .02f;

        [SerializeField] private RectTransform target;

        private bool rotated;

        public void UpdateState()
        {
            rotated = !rotated;

            Rotate(rotated ? 90 : 360);
        }

        private void Rotate(float xAngle)
        {
            target.Rotate(xAngle, xAngle, xAngle);

            UnityEngine.Debug.Log("DDDDDDDDDDDDDDD");
        }
    }
}