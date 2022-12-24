using UnityEngine;
using Assets.Scripts.Topology;
using Assets.Scripts.Windows;
using Assets.Scripts.InputSystem;
using Zenject;
using System.Collections;
using Unity.VisualScripting;

namespace Assets.Scripts
{
    public class FastEditMenuController : MonoBehaviour
    {
        [SerializeField] private FastEditMenu editMenu;

        [SerializeField] private EditPanel editPanel;

        [Inject] private InputServise inputServise;

        private bool menuOpened;

        private void Start()
        {
            editMenu.AlphaController.SetArrayAlpha(0);
        }

        private void OnEnable()
        {
            inputServise.OnGameObjectSelected += Select;
            inputServise.OnKeyPressed += OnKeyPressed;
        }

        private void OnDisable()
        {
            inputServise.OnGameObjectSelected -= Select;
            inputServise.OnKeyPressed -= OnKeyPressed;
        }

        private void OnKeyPressed(KeyCode pressedKey)
        {
            if (pressedKey != KeyCode.Mouse0 || pressedKey != KeyCode.Mouse1)
            {
                editMenu.AlphaController.SetArrayAlpha(0);
            }
        }

        private void Select(GameObject objectSelected)
        {
            if (!Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKeyDown(KeyCode.Mouse0))
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

                StartCoroutine(ShowEditMenuWithDelay());

                return;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1) && objectSelected.GetComponentInParent<TopologyObject>())
            {
                editPanel.Select(objectSelected.GetComponentInParent<TopologyObject>().transform);

                editMenu.AlphaController.SetArrayAlpha(1);

                editMenu.transform.position = Input.mousePosition;

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

            editMenu.AlphaController.SetArrayAlpha(0);
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

        private IEnumerator ShowEditMenuWithDelay()
        {
            yield return new WaitForSeconds(1);

            editMenu.AlphaController.SetArrayAlpha(0);
        }
    }
}