using Assets.Scripts.Controls;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Zenject
{
    public class SceneControllerInstaller : MonoInstaller
    {
        [SerializeField] private SceneController controller;

        public override void InstallBindings()
        {
            Container.Bind<SceneController>().FromInstance(controller).AsSingle();
            Container.QueueForInject(controller);
        }
    }
}