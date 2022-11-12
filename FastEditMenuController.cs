using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Topology;

namespace Assets.Scripts
{
    public class FastEditMenuController : MonoBehaviour
    {
        [SerializeField] private FastEditMenu editMenu;

        [SerializeField] private EditPanel editPanel;

        private bool menuOpened;

        public void Close()
        {
            menuOpened = false;

            editMenu.gameObject.SetActive(false);
        }

        private void Start()
        {
            editMenu.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) | Input.GetKeyDown(KeyCode.Escape))
            {
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                pointer.position = Input.mousePosition;

                List<RaycastResult> raycastResults = new List<RaycastResult>();

                EventSystem.current.RaycastAll(pointer, raycastResults);

                if (raycastResults.Count > 0)
                {
                    foreach (var go in raycastResults)
                    {
                        if (go.gameObject.GetComponentInParent<Class>())
                        {
                            editPanel.SelectedClass = go.gameObject.GetComponentInParent<Class>();

                            editPanel.Select();

                            FindObjectOfType<ObjectSelector>().selectedObject = go.gameObject.GetComponentInParent<Class>();

                            return;
                        }
                    }
                }

                menuOpened = false;

                editMenu.gameObject.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                pointer.position = Input.mousePosition;

                List<RaycastResult> raycastResults = new List<RaycastResult>();

                EventSystem.current.RaycastAll(pointer, raycastResults);

                if (raycastResults.Count > 0)
                {
                    foreach (var go in raycastResults)
                    {
                        if (go.gameObject.GetComponentInParent<Class>())
                        {
                            editPanel.SelectedClass = go.gameObject.GetComponentInParent<Class>();

                            SelectStateMenu();
                        }
                    }
                }
            }
        }

        private void SelectStateMenu()
        {
            menuOpened = !menuOpened;

            if (menuOpened)
            {
                editMenu.gameObject.SetActive(true);

                editMenu.transform.position = Input.mousePosition;
            }
            else
            {
                editMenu.gameObject.SetActive(false);
            }
        }
    }
}