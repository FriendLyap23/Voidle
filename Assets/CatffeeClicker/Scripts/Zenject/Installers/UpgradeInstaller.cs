using Zenject;

public class UpgradeInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<UpgradeRegistry>().AsSingle();
        Container.BindInterfacesAndSelfTo<UpgradeFactory>().AsSingle();
        Container.Bind<PurchaseService>().AsSingle();
    }
}
