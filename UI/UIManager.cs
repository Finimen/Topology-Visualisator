using UnityEngine;

namespace Assets.Scripts.TweenTools
{
    [DisallowMultipleComponent]
    internal class UIManager : MonoBehaviour
    {
        [SerializeField] private Canvas canvasMain;

        [SerializeField] private LeanTweenBehaviour currentUI;

        public static UIManager Instance;

        public Canvas CanvasMain { get { return canvasMain; } }

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void UpdateCurrentUI(LeanTweenBehaviour uIElement, bool hideLast = false)
        {
            if (currentUI && hideLast)
            {
                currentUI.Hide();
            }

            if (uIElement)
            {
                currentUI = uIElement;

                currentUI.Show();
            }
        }

        public void HideAll()
        {
            UpdateCurrentUI(null, true);
        }

        public void ShowAll()
        {
            UpdateCurrentUI(currentUI, true);
        }
    }
}