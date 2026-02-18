using UnityEngine;
using UnityEngine.AddressableAssets;

public class UpgradesViewModelFactory
{
    private readonly UpgradeFactory _upgradeFactory;
    private readonly PurchaseService _purchaseService;

    public UpgradesViewModelFactory(UpgradeFactory upgradeFactory, PurchaseService purchaseService)
    {
        _upgradeFactory = upgradeFactory;
        _purchaseService = purchaseService;
    }

    public UpgradeViewModel Create(string name, string description, long currentPrice, float priceMultiplier,
        int value, UpgradeType type, AssetReferenceSprite icon)
    {
        var upgradesStorage = _upgradeFactory.Create(name, description, currentPrice, priceMultiplier, value, type, icon);
        return new UpgradeViewModel(upgradesStorage, _purchaseService);
    }
}
