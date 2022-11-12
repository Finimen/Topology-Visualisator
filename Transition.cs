using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Transition : MonoBehaviour
    {
        [SerializeField] private Image width;
        [SerializeField] private Image height;

        [SerializeField, Range(0,.1f)] private float scaleCoeff;

        [SerializeField] private Transform startPosition;
        [SerializeField] private Transform endPosition;

        private Vector3 startScaleWidth;
        private Vector3 startScaleHeight;

        public Transform StartPosition 
        {
            get
            {
                return startPosition;
            }
            set
            {
                startPosition = value;
            }
        }

        public Transform EndPosition
        {
            get
            {
                return endPosition;
            }
            set
            {
                endPosition = value;
            }
        }

        private void Start()
        {
            Reset();
        }

        private void Update()
        {
            width.transform.position = startPosition.position;
            height.transform.position = startPosition.position;

            float distanceY = endPosition.position.y - startPosition.position.y;

            height.transform.localScale = new Vector3(scaleCoeff, Mathf.Abs(distanceY) * startScaleHeight.y / 50 / 2, 1);

            if (distanceY > 0)
            {
                height.transform.position = new Vector3(height.transform.position.x, startPosition.position.y + 50 * height.transform.localScale.y, width.transform.position.z);
            }
            else
            {
                height.transform.position = new Vector3(height.transform.position.x, startPosition.position.y - 50 * height.transform.localScale.y, width.transform.position.z);
            }

            float distanceX = endPosition.position.x - startPosition.position.x;

            width.transform.localScale = new Vector3(Mathf.Abs(distanceX) * startScaleWidth.x / 50 / 2, scaleCoeff, 1);

            if (distanceX > 0)
            {
                if (height.transform.position.y > startPosition.position.y)
                {
                    width.transform.position = new Vector3(startPosition.position.x + 50 * width.transform.localScale.x - 5, startPosition.position.y + 50 * height.transform.localScale.y * 2, width.transform.position.z);
                }
                else
                {
                    width.transform.position = new Vector3(startPosition.position.x + 50 * width.transform.localScale.x - 5, startPosition.position.y - 50 * height.transform.localScale.y * 2, width.transform.position.z);
                }
            }
            else
            {
                if (height.transform.position.y > startPosition.position.y)
                {
                    width.transform.position = new Vector3(startPosition.position.x - 50 * width.transform.localScale.x + 5, startPosition.position.y + 50 * height.transform.localScale.y * 2, width.transform.position.z);
                }
                else
                {
                    width.transform.position = new Vector3(startPosition.position.x - 50 * width.transform.localScale.x + 5, startPosition.position.y - 50 * height.transform.localScale.y * 2, width.transform.position.z);
                }
            }
        }

        private void Reset()
        {
            startScaleWidth = Vector3.one;
            startScaleHeight = Vector3.one;

            width.transform.position = startPosition.position;
            height.transform.position = startPosition.position;
        }
    }
}