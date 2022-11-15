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
            inputServise.OnGameObjectSelected += SelectAction;
        }

        private void OnDisable()
        {
            inputServise.OnGameObjectSelected -= SelectAction;
        }

        private void SelectAction(GameObject objectSelected)
        {
            if(!Input.GetKeyDown(KeyCode.Mouse1)) 
            {
                return;
            }

            if (objectSelected.gameObject.GetComponentInParent<TopologyObject>())
            {
                editPanel.SelectedObject = objectSelected.gameObject.GetComponentInParent<TopologyObject>();

                editPanel.Select();

                editMenu.gameObject.SetActive(true);

                editMenu.transform.position = Input.mousePosition;

                UnityEngine.Debug.Log("HAPPY_:D");

                return;
            }
            else if (objectSelected.gameObject.GetComponentInParent<FastEditMenu>() || objectSelected.gameObject.GetComponentInParent<EditPanel>())
            {
                return;
            }
            else if (objectSelected.gameObject.GetComponentInParent<AddWindow>())
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