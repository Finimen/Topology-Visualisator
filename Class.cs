using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.Topology
{
    [ExecuteAlways]
    public class Class : TopologyObject
    {
        [SerializeField] private Transform variablesPosition;
        [SerializeField] private Transform methodsPosition;

        [SerializeField] private float offestY;

        [SerializeField] private Image blackgroundImage;

        [SerializeField] private Text nameText;

        [SerializeField] private string name;

        [SerializeField] private Color blackgroundColor;

        [SerializeField] private Variable[] variables;

        [SerializeField] private Method[] methods;

        [Header("Prefabs")]
        [SerializeField] private VariableContainer variablePrefab;

        [SerializeField] private MethodContainer methodPrefab;

        [SerializeField] private bool variablesIsActive;
        [SerializeField] private bool methodsIsActive;

        [SerializeField] private string classParrent;

        private List<VariableContainer> spawnedVariableContainers = new List<VariableContainer>();
        private List<MethodContainer> spawnedMethodContainers = new List<MethodContainer>();

        public Variable[] Variables
        {
            get
            {
                return variables;
            }
            set
            {
                variables.AddRange(value);
            }
        }

        public void Rename(string name)
        {
            this.nameText.text = name;
        }

        public void SelectStateVariables()
        {
            if(variables.Length == 0) 
            {
                UnityEngine.Debug.Log("VariablesIsEmpty");

                return;
            }

            variablesIsActive = !variablesIsActive;

            if (variablesIsActive)
            {
                foreach (var variableData in variables)
                {
                    VariableContainer variableUI = Instantiate(variablePrefab, transform);

                    variableUI.transform.position = new Vector3(variablesPosition.position.x, variablesPosition.position.y - offestY 
                        * (spawnedVariableContainers.Count + 1), variablesPosition.position.z);

                    variableUI.transform.rotation = transform.rotation;

                    variableUI.ProtectAndName.text = variableData.ProtectType + " " + variableData.Name;

                    spawnedVariableContainers.Add(variableUI);
                }

                methodsPosition.GetComponent<FollowTarget>().Target = spawnedVariableContainers[spawnedVariableContainers.Count - 1].transform;
            }
            else
            {
                for(int i = spawnedVariableContainers.Count; i > 0; i --)
                {
                    Destroy(spawnedVariableContainers[0].gameObject);

                    spawnedVariableContainers.Remove(spawnedVariableContainers[0]);
                }

                methodsPosition.GetComponent<FollowTarget>().Target = variablesPosition;
            }
        }

        public void SelectStateMethods()
        {
            if (methods.Length == 0)
            {
                UnityEngine.Debug.Log("MethodsIsEmpty");

                return;
            }

            methodsIsActive = !methodsIsActive;

            if (methodsIsActive)
            {
                foreach (var methodData in methods)
                {
                    MethodContainer methodUI = Instantiate(methodPrefab, transform);

                    methodUI.transform.position = new Vector3(methodsPosition.position.x, methodsPosition.position.y - offestY
                        * (spawnedMethodContainers.Count + 1), methodsPosition.position.z);

                    methodUI.transform.rotation = transform.rotation;

                    methodUI.ProtectAndName.text = methodData.ProtectType + " " + methodData.ReturnedType + " " + methodData.Name + methodData.Arguments;

                    if (spawnedMethodContainers.Count == 0)
                    {
                        methodUI.GetComponent<FollowTarget>().Target = methodsPosition;
                    }
                    else
                    {
                        methodUI.GetComponent<FollowTarget>().Target = spawnedMethodContainers[spawnedMethodContainers.Count - 1].transform;
                    }

                    spawnedMethodContainers.Add(methodUI);
                }
            }
            else
            {
                for (int i = spawnedMethodContainers.Count; i > 0; i--)
                {
                    Destroy(spawnedMethodContainers[0].gameObject);

                    spawnedMethodContainers.Remove(spawnedMethodContainers[0]);
                }
            }
        }

        private void Update()
        {
            nameText.text = name + " : " + classParrent;
            
            blackgroundImage.color = blackgroundColor;
        }
    }
}