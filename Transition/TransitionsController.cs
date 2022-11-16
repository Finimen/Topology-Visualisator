using Assets.Scripts.InputSystem;
using Assets.Scripts.Topology;
using UnityEditor.SearchService;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class TransitionsController : MonoBehaviour
    {
        [SerializeField] private Transition transitionPrefab;
        [SerializeField] private Transform canvas;

        [Inject] private InputServise inputServise;

        private Transition transition;

        public void CreateTransition()
        {
            transition = Instantiate(transitionPrefab, canvas);

            transition.StartPosition.position = Input.mousePosition;

            transition.StartPosition = inputServise.SelectedTopologyObject.transform;
        }

        private void Select(TopologyObject objectSelected)
        {
            if (!Input.GetMouseButtonDown(0) || !transition)
            {
                return;
            }

            transition.EndPosition = objectSelected.transform;

            if (objectSelected.GetComponentInParent<Interface>())
            {
                transition.BlackgroundColor = (new Color(.01f, .01f, .85f));
            }

            if (objectSelected.GetComponentInParent<Class>())
            {
                transition.BlackgroundColor = (new Color(.01f, .85f, .01f));
            }

            transition.Spawn();

            transition = null;
        }

        private void OnEnable()
        {
            inputServise.OnTopologyObjectSelected += Select;
        }

        private void OnDisable()
        {
            inputServise.OnTopologyObjectSelected -= Select;
        }

        private void Update()
        {
            if (transition)
            {
                transition.EndPosition.position = Input.mousePosition;
            }
        }
    }
}