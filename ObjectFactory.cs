using Assets.Scripts.InputSystem;
using Assets.Scripts.ReturnSystem;
using Assets.Scripts.SaveSystem;
using Assets.Scripts.Topology;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class ObjectFactory : MonoBehaviour
    {
        [SerializeField] private Class classPrefab;
        [SerializeField] private Interface interfacePrefab;
        [SerializeField] private Transform sceneCanvas;

        [SerializeField] private Transition transitionPrefab;
        [SerializeField] private Transform canvas;

        [Inject] private InputServise inputServise;

        private Transition transition;

        private ObjectsLibary objectsLibary;

        private Camera cameraMain;

        public ObjectsLibary ObjectsLibary
        {
            get 
            { 
                return objectsLibary;
            }
        }

        public void Initialize(Camera camera)
        {
            objectsLibary = new ObjectsLibary();

            objectsLibary.Initialize(100);

            cameraMain = camera;
        }

        public void CreateClass()
        {
            Class newClass = Instantiate(classPrefab, sceneCanvas.transform);

            newClass.transform.position = new Vector3(cameraMain.transform.position.x + Random.Range(-5,5),cameraMain.transform.position.y + Random.Range(-5, 5), 10);

            newClass.GetComponent<MoveableObject>().Setup(inputServise);

            objectsLibary.Add(newClass);

            Undo.Record("default", objectsLibary);
        }

        public void CreateInterface()
        {
            Interface newInterface = Instantiate(interfacePrefab, sceneCanvas.transform);

            newInterface.transform.position = new Vector3(cameraMain.transform.position.x + Random.Range(-5, 5), cameraMain.transform.position.y + Random.Range(-5, 5), 10);

            newInterface.GetComponent<MoveableObject>().Setup(inputServise);

            objectsLibary.Add(newInterface);

            Undo.Record("default", objectsLibary);
        }

        public void CreateTransition()
        {
            transition = Instantiate(transitionPrefab, canvas);

            transition.StartPosition.position = Input.mousePosition;

            transition.StartPosition = inputServise.SelectedTopologyObject.GetComponent<RectTransform>();

            objectsLibary.Add(transition);

            Undo.Record("default", objectsLibary);
        }

        private void Select(TopologyObject objectSelected)
        {
            if (!Input.GetMouseButtonDown(0) || !transition)
            {
                return;
            }

            transition.EndPosition = objectSelected.GetComponent<RectTransform>();

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