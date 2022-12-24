using UnityEngine;
using Assets.Scripts.TweenTools;

namespace Assets.Scripts
{
    [RequireComponent(typeof(UIArrayAlphaController))]
    public class FastEditMenu : MonoBehaviour
    {
        public UIArrayAlphaController AlphaController { get; private set; }

        private void Awake()
        {
            AlphaController = GetComponent<UIArrayAlphaController>();
        }
    }
}