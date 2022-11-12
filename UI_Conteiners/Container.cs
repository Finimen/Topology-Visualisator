using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Container : MonoBehaviour
    {
        [SerializeField] private Text protectAndName;

        public Text ProtectAndName
        {
            get
            {
                return protectAndName;
            }
            set
            {
                protectAndName = value;
            }
        }
    }
}