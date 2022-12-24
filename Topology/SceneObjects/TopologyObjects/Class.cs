using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Topology
{
    [ExecuteAlways] 
    [Serializable]
    public class Class : TopologyObject
    {
        [Header("Prefabs")]
        [SerializeField] private VariableContainer variablePrefab;

        [SerializeField] private MethodContainer methodPrefab;

        [SerializeField] private bool variablesIsActive;
        [SerializeField] private bool methodsIsActive;

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

            SetStateVariables(variablesIsActive);
        }

        protected void SetStateVariables(bool state)
        {
            float currentOffest = offest * transform.localScale.x;

            if (variables.Count == 0)
            {
                UnityEngine.Debug.Log("VariablesIsEmpty");

                return;
            }

            if (state)
            {
                foreach (var variableData in variables)
                {
                    VariableContainer variableUI = Instantiate(variablePrefab, group);

                    variableUI.ProtectAndName.text = $"{variableData.ProtectType} {variableData.Type} {variableData.Name}";

                    variableUI.transform.SetSiblingIndex(variablesPosition.GetSiblingIndex() + 1);

                    spawnedVariableContainers.Add(variableUI);
                }
            }
            else
            {
                for (int i = spawnedVariableContainers.Count; i > 0; i--)
                {
                    Destroy(spawnedVariableContainers[0].gameObject);

                    spawnedVariableContainers.Remove(spawnedVariableContainers[0]);
                }
            }
        }

        public override void SelectStateMethods()
        {
            methodsIsActive = !methodsIsActive;

            SetStateMethods(methodsIsActive);
        }

        public void SetStateMethods(bool state)
        {
            float currentOffest = offest * transform.localScale.x;

            if (methods.Count == 0)
            {
                UnityEngine.Debug.Log("MethodsIsEmpty");

                return;
            }

            if (state)
            {
                foreach (var methodData in methods)
                {
                    MethodContainer methodUI = Instantiate(methodPrefab, group);

                    methodUI.ProtectAndName.text = $"{methodData.ProtectType} {methodData.ReturnedType} {methodData.Name} {methodData.Arguments}";

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
            nameText.text = name + " : " + parrent;

            typeText.text = "class";

            blackgroundImage.color = blackgroundColor;

            if(variablesIsActive && spawnedVariableContainers.Count != variables.Count)
            {
                SelectStateVariables();
                SelectStateVariables();
            }
        }
    }
}