using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Topology
{
    using Debug = UnityEngine.Debug;

    public class Transition : MonoBehaviour, ISceneObject
    {
        [SerializeField] private Image width;
        [SerializeField] private Image height;

        [SerializeField, Range(0,.1f)] private float scaleCoeff;

        [SerializeField] private Transform startPosition;
        [SerializeField] private Transform endPosition;

        private ISceneObject startObject;
        private ISceneObject endObject;

        private TransitionMode mode;

        private Vector3 startScaleWidth;
        private Vector3 startScaleHeight;

        private bool isSpawned;

        public Transform StartPosition 
        {
            get
            {
                return startPosition;
            }
            set
            {
                startPosition = value;

                if(TryGetComponent(out ISceneObject sceneObject))
                {
                    startObject = sceneObject;
                }
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

                if (TryGetComponent(out ISceneObject sceneObject))
                {
                    endObject = sceneObject;
                }
            }
        }

        public Color BlackgroundColor
        {
            get
            {
                return width.color;
            }
            set
            {
                width.color = value;
                height.color = value;
            }
        }

        public void Spawn()
        {
            isSpawned = true;

            if(endObject is Interface || startObject is Interface)
            {
                Debug.Log("BlueColor");
            }
            else if (endObject is Class && startObject is Class)
            {
                Debug.Log("GreenColor");
            }
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void SetScale(float scale)
        {
            scaleCoeff = (float)scale / 10;
        }

        private void Start()
        {
            Reset();
        }

        private void Update()
        {
            if (!startPosition || !endPosition)
            {
                Destroy(gameObject);
                return;
            }

            mode = TransitionMode.Engular;

            switch (mode)
            {
                case TransitionMode.Engular:
                    EngularMode();
                    break;
                case TransitionMode.Linear:
                    LinearMode();
                    break;
            }

            if(isSpawned)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse1))
            {
                Destroy(gameObject);
            }
        }

        private void EngularMode()
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

        private void LinearMode()
        {
            width.gameObject.SetActive(false);

            height.transform.position = startPosition.position;

            float distance = Vector2.Distance(endPosition.position, startPosition.position);

            height.transform.position = new Vector3(startPosition.position.x * width.transform.localScale.x - 5, startPosition.position.y + 50 * height.transform.localScale.y * 2, width.transform.position.z);

            height.transform.localScale = new Vector3(Mathf.Abs(distance) * startScaleWidth.x / 50, scaleCoeff, 1);
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