using Assets.Scripts.InputSystem;
using Assets.Scripts.Topology;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    [RequireComponent(typeof(TopologyObject))]
    public class MoveableObject : MonoBehaviour
    {
        [Inject] private Camera cameraMain;

        private InputServise inputServise;

        private TopologyObject current;

        private Vector3 offest;

        private bool movingStarted;

        private void OnDisable()
        {
            inputServise.OnTopologyObjectSelected -= Select;
        }

        public void Setup(InputServise inputServise)
        {
            current = GetComponent<TopologyObject>();

            this.inputServise = inputServise;

            inputServise.OnTopologyObjectSelected += Select;
        }

        private void Select(TopologyObject objectSelected)
        {
            if (movingStarted)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && objectSelected == current)
            {
                movingStarted = true;

                offest = Input.mousePosition - transform.position;
            }
        }

        private void Update()
        {
            if (movingStarted)
            {
                Move(Input.mousePosition);

                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    movingStarted = false;
                }
            }
        }

        private void Move(Vector3 currentMousePosition)
        {
            transform.position = currentMousePosition - offest;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                transform.position =  new Vector3((int)(currentMousePosition.x - offest.x), (int)(currentMousePosition.y - offest.y), (int)(currentMousePosition.z - offest.z));
            }
        }
    }
}