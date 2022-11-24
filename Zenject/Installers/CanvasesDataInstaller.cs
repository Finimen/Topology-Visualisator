using Assets.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Zenject.Installers
{
    internal class CanvasesDataInstaller : MonoInstaller
    {
        [SerializeField] private CanvasesData canvasesData;

        public override void InstallBindings()
        {
            Container.Bind<CanvasesData>().FromInstance(canvasesData).AsCached();
        }
    }
}