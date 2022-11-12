using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Topology;

namespace Assets.Scripts
{
    public class EditPanel : MonoBehaviour
    {
        public Class SelectedClass;

        [SerializeField] private float offestY;

        [SerializeField] private FollowTarget addVariableButton;

        [SerializeField] private Transform variablesPosition;
        [SerializeField] private Transform methodsPosition;

        [SerializeField] private VariableContainer variablePrefab;

        [SerializeField] private Text selectedText;

        private List<VariableContainer> spawnedVariableContainers = new List<VariableContainer>();

        private StringBuilder newName;

        private bool variablesIsActive;

        private bool startRenaming;

        public void Select()
        {
            selectedText.text = SelectedClass.name;
        }

        public void Deselect()
        {
            selectedText.text = "null";
        }

        public void Rename()
        {
            UnityEngine.Debug.Log("Renaming");

            newName = new StringBuilder();

            startRenaming = true;
        }

        private void OnGUI()
        {
            if (!startRenaming)
            {
                return;
            }

            if (Event.current.isKey)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    UnityEngine.Debug.Log(newName.ToString());

                    SelectedClass.Rename(newName.ToString());

                    startRenaming = false;
                }
                else
                {
                    newName.AppendLine(Event.current.keyCode.ToString());
                }
            }
        }

        public void AddVariable()
        {

        }

        public void SelectStateVariables()
        {
            variablesIsActive = !variablesIsActive;

            if (variablesIsActive)
            {
                foreach (var variableData in SelectedClass.Variables)
                {
                    VariableContainer variableUI = Instantiate(variablePrefab, transform);

                    variableUI.transform.position = new Vector3(variablesPosition.position.x, variablesPosition.position.y - offestY
                        * (spawnedVariableContainers.Count + 1), variablesPosition.position.z);

                    variableUI.transform.rotation = transform.rotation;

                    variableUI.ProtectAndName.text = variableData.ProtectType + " " + variableData.Name;

                    spawnedVariableContainers.Add(variableUI);
                }

                addVariableButton.gameObject.SetActive(true);

                if (SelectedClass.Variables.Length > 0)
                {
                    addVariableButton.transform.position = new Vector3(spawnedVariableContainers[spawnedVariableContainers.Count - 1].transform.position.x, spawnedVariableContainers[spawnedVariableContainers.Count - 1].transform.position.y - offestY
                        * (spawnedVariableContainers.Count), spawnedVariableContainers[spawnedVariableContainers.Count - 1].transform.position.z);
                
                    addVariableButton.Target = spawnedVariableContainers[spawnedVariableContainers.Count - 1].transform;
                }
                else
                {
                    addVariableButton.transform.position = new Vector3(variablesPosition.position.x, variablesPosition.position.y, variablesPosition.position.z);

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
    }
}