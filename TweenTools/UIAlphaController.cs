using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.TweenTools
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ILayoutElement))]
    public class UIAlphaController : MonoBehaviour
    {
        [SerializeField] private float lerpTime = .02f;

        [SerializeField] private float currentColorAlpha;

        [SerializeField] private bool disableObjectHidden;

        private Text currentText;

        private Button currentButton;

        private Image currentImage;

        private void Awake()
        {
            if (GetComponent<Button>())
            {
                currentButton = GetComponent<Button>();

                currentButton.image.color = new Color(currentButton.image.color.r, currentButton.image.color.g, currentButton.image.color.b, currentColorAlpha);
            }

            if (GetComponent<Text>())
            {
                currentText = GetComponent<Text>();

                currentText.color = new Color(currentText.color.r, currentText.color.g, currentText.color.b, currentColorAlpha);
            }

            if (GetComponent<Image>())
            {
                currentImage = GetComponent<Image>();

                currentImage.color = new Color(currentImage.color.r, currentImage.color.g, currentImage.color.b, currentColorAlpha);
            }

            if (!currentText & !currentImage & !currentButton)
            {
                Destroy(this);
            }
        }

        private void Update()
        {
            if (currentButton)
            {
                currentButton.image.color = Color.Lerp(currentButton.image.color, new Color(currentButton.image.color.r, currentButton.image.color.g, currentButton.image.color.b, currentColorAlpha), lerpTime);

                if (disableObjectHidden)
                {
                    currentButton.enabled = currentButton.image.color.a > .05f ? true : false;
                }
            }
            else if (currentImage)
            {
                currentImage.color = Color.Lerp(currentImage.color, new Color(currentImage.color.r, currentImage.color.g, currentImage.color.b, currentColorAlpha), lerpTime);

                if (disableObjectHidden)
                {
                    gameObject.SetActive(currentImage.color.a > .05f ? true : false);
                }
            }
            else if (currentText)
            {
                currentText.color = Color.Lerp(currentText.color, new Color(currentText.color.r, currentText.color.g, currentText.color.b, currentColorAlpha), lerpTime);

                if (disableObjectHidden)
                {
                    currentText.raycastTarget = currentText.color.a > .05f ? true : false;
                }
            }
        }

        public void SetColorAlpha(float colorAlpha)
        {
            colorAlpha = Mathf.Clamp01(colorAlpha);

            currentColorAlpha = colorAlpha;
        }
    }
}