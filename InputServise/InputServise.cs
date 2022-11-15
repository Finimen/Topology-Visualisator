using Assets.Scripts.Topology;
using System;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.InputSystem
{
    public class InputServise : MonoBehaviour
    {
        public Action<ISceneObject> OnISceneObjectSelected;
        public Action<TopologyObject> OnTopologyObjectSelected;
        public Action<GameObject> OnGameObjectSelected;

        [SerializeField] private KeyCode mouse0;
        [SerializeField] private KeyCode mouse1;

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

                    if (go.gameObject.TryGetComponent<ISceneObject>(out ISceneObject sceneObject))
                    {
                        OnISceneObjectSelected?.Invoke(sceneObject);
                    }

                    if (go.gameObject.GetComponentInParent<TopologyObject>())
                    {
                        OnTopologyObjectSelected?.Invoke(go.gameObject.GetComponentInParent<TopologyObject>());
                    }
                }
            }
        }
    }
}
