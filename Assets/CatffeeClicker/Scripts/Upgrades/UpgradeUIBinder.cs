using R3;
using UnityEngine;
using Zenject;

public class UpgradeUIBinder : MonoBehaviour
{
    [Header("Upgrade Configuration")]
    [SerializeField] private string _upgradeName;
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private int _value;
    [SerializeField] private UpgradeType _type;
    [SerializeField] private Sprite _icon;

    [Space]
    [Header("UI References")]
    [SerializeField] private TextView _nameUpgradeView;
    [SerializeField] private TextView _descriptionUpgradeView;
    [SerializeField] private TextView _priceUpgradeView;

    [SerializeField] private ImageView _iconUpgradeView;

    [SerializeField] private ButtonView _buyUpgradeButton;

    private PurchaseService _purchaseService;
    private UpgradesViewModelFactory _viewModelFactory;
    private UpgradeViewModel _upgradesViewModel;

    private CompositeDisposable _disposables = new();

    [Inject]
    private void Constructor(PurchaseService purchaseService, UpgradesViewModelFactory upgradesViewModelFactory)
    {
        _purchaseService = purchaseService;
        _viewModelFactory = upgradesViewModelFactory;

        _upgradesViewModel = _viewModelFactory.Create(_upgradeName, _description, _price, _value, _type, _icon);
        _upgradesViewModel.Initialize();
    }

    private void Start()
    {
        SetupBindings();

        _buyUpgradeButton.OnClick += () => _purchaseService.TryPurchaseUpgrade(_upgradesViewModel._upgradesStorage);
    }


    private void SetupBindings()
    {
        _upgradesViewModel.Name
            .Subscribe(name => _nameUpgradeView.CurrencyText.text = name)
            .AddTo(_disposables);

        _upgradesViewModel.Description
            .Subscribe(description => _descriptionUpgradeView.CurrencyText.text = description)
            .AddTo(_disposables);

        _upgradesViewModel.Price
            .Subscribe(price => _priceUpgradeView.CurrencyText.text = price)
            .AddTo(_disposables);

        _upgradesViewModel.Icon
            .Subscribe(icon => _iconUpgradeView.Image.sprite = icon)
            .AddTo(_disposables);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
