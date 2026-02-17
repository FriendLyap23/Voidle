using R3;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class UpgradeViewModel : IInitializable, IDisposable
{
    private PurchaseService _purchaseService;
    private CompositeDisposable _disposables = new();

    private UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<Sprite> _iconHandle;
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

        LoadIcon();

        _upgradesStorage.CurrentPrice.Subscribe(FormatterPrice).AddTo(_disposables);
    }

    public void LoadIcon() 
    {
        if (_upgradesStorage.Icon == null || !_upgradesStorage.Icon.RuntimeKeyIsValid())
            return;

        _iconHandle = _upgradesStorage.Icon.LoadAssetAsync();
        _iconHandle.Completed += handle =>
            {
                if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
                {
                    _icon.Value = handle.Result;
                }
            };
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
        if (_iconHandle.IsValid())
            Addressables.Release(_iconHandle);

        _name.Dispose();
        _description.Dispose();
        _price.Dispose();
        _icon.Dispose();

        _disposables?.Dispose();
    }
}
