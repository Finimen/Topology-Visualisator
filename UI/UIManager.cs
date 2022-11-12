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
using UnityEngine.UI;

namespace Robots.MainCore.UI
{
    [DisallowMultipleComponent]
    internal class UIManager : MonoBehaviour
    {
        [SerializeField] private Canvas canvasMain;

        [SerializeField] private UIElement currentUI;

        [Space(15)]
        [SerializeField] private Button defuatButtonPrefab;

        [Space(15)]
        [SerializeField] private Transform robotTypes;
        [SerializeField] private Transform robotModifications;
        [SerializeField] private Transform superWeapons;

        [SerializeField] private Transform allTeams;

        public static UIManager Instance;

        public Canvas CanvasMain { get { return canvasMain; } }

        public Button DefuatButtonPrefab { get { return defuatButtonPrefab; } }

        public Transform RobotTypes { get { return robotTypes; } }
        public Transform RobotModifications { get { return robotModifications; } }
        public Transform SuperWeapons { get { return superWeapons; } }
        public Transform AllTeams { get { return allTeams; } }

        private void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(this);
        }

        public void SpawnDeffuatButton(Transform parentTransform, string buttonText = "")
        {
            Button buttonClone = Instantiate(defuatButtonPrefab, parentTransform);

            if (buttonClone.GetComponentInChildren<Text>())
            {
                Text buttonCloneText = buttonClone.GetComponentInChildren<Text>();

                buttonCloneText.text = buttonText;
            }
        }

        public void UpdateCurrentUI(UIElement uIElement, float timeScale, bool hideLast = false)
        {
            if (currentUI && hideLast)
            {
                currentUI.HideUI(timeScale);
            }

            if (uIElement)
            {
                currentUI = uIElement;

                currentUI.ShowUI(timeScale);
            }
        }

        public void HideAll()
        {
            UpdateCurrentUI(null, .5f, true);
        }

        public void ShowAll()
        {
            UpdateCurrentUI(currentUI, .5f, true);
        }
    }
}