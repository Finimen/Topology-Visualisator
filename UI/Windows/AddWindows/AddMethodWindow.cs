using UnityEngine;

namespace Assets.Scripts.Windows
{
    public class AddMethodWindow : AddWindow
    {
        [SerializeField] private string Name;
        [SerializeField] private string ReturnedType;
        [SerializeField] private string Arguments;

        [SerializeField] private EditPanel panel;

        public override void Add()
        {
            panel.AddMethod(new Method(Name, ReturnedType, Arguments, ProtectType));
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetReturnedType(string type)
        {
            ReturnedType = type;
        }

        public void SetArguments(string arguments)
        {
            Arguments = arguments;
        }

        private void Start()
        {
            SetupDropdownMenu(); 
        }
    }
}