using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Topology
{
    [ExecuteAlways]
    public class Interface : TopologyObject
    {
        [Header("Prefabs")]
        [SerializeField] private VariableContainer variablePrefab;
        [SerializeField] private MethodContainer methodPrefab;

        [SerializeField] private bool variablesIsActive;
        [SerializeField] private bool methodsIsActive;

        [SerializeField] private string classParrent;

        private List<VariableContainer> spawnedVariableContainers = new List<VariableContainer>();
        private List<MethodContainer> spawnedMethodContainers = new List<MethodContainer>();

        public override List<Variable> Variables
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

        public override List<Method> Methods
        {
            get
            {
                return methods;
            }
            set
            {
                methods.AddRange(value);
            }
        }

        public override void SelectStateVariables()
        {
            variablesIsActive = !variablesIsActive;

            SelectStateVariables(variablesIsActive);
        }

        public void SelectStateVariables(bool state)
        {
            if (variables.Count == 0)
            {
                UnityEngine.Debug.Log("VariablesIsEmpty");

                return;
            }

            if (state)
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
                for (int i = spawnedVariableContainers.Count; i > 0; i--)
                {
                    Destroy(spawnedVariableContainers[0].gameObject);

                    spawnedVariableContainers.Remove(spawnedVariableContainers[0]);
                }

                methodsPosition.GetComponent<FollowTarget>().Target = variablesPosition;
            }
        }

        public override void SelectStateMethods()
        {
            if (methods.Count == 0)
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

        public override bool VariablesUnchanged()
        {
            if (spawnedVariableContainers.Count != variables.Count)
            {
                return true;
            }

            return false;
        }

        public override bool MethodsUnchanged()
        {
            if (spawnedMethodContainers.Count != methods.Count)
            {
                return true;
            }

            return false;
        }

        private void Update()
        {
            nameText.text = name + " : " + classParrent;

            blackgroundImage.color = blackgroundColor;
        }
    }
}