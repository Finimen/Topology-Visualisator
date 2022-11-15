using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Topology;
using Assets.Scripts.Controls;

namespace Assets.Scripts
{
    public class EditPanel : MonoBehaviour
    {
        public TopologyObject SelectedObject;

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

        [SerializeField] private SceneMover sceneMover;

        [SerializeField] private float offestY;

        private List<VariableContainer> spawnedVariableContainers = new List<VariableContainer>();
        private List<MethodContainer> spawnedMethodContainers = new List<MethodContainer>();

        private bool variablesIsActive;
        private bool methodsIsActive;

        private bool startRenaming;

        public void Select()
        {
            field.text = SelectedObject.Name;

            if (SelectedObject.GetComponent<Class>() != null)
            {
                selectedText.text = "Selected class:";
            }
            else if (SelectedObject.GetComponent<Interface>() != null)
            {
                selectedText.text = "Selected interface:";
            }

            sceneMover.enabled = true;

            variablesPosition.gameObject.SetActive(true);
            methodsPosition.gameObject.SetActive(true);

            scaleSlider.value = SelectedObject.transform.localScale.x;

            foreach(GameObject gameObject in objectSettings)
            {
                gameObject.SetActive(true);
            }

            SelectStateVariables();
            SelectStateVariables();
            SelectStateMethods();
            SelectStateMethods();

            FindObjectOfType<G_CUIColorPicker>().Color = SelectedObject.BlackgroundColor;
        }

        public void DeSelect()
        {
            selectedText.text = "Nothing selected";
            field.text = "";

            variablesPosition.gameObject.SetActive(false);
            methodsPosition.gameObject.SetActive(false);

            foreach (GameObject gameObject in objectSettings)
            {
                gameObject.SetActive(false);
            }

            sceneMover.enabled = true;
        }

        public void Delete()
        {
            if (SelectedObject)
            {
                Destroy(SelectedObject.gameObject);
            }
        }

        public void Rename(string name)
        {
            SelectedObject.Rename(name);
        }

        public void AddVariable(Variable variable)
        {
            SelectedObject.AddVariable(variable);
        }

        public void AddMethod(Method method)
        {
            SelectedObject.AddMethod(method);
        }

        public void SelectStateVariables()
        {
            variablesIsActive = !variablesIsActive;

            if (variablesIsActive)
            {
                foreach (var variableData in SelectedObject.Variables)
                {
                    VariableContainer variableUI = Instantiate(variablePrefab, transform);

                    variableUI.transform.position = new Vector3(variablesPosition.position.x, variablesPosition.position.y - offestY
                        * (spawnedVariableContainers.Count + 1), variablesPosition.position.z);

                    variableUI.transform.rotation = transform.rotation;

                    variableUI.ProtectAndName.text = variableData.ProtectType + " " + variableData.Type + " " + variableData.Name;

                    spawnedVariableContainers.Add(variableUI);
                }

                addVariableButton.gameObject.SetActive(true);

                if (SelectedObject.Variables.Count > 0)
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

            SelectedObject.transform.localScale = Vector3.one * scale;
        }

        public void SelectStateMethods()
        {
            methodsIsActive = !methodsIsActive;

            if (methodsIsActive)
            {
                foreach (var methodData in SelectedObject.Methods)
                {
                    MethodContainer methodUI = Instantiate(methodPrefab, transform);

                    methodUI.transform.position = new Vector3(methodsPosition.position.x, methodsPosition.position.y - offestY
                        * (spawnedMethodContainers.Count + 1), methodsPosition.position.z);

                    methodUI.transform.rotation = transform.rotation;

                    methodUI.ProtectAndName.text = methodData.ProtectType + " " + methodData.ReturnedType + " " + methodData.Name + " " + methodData.Arguments;

                    spawnedMethodContainers.Add(methodUI);
                }

                addMethodButton.gameObject.SetActive(true);

                if (SelectedObject.Methods.Count > 0)
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
            if (!SelectedObject)
            {
                return;
            }

            if (SelectedObject.VariablesUnchanged())
            {
                SelectStateVariables();
                SelectStateVariables();
            }

            if (SelectedObject.MethodsUnchanged())
            {
                SelectStateMethods();
                SelectStateMethods();
            }
        }
    }
}