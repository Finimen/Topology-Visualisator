using UnityEngine;

namespace Assets.Scripts.TweenTools
{
    public class LeanTweenBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform showed;
        [SerializeField] private Transform hidden;

        [SerializeField] private float time = 1;

        public void Show()
        {
            LeanTween.move(gameObject, showed, time);
        }

        public void Hide()
        {
            LeanTween.move(gameObject, hidden, time);
        }
    }
}