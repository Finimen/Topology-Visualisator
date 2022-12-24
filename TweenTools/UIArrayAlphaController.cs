using UnityEngine;

namespace Assets.Scripts.TweenTools
{
    public class UIArrayAlphaController : MonoBehaviour
    {
        [SerializeField] private UIAlphaController[] uiColorControllers;

        [SerializeField] private bool autoGetChildrens;

        [SerializeField] private float alphaStart = 0f;

        protected float alpha;

        private void Awake()
        {
            if (autoGetChildrens)
            {
                uiColorControllers = GetComponentsInChildren<UIAlphaController>();
            }
        }

        private void Start()
        {
            SetArrayAlpha(alphaStart);
        }

        public void SetArrayAlpha(float alpha)
        {
            foreach (UIAlphaController colorController in uiColorControllers)
            {
                colorController.SetColorAlpha(alpha);
            }
        }

        public void ChangeAlpha()
        {
            alpha = alpha == 1 ? 0 : 1;

            foreach (UIAlphaController colorController in uiColorControllers)
            {
                colorController.SetColorAlpha(alpha);
            }
        }
    }
}