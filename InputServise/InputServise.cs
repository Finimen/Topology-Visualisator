using Assets.Scripts.Topology;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Tayx.Graphy.GraphyManager;

namespace Assets.Scripts.InputSystem
{
    public class InputServise : MonoBehaviour
    {
        public Action<KeyCode> OnKeyPressed;
        
        public Action<ISceneObject> OnISceneObjectSelected;
        
        public Action<TopologyObject> OnTopologyObjectSelected;
        public Action<GameObject> OnGameObjectSelected;

        public Action OnVoidSelected;

        [SerializeField] private KeyCode mouse0;
        [SerializeField] private KeyCode mouse1;

        public TopologyObject SelectedTopologyObject
        {
            get;
            private set; 
        }

        public GameObject SelectedGameObject
        {
            get;
            private set;
        }

        public Vector3 MousePosition
        {
            get;
            private set; 
        }

        private void Update()
        {
            MousePosition = Input.mousePosition;

            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();

            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (var go in raycastResults)
                {
                    OnGameObjectSelected?.Invoke(go.gameObject);

                    SelectedGameObject = go.gameObject;

                    if (go.gameObject.TryGetComponent(out ISceneObject sceneObject))
                    {
                        OnISceneObjectSelected?.Invoke(sceneObject);
                    }

                    if (go.gameObject.GetComponentInParent<TopologyObject>())
                    {
                        OnTopologyObjectSelected?.Invoke(go.gameObject.GetComponentInParent<TopologyObject>());

                        SelectedTopologyObject = go.gameObject.GetComponentInParent<TopologyObject>();
                    }
                    else if (Input.GetMouseButton(0) && SelectedTopologyObject)
                    {
                        OnVoidSelected?.Invoke();
                    }
                }
            }
        }

        private void OnGUI()
        {
            if (Event.current.isKey)
            {
                OnKeyPressed?.Invoke(Event.current.keyCode);
            }
        }
    }
}