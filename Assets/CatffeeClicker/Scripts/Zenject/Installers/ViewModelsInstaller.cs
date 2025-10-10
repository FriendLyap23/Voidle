using Zenject;

public sealed class ViewModelsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<MoneyViewModel>()
            .AsSingle()
            .NonLazy();

        Container
            .BindInterfacesAndSelfTo<LevelViewModel>()
            .AsSingle()
            .NonLazy();

        Container
            .Bind<UpgradesViewModelFactory>()
            .AsSingle()
            .NonLazy();
    }
}
