using UnityEngine;
using Zenject;

public sealed class MoneyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<MoneyStorage>().AsSingle().NonLazy();
    }
}
