using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Debug
{
    public class DebugCurrentElementFinder : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                pointer.position = Input.mousePosition;

                List<RaycastResult> raycastResults = new List<RaycastResult>();

                EventSystem.current.RaycastAll(pointer, raycastResults);

                if (raycastResults.Count > 0)
                {
                    foreach (var go in raycastResults)
                    {
                        UnityEngine.Debug.Log(go.gameObject.name, go.gameObject);
                    }
                }
            }
        }
    }
}