using Assets.Scripts.Topology;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    public class TransitionsController : MonoBehaviour
    {
        [SerializeField] private Transition transitionPrefab;
        [SerializeField] private Transform canvas;

        private Transition transition;

        private ObjectSelector objectSelector;

        public void CreateTransition()
        {
            transition =  Instantiate(transitionPrefab, canvas);

            transition.StartPosition.position = Input.mousePosition;

            transition.StartPosition = objectSelector.selectedObject.transform;
        }

        private void Start()
        {
            objectSelector = FindObjectOfType<ObjectSelector>();
        }

        private void Update()
        {
            if (transition)
            {
                transition.EndPosition.position = Input.mousePosition;

                if(Input.GetMouseButtonDown(0))
                {
                    PointerEventData pointer = new PointerEventData(EventSystem.current);
                    pointer.position = Input.mousePosition;

                    List<RaycastResult> raycastResults = new List<RaycastResult>();

                    EventSystem.current.RaycastAll(pointer, raycastResults);

                    if (raycastResults.Count > 0)
                    {
                        foreach (var go in raycastResults)
                        {
                            if (go.gameObject.GetComponentInParent<TopologyObject>())
                            {
                                transition.EndPosition = go.gameObject.GetComponentInParent<TopologyObject>().transform;

                                if(go.gameObject.GetComponentInParent<Interface>())
                                {
                                    transition.SetColor(new Color(.01f,.01f,.85f));
                                }

                                if (go.gameObject.GetComponentInParent<Class>())
                                {
                                    transition.SetColor(new Color(.01f, .85f, .01f));
                                }

                                transition.Spawn();

                                transition = null;

                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}