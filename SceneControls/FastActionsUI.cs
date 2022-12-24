using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.SceneControls
{
    internal class FastActionsUI : MonoBehaviour
    {
        [SerializeField] private Button copy;
        [SerializeField] private Button paste;

        [SerializeField] private FastActions fastActions;

        private void Update()
        {
            if (fastActions.CopiedObjectIsNull)
            {
                paste.interactable = false;
            }
            else
            {
                paste.interactable = true;
            }
        }
    }
}