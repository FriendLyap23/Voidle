using UnityEngine;

public class UpgradesViewModelFactory
{
    private readonly UpgradeFactory _upgradeFactory;

    public UpgradesViewModelFactory(UpgradeFactory upgradeFactory)
    {
        _upgradeFactory = upgradeFactory;
    }

    public UpgradeViewModel Create(string name, string description, int price, int value, UpgradeType type, Sprite icon)
    {
        var upgradesStorage = _upgradeFactory.Create(name, description, price, value, type, icon);
        return new UpgradeViewModel(upgradesStorage);
    }
}
