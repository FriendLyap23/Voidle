using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UpgradeFactory
{
    private readonly Dictionary<string, UpgradesStorage> _cache = new();
    private readonly UpgradeRegistry _upgradeRegistry;

    public UpgradeFactory(UpgradeRegistry upgradeRegistry)
    {
        _upgradeRegistry = upgradeRegistry;
    }

    public UpgradesStorage Create(string name, string description, long currentPrice, float priceMultiplier,
        int value, UpgradeType type, AssetReferenceSprite icon)
    {
        if (!_cache.ContainsKey(name))
        {
            var upgrade = new UpgradesStorage(name, description, currentPrice, priceMultiplier, value, type, icon);
            _cache[name] = upgrade;

            _upgradeRegistry.RegisterUpgrade(upgrade);
        }

        return _cache[name];
    }
}
