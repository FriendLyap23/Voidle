using R3;
using System;
using UnityEngine;
using Zenject;

public class UpgradeViewModel : IInitializable, IDisposable
{
    public UpgradesStorage _upgradesStorage { get; private set; }

    public readonly ReactiveProperty<string> Name = new();
    public readonly ReactiveProperty<string> Description = new();

    public readonly ReactiveProperty<string> Price = new();
    public readonly ReactiveProperty<Sprite> Icon = new();

    public UpgradeViewModel(UpgradesStorage upgradesStorage)
    {
        _upgradesStorage = upgradesStorage;
    }

    public void Initialize()
    {
        Name.Value = _upgradesStorage.Name;
        Description.Value = _upgradesStorage.Description;
        Icon.Value = _upgradesStorage.Icon;

        PriceChanged(_upgradesStorage.CurrentPrice);

        _upgradesStorage.OnPriceUpgradeChanged += PriceChanged;
    }

    private void PriceChanged(int newPrice) 
    {
        Price.Value = newPrice.ToString();
    }

    public void Dispose()
    {
        _upgradesStorage.OnPriceUpgradeChanged -= PriceChanged;
    }
}
