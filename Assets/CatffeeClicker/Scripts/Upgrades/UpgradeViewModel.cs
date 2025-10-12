using R3;
using System;
using UnityEngine;
using Zenject;

public class UpgradeViewModel : IInitializable
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
        Price.Value = _upgradesStorage.BasePrice.ToString();
        Icon.Value = _upgradesStorage.Icon;
    }
}
