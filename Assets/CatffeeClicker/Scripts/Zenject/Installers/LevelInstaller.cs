using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<LevelStorage>().AsSingle().NonLazy();
    }
}
