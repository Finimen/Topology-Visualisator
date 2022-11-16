using UnityEngine;
using Zenject;

namespace Assets.Scripts.Zenject
{
    public class G_CUIColorPickerInstaller : MonoInstaller
    {
        [SerializeField] private G_CUIColorPicker colorPiker;

        public override void InstallBindings()
        {
            Container.Bind<G_CUIColorPicker>().FromInstance(colorPiker).AsSingle();
            Container.QueueForInject(colorPiker);
        }
    }
}