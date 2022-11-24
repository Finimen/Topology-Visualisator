using Assets.Scripts.InputSystem;
using Assets.Scripts.ReturnSystem;
using Assets.Scripts.Topology;
using UnityEngine;

namespace Assets.Scripts.SceneControls
{
    public class FastActions
    {
        private Transform canvas;

        private InputServise inputServise;

        private TopologyObject copiedObject;

        private TopologyObject selectedSceneObject;

        private KeyCode fastActionKey = KeyCode.LeftControl;

        private KeyCode returnKey = KeyCode.Z;
        private KeyCode copyKey = KeyCode.C;
        private KeyCode pasteKey = KeyCode.V;
        private KeyCode duplicateKey = KeyCode.D;

        public void Enable(InputServise inputServise, Transform canvas)
        {
            this.inputServise = inputServise;

            this.canvas = canvas;

            inputServise.OnKeyPressed += ValidateKey;
        }

        public void Disable()
        {
            inputServise.OnKeyPressed -= ValidateKey;
        }

        private void ValidateKey(KeyCode pressedKey)
        {
            if (pressedKey == KeyCode.Delete)
            {
                Delete();
            }

            if (!Input.GetKey(fastActionKey))
            {
                return;
            }

            if (pressedKey == returnKey)
            {
                Return();
            }
            else if (pressedKey == copyKey)
            {
                Copy();
            }
            else if(pressedKey == pasteKey)
            {
                Paste();
            }
            else if( pressedKey == duplicateKey)
            {
                Duplicate();
            }
        }

        private void Return()
        {
            UnityEngine.Debug.Log(nameof(Return));

            Undo.Return();
        }

        private void Copy()
        {
            if (inputServise.SelectedTopologyObject)
            {
                copiedObject = inputServise.SelectedTopologyObject;

                UnityEngine.Debug.Log(copiedObject.name);
            }

            UnityEngine.Debug.Log(nameof(Copy));
        }

        private void Paste()
        {
            if(copiedObject == null)
            {
                return;
            }

            TopologyObject topologyObject = MonoBehaviour.Instantiate(copiedObject, canvas);

            topologyObject.transform.position = inputServise.MousePosition;

            topologyObject.GetComponent<MoveableObject>().Setup(inputServise);

            UnityEngine.Debug.Log(nameof(Paste));
        }

        private void Duplicate()
        {
            if (inputServise.SelectedTopologyObject)
            {
                TopologyObject topologyObject = MonoBehaviour.Instantiate(inputServise.SelectedTopologyObject, canvas);

                topologyObject.transform.position = inputServise.MousePosition;

                topologyObject.GetComponent<MoveableObject>().Setup(inputServise);
            }

            UnityEngine.Debug.Log(nameof(Duplicate));
        }

        private void Delete()
        {
            //Undo.Record("defuat", MonoBehaviour.FindObjectOfType<ObjectFactory>().ObjectsLibary);

            if (inputServise.SelectedTopologyObject)
            {
                MonoBehaviour.Destroy(inputServise.SelectedTopologyObject.gameObject);
            }
        }
    }
}