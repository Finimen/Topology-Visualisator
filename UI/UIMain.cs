/*               
            ░███████╗██╗███╗░░██╗██╗██╗░░░░░██╗███████╗███╗░░██╗   ░██████╗███╗░░██╗██╗██████╗░███████╗██████╗░░
			░██╔════╝██║████╗░██║██║████░░████║██╔════╝████╗░██║   ██╔════╝████╗░██║██║██╔══██╗██╔════╝██╔══██╗░
			░███████╗██║██╔██╗██║██║██║░██░░██║█████╗░░██╔██╗██║   ╚█████╗░██╔██╗██║██║██████╔╝█████╗░░██████╔╝░
			░██╔════╝██║██║╚████║██║██║░░░░░██║██╔══╝░░██║╚████║   ░╚═══██╗██║╚████║██║██╔═══╝░██╔══╝░░██╔══██╗░
			░██║░░░░░██║██║░╚███║██║██║░░░░░██║███████╗██║░╚███║   ██████╔╝██║░╚███║██║██║░░░░░███████╗██║░░██║░
			░╚═╝░░░░░╚═╝╚═╝░░╚══╝╚═╝╚═╝░░░░░╚═╝╚══════╝╚═╝░░╚══╝   ╚═════╝░╚═╝░░╚══╝╚═╝╚═╝░░░░░╚══════╝╚═╝░░╚═╝░
____________________________________________________________________________________________________________________________________________
                █▀▀▄ █──█ 　 ▀▀█▀▀ █──█ █▀▀ 　 ░█▀▀▄ █▀▀ ▀█─█▀ █▀▀ █── █▀▀█ █▀▀█ █▀▀ █▀▀█ 
                █▀▀▄ █▄▄█ 　 ─░█── █▀▀█ █▀▀ 　 ░█─░█ █▀▀ ─█▄█─ █▀▀ █── █──█ █──█ █▀▀ █▄▄▀ 
                ▀▀▀─ ▄▄▄█ 　 ─░█── ▀──▀ ▀▀▀ 　 ░█▄▄▀ ▀▀▀ ──▀── ▀▀▀ ▀▀▀ ▀▀▀▀ █▀▀▀ ▀▀▀ ▀─▀▀
____________________________________________________________________________________________________________________________________________
*/
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Robots.MainCore.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    internal class UIMain : MonoBehaviour
    {
        [SerializeField] private GameObject[] uiToActive;

        [SerializeField] private Transform moveToTransform;

        [SerializeField] private float yPosition;

        [SerializeField] private float timeScale = .5f;

        [SerializeField] private bool hideLast = true;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void SetActiveFalse(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        public void SetActiveTrue(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        public void PlayClip(AudioClip audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }

        public void SetCurrentUI(UIElement uIElement)
        {
            UIManager.Instance.UpdateCurrentUI(uIElement, timeScale , hideLast);
        }

        public void Instansate(GameObject gameObjectPrefab)
        {
            Instantiate(gameObjectPrefab);
        }

        public void UIArrayToActive(bool activeState)
        {
            foreach(GameObject ui in uiToActive)
            {
                ui.SetActive(activeState);
            }
        }

        public void HideGameObject(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }

        public void ShowCursor(bool locked)
        {
            Cursor.visible = true;

            SetCursorLockMode(locked);
        }

        public void HideCursor(bool locked)
        {
            Cursor.visible = false;

            SetCursorLockMode(locked);
        }

        private void SetCursorLockMode(bool locked)
        {
            if (locked)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }

        public void ShowGameObject(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }

        public void DisableMonoBehaviour(MonoBehaviour monoBehaviour)
        {
            monoBehaviour.enabled = false;
        }

        public void EnableMonoBehaviour(MonoBehaviour monoBehaviour)
        {
            monoBehaviour.enabled = true;
        }

        public void LoadSceneIndex(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}