using R3;
using System;
using UnityEngine;
using Zenject;

public class UpgradeViewModel : IInitializable, IDisposable
{
    private PurchaseService _purchaseService;

    private CompositeDisposable _disposables = new();
    public UpgradesStorage _upgradesStorage { get; private set; }

    private ReactiveProperty<string> _name = new();
    private ReactiveProperty<string> _description = new();
    private ReactiveProperty<string> _price = new();
    private ReactiveProperty<Sprite> _icon = new();

    public ReadOnlyReactiveProperty<string> Name => _name;
    public ReadOnlyReactiveProperty<string> Description => _description;
    public ReadOnlyReactiveProperty<string> Price => _price;
    public ReadOnlyReactiveProperty<Sprite> Icon => _icon;

    public UpgradeViewModel(UpgradesStorage upgradesStorage, PurchaseService purchaseService)
    {
        _upgradesStorage = upgradesStorage;
        _purchaseService = purchaseService;
    }

    public void Initialize()
    {
        _name.Value = _upgradesStorage.Name;
        _description.Value = _upgradesStorage.Description;
        _icon.Value = _upgradesStorage.Icon;

        _upgradesStorage.CurrentPrice.Subscribe(FormatterPrice).AddTo(_disposables);
    }

    public void Purchase() 
    {
        _purchaseService.TryPurchaseUpgrade(_upgradesStorage);
    }

    private void FormatterPrice(long price) 
    {
        _price.Value = CurrencyFormatter.Format(price);
    }

    public void Dispose()
    {
        _name.Dispose();
        _description.Dispose();
        _price.Dispose();
        _icon.Dispose();

        _disposables?.Dispose();
    }
}
