using UnityEngine;
using Assets.Scripts.Topology;
using Assets.Scripts.Windows;
using Assets.Scripts.InputSystem;
using Zenject;

namespace Assets.Scripts
{
    public class FastEditMenuController : MonoBehaviour
    {
        [SerializeField] private FastEditMenu editMenu;

        [SerializeField] private EditPanel editPanel;

        [Inject] private InputServise inputServise;

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

        private void OnEnable()
        {
            inputServise.OnGameObjectSelected += Select;
        }

        private void OnDisable()
        {
            inputServise.OnGameObjectSelected -= Select;
        }

        private void Select(GameObject objectSelected)
        {
            if(!Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKeyDown(KeyCode.Mouse0))
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && objectSelected.GetComponentInParent<Transition>())
            {
                editPanel.Select(objectSelected.GetComponentInParent<Transition>().transform);

                UnityEngine.Debug.Log("Transition");
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0) && objectSelected.GetComponentInParent<TopologyObject>())
            {
                editPanel.Select(objectSelected.GetComponentInParent<TopologyObject>().transform);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1) && objectSelected.GetComponentInParent<TopologyObject>())
            {
                editPanel.Select(objectSelected.GetComponentInParent<TopologyObject>().transform);

                editMenu.gameObject.SetActive(true);

                editMenu.transform.position = Input.mousePosition;

                UnityEngine.Debug.Log("HAPPY_:D");

                return;
            }
            else if (objectSelected.GetComponentInParent<FastEditMenu>() || objectSelected.GetComponentInParent<EditPanel>())
            {
                return;
            }
            else if (objectSelected.GetComponentInParent<AddWindow>())
            {
                return;
            }

            editMenu.gameObject.SetActive(false);
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