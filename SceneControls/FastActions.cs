using Assets.Scripts.InputSystem;
using Assets.Scripts.ReturnSystem;
using Assets.Scripts.Topology;
using UnityEngine;

namespace Assets.Scripts.SceneControls
{
    public class FastActions : MonoBehaviour
    {
        private Transform canvas;

        private InputServise inputServise;

        private TopologyObject copiedObject;

        private TopologyObject selectedSceneObject;

        [SerializeField] private KeyCode fastActionKey = KeyCode.LeftControl;

        [SerializeField] private KeyCode returnKey = KeyCode.Z;
        [SerializeField] private KeyCode copyKey = KeyCode.C;
        [SerializeField] private KeyCode pasteKey = KeyCode.V;
        [SerializeField] private KeyCode duplicateKey = KeyCode.D;

        public bool CopiedObjectIsNull
        {
            get
            {
                if (copiedObject)
                {
                    return false;
                }

                return true;
            }
        }

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

        public void Copy()
        {
            if (inputServise.SelectedTopologyObject)
            {
                copiedObject = inputServise.SelectedTopologyObject;

                UnityEngine.Debug.Log(copiedObject.name);
            }

            UnityEngine.Debug.Log(nameof(Copy));
        }

        public void Paste()
        {
            if(copiedObject == null)
            {
                return;
            }

            TopologyObject topologyObject = Instantiate(copiedObject, canvas);

            topologyObject.transform.position = inputServise.MousePosition;

            topologyObject.GetComponent<MoveableObject>().Setup(inputServise);

            UnityEngine.Debug.Log(nameof(Paste));
        }

        private void Duplicate()
        {
            if (inputServise.SelectedTopologyObject)
            {
                TopologyObject topologyObject = Instantiate(inputServise.SelectedTopologyObject, canvas);

                topologyObject.transform.position = inputServise.MousePosition;

                topologyObject.GetComponent<MoveableObject>().Setup(inputServise);
            }

            UnityEngine.Debug.Log(nameof(Duplicate));
        }

        public void Delete()
        {
            //Undo.Record("defuat", FindObjectOfType<ObjectFactory>().ObjectsLibary);

            if (inputServise.SelectedTopologyObject)
            {
                inputServise.SelectedTopologyObject.Destroy();
            }
        }
    }
}