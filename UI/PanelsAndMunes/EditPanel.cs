using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Topology;
using Assets.Scripts.Controls;
using Zenject;
using Assets.Scripts.InputSystem;
using UnityEditor.Rendering;

namespace Assets.Scripts
{
    public class EditPanel : MonoBehaviour
    {
        [SerializeField] private Text selectedText;
        [SerializeField] private Text scaleText;

        [SerializeField] private GameObject[] objectSettings;

        [SerializeField] private Slider scaleSlider;
        [SerializeField] private InputField field;

        [SerializeField] private FollowTarget addVariableButton;
        [SerializeField] private FollowTarget addMethodButton;
        [SerializeField] private FollowTarget otherSettings;

        [SerializeField] private Transform variablesPosition;
        [SerializeField] private Transform methodsPosition;

        [SerializeField] private VariableContainer variablePrefab;
        [SerializeField] private MethodContainer methodPrefab;

        [SerializeField] private float offestY;

        [Inject] private SceneController sceneController;
        [Inject] private InputServise inputServise;
        [Inject] private G_CUIColorPicker colorPicker;

        private List<VariableContainer> spawnedVariableContainers = new List<VariableContainer>();
        private List<MethodContainer> spawnedMethodContainers = new List<MethodContainer>();

        private ISceneObject selectedSceneObject;

        private bool variablesIsActive;
        private bool methodsIsActive;

        public void Select(Transform selected)
        {
            field.text = inputServise.SelectedTopologyObject.Name;

            if (!selected.TryGetComponent(out ISceneObject sceneObject))
            {
                return;
            }

            selectedSceneObject = sceneObject;

            if (selected.GetComponentInParent<Transition>() != null)
            {
                selectedText.text = "Selected transition:";

                field.gameObject.SetActive(false);

                otherSettings.transform.position = new Vector3(otherSettings.transform.position.x, selectedText.transform.position.y - offestY, otherSettings.transform.position.z);

                otherSettings.Target = selectedText.transform;

                variablesPosition.gameObject.SetActive(false);
                methodsPosition.gameObject.SetActive(false);
            }
            else if (selected.GetComponent<Class>() != null)
            {
                selectedText.text = "Selected class:";

                field.gameObject.SetActive(true);

                variablesPosition.gameObject.SetActive(true);
                methodsPosition.gameObject.SetActive(true);
            }
            else if (selected.GetComponent<Interface>() != null)
            {
                selectedText.text = "Selected interface:";

                variablesPosition.gameObject.SetActive(true);
                methodsPosition.gameObject.SetActive(true);
            }

            sceneController.enabled = true;

            scaleSlider.value = selected.transform.localScale.x;

            foreach(GameObject gameObject in objectSettings)
            {
                gameObject.SetActive(true);
            }

            SelectStateVariables();
            SelectStateVariables();
            SelectStateMethods();
            SelectStateMethods();

            colorPicker.Color = sceneObject.BlackgroundColor;
            colorPicker.SetSelectedObject(sceneObject);
        }

        private void DeSelect()
        {
            selectedText.text = "Nothing selected";
            field.text = "";

            variablesPosition.gameObject.SetActive(false);
            methodsPosition.gameObject.SetActive(false);

            foreach (GameObject gameObject in objectSettings)
            {
                gameObject.SetActive(false);
            }

            sceneController.enabled = true;
        }

        public void Delete()
        {
            selectedSceneObject.Destroy();
        }

        public void Rename(string name)
        {
            inputServise.SelectedTopologyObject.Rename(name);
        }

        public void AddVariable(Variable variable)
        {
            inputServise.SelectedTopologyObject.AddVariable(variable);
        }

        public void AddMethod(Method method)
        {
            inputServise.SelectedTopologyObject.AddMethod(method);
        }

        public void SelectStateVariables()
        {
            variablesIsActive = !variablesIsActive;

            if (variablesIsActive)
            {
                foreach (var variableData in inputServise.SelectedTopologyObject.Variables)
                {
                    VariableContainer variableUI = Instantiate(variablePrefab, transform);

                    variableUI.transform.position = new Vector3(variablesPosition.position.x, variablesPosition.position.y - offestY
                        * (spawnedVariableContainers.Count + 1), variablesPosition.position.z);

                    variableUI.transform.rotation = transform.rotation;

                    variableUI.ProtectAndName.text = variableData.ProtectType + " " + variableData.Type + " " + variableData.Name;

                    spawnedVariableContainers.Add(variableUI);
                }

                addVariableButton.gameObject.SetActive(true);

                if (inputServise.SelectedTopologyObject.Variables.Count > 0)
                {
                    addVariableButton.transform.position = new Vector3(spawnedVariableContainers[spawnedVariableContainers.Count - 1].transform.position.x, spawnedVariableContainers[spawnedVariableContainers.Count - 1].transform.position.y - offestY
                        * (spawnedVariableContainers.Count), spawnedVariableContainers[spawnedVariableContainers.Count - 1].transform.position.z);
                
                    addVariableButton.Target = spawnedVariableContainers[spawnedVariableContainers.Count - 1].transform;
                }
                else
                {
                    addVariableButton.transform.position = new Vector3(variablesPosition.position.x, variablesPosition.position.y - offestY, variablesPosition.position.z);

                    addVariableButton.Target = variablesPosition;
                }

                methodsPosition.GetComponent<FollowTarget>().Target = addVariableButton.transform;
            }
            else
            {
                for (int i = spawnedVariableContainers.Count; i > 0; i--)
                {
                    Destroy(spawnedVariableContainers[0].gameObject);

                    spawnedVariableContainers.Remove(spawnedVariableContainers[0]);
                }

                methodsPosition.GetComponent<FollowTarget>().Target = variablesPosition;

                addVariableButton.gameObject.SetActive(false);
            }
        }

        public void SetScale(float scale)
        {
            scaleText.text = "Scale: " + scale;

            selectedSceneObject.SetScale(scale);
        }

        public void SelectStateMethods()
        {
            methodsIsActive = !methodsIsActive;

            if (methodsIsActive)
            {
                foreach (var methodData in inputServise.SelectedTopologyObject.Methods)
                {
                    MethodContainer methodUI = Instantiate(methodPrefab, transform);

                    methodUI.transform.position = new Vector3(methodsPosition.position.x, methodsPosition.position.y - offestY
                        * (spawnedMethodContainers.Count + 1), methodsPosition.position.z);

                    methodUI.transform.rotation = transform.rotation;

                    methodUI.ProtectAndName.text = methodData.ProtectType + " " + methodData.ReturnedType + " " + methodData.Name + " " + methodData.Arguments;

                    spawnedMethodContainers.Add(methodUI);
                }

                addMethodButton.gameObject.SetActive(true);

                if (inputServise.SelectedTopologyObject.Methods.Count > 0)
                {
                    addMethodButton.transform.position = new Vector3(spawnedMethodContainers[spawnedMethodContainers.Count - 1].transform.position.x, spawnedMethodContainers[spawnedMethodContainers.Count - 1].transform.position.y - offestY
                        * (spawnedMethodContainers.Count), spawnedMethodContainers[spawnedMethodContainers.Count - 1].transform.position.z);

                    addMethodButton.Target = spawnedMethodContainers[spawnedMethodContainers.Count - 1].transform;
                }
                else
                {
                    addMethodButton.transform.position = new Vector3(methodsPosition.position.x, methodsPosition.position.y - offestY, methodsPosition.position.z);

                    addMethodButton.Target = methodsPosition;
                }

                otherSettings.Target = addMethodButton.transform;
            }
            else
            {
                for (int i = spawnedMethodContainers.Count; i > 0; i--)
                {
                    Destroy(spawnedMethodContainers[0].gameObject);

                    spawnedMethodContainers.Remove(spawnedMethodContainers[0]);
                }

                if (variablesIsActive)
                {
                    methodsPosition.GetComponent<FollowTarget>().Target = addVariableButton.transform;
                }
                else
                {
                    methodsPosition.GetComponent<FollowTarget>().Target = variablesPosition;
                }

                addMethodButton.gameObject.SetActive(false);

                otherSettings.Target = methodsPosition;
            }
        }

        private void OnEnable()
        {
            //inputServise.OnVoidSelected += DeSelect;
        }

        private void OnDisable()
        {
            //inputServise.OnVoidSelected -= DeSelect;
        }

        private void Start()
        {
            variablesPosition.gameObject.SetActive(false);
            methodsPosition.gameObject.SetActive(false);

            foreach (GameObject gameObject in objectSettings)
            {
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (selectedSceneObject == null)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }

            if (inputServise.SelectedTopologyObject.VariablesUnchanged())
            {
                SelectStateVariables();
                SelectStateVariables();
            }

            if (inputServise.SelectedTopologyObject.MethodsUnchanged())
            {
                SelectStateMethods();
                SelectStateMethods();
            }
        }
    }
}