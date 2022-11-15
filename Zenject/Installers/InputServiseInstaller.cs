using Assets.Scripts.InputSystem;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Zenject
{
    public class InputServiseInstaller : MonoInstaller
    {
        [SerializeField] private InputServise inputServise;

        public override void InstallBindings()
        {
            Container.Bind<InputServise>().FromInstance(inputServise).AsCached();
            Container.QueueForInject(inputServise);
        }
    }
}