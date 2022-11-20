using Assets.Scripts.InputSystem;
using Assets.Scripts.SaveSystem;
using Assets.Scripts.Topology;
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
        }

        public void CreateInterface()
        {
            Interface newInterface = Instantiate(interfacePrefab, sceneCanvas.transform);

            newInterface.transform.position = new Vector3(cameraMain.transform.position.x + Random.Range(-5, 5), cameraMain.transform.position.y + Random.Range(-5, 5), 10);

            newInterface.GetComponent<MoveableObject>().Setup(inputServise);

            objectsLibary.Add(newInterface);
        }

        public void CreateTransition()
        {
            transition = Instantiate(transitionPrefab, canvas);

            transition.StartPosition.position = Input.mousePosition;

            transition.StartPosition = inputServise.SelectedTopologyObject.transform;

            objectsLibary.Add(transition);
        }

        private void Select(TopologyObject objectSelected)
        {
            if (!Input.GetMouseButtonDown(0) || !transition)
            {
                return;
            }

            transition.EndPosition = objectSelected.transform;

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

            if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                FindObjectOfType<SaveManager>().OnSave(objectsLibary.Classes, objectsLibary.Transitions);
            }

            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                foreach (Class @class in objectsLibary.Classes)
                {
                    if(@class != null)
                    {
                        Destroy(@class.gameObject);
                    }
                }

                foreach (Transition transition in objectsLibary.Transitions)
                {
                    if (transition != null)
                    {
                        Destroy(transition.gameObject);
                    }
                }

                FindObjectOfType<SaveManager>().OnLoad(objectsLibary.Classes, objectsLibary.Transitions);
            }
        }
    }
}