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
        public event Action OnMove;

        private InputServise inputServise;

        private TopologyObject current;

        private Vector3 offest;

        private bool movingStarted;

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
                if(inputServise.SelectedTopologyObject != current && inputServise.SelectedTopologyObject.IsMove)
                {
                    movingStarted = false;
                }

                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    Undo.Record("defuat", FindObjectOfType<ObjectFactory>().ObjectsLibary);

                    movingStarted = false;
                }

                Move(Input.mousePosition);

                current.IsMove = movingStarted;
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