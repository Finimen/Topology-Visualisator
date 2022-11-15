using UnityEngine;
using Zenject;

namespace Assets.Scripts.Zenject
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private new Camera camera;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(camera).AsSingle();
            Container.QueueForInject(camera);
        }
    }
}