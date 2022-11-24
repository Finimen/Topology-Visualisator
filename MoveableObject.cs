using Assets.Scripts.InputSystem;
using Assets.Scripts.Topology;
using UnityEngine;
using System;
using Assets.Scripts.ReturnSystem;

namespace Assets.Scripts
{
    [RequireComponent(typeof(TopologyObject))]
    public class MoveableObject : MonoBehaviour
    {
        public Action OnMove;

        private InputServise inputServise;

        private TopologyObject current;

        private Camera cameraMain;

        private Vector3 offest;

        private bool movingStarted;

        public void Initialize(Camera camera)
        {
            cameraMain = camera;
        }

        public void Setup(InputServise inputServise)
        {
            current = GetComponent<TopologyObject>();

            this.inputServise = inputServise;

            inputServise.OnTopologyObjectSelected += Select;
        }

        private void OnDisable()
        {
            inputServise.OnTopologyObjectSelected -= Select;
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
                    Undo.Record("defuat", FindObjectOfType<ObjectFactory>().ObjectsLibary);
                        
                    movingStarted = false;
                }
            }
        }

        private void Move(Vector3 currentMousePosition)
        {
            transform.position = currentMousePosition - offest;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                OnMove?.Invoke();

                transform.position =  new Vector3((int)(currentMousePosition.x - offest.x), (int)(currentMousePosition.y - offest.y), (int)(currentMousePosition.z - offest.z));
            }
        }
    }
}