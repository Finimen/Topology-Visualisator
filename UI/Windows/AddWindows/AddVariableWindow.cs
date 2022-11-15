using UnityEngine;

namespace Assets.Scripts.Windows
{
    public class AddVariableWindow : AddWindow
    {
        [SerializeField] private string Name;
        [SerializeField] private string Type;

        [SerializeField] private EditPanel panel;

        public override void Add()
        {
            panel.AddVariable(new Variable(Name, Type, ProtectType));
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetType(string type)
        {
            Type = type;
        }

        private void Start ()
        {
            SetupDropdownMenu();
        }
    }
}