using Assets.Scripts.Topology;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class MoveableObject : MonoBehaviour
    {
        private ObjectSelector selector;

        private Camera cameraMain;

        private Vector3 offest;

        private bool movingStarted;

        private void Awake()
        {
            cameraMain = FindObjectOfType<Camera>();

            selector = FindObjectOfType<ObjectSelector>();
        }

        private void Update()
        {
            if (!movingStarted && Input.GetKeyDown(KeyCode.Mouse0))
            {
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                pointer.position = Input.mousePosition;

                List<RaycastResult> raycastResults = new List<RaycastResult>();

                EventSystem.current.RaycastAll(pointer, raycastResults);

                if (raycastResults.Count > 0)
                {
                    foreach (var gameObject in raycastResults)
                    {
                        if (gameObject.gameObject.GetComponentInParent<MoveableObject>() == this)
                        {
                            selector.selectedObject = gameObject.gameObject.GetComponentInParent<TopologyObject>();

                            movingStarted = true;

                            offest = Input.mousePosition - transform.position;
                        }
                    }
                }
            }

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
        }
    }
}