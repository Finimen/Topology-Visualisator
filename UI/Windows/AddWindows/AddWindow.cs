using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Assets.Scripts.Windows
{
    public abstract class AddWindow : MonoBehaviour
    {
        public ProtectType ProtectType;

        [SerializeField] private Dropdown protect;

        private List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        protected void SetupDropdownMenu()
        {
            for (int i = 0; i < Enum.GetValues(typeof(ProtectType)).Length; i++)
            {
                ProtectType protectType = (ProtectType)i;

                options.Add(new Dropdown.OptionData(protectType.ToString()));
            }

            protect.ClearOptions();

            protect.AddOptions(options);
        }

        public void SetProtect()
        {
            if (Enum.TryParse(options[protect.value].text, out ProtectType protectType))
            {
                ProtectType = protectType;
            }
        }

        public abstract void Add();
    }
}