using Assets.Scripts.Topology;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.InputSystem
{
    public class InputServise : MonoBehaviour
    {
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

        private void Update()
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();

            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (var go in raycastResults)
                {
                    OnGameObjectSelected?.Invoke(go.gameObject);

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
    }
}