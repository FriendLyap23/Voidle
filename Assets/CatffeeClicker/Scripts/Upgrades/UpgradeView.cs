using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UpgradeView : MonoBehaviour
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
    [SerializeField] private TMP_Text _nameUpgradeView;
    [SerializeField] private TMP_Text _descriptionUpgradeView;
    [SerializeField] private TMP_Text _priceUpgradeView;

    [SerializeField] private Image _iconUpgradeView;

    [SerializeField] private Button _buyUpgradeButton;

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

        _buyUpgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    }

    public void OnUpgradeButtonClick()
    {
        _purchaseService.TryPurchaseUpgrade(_upgradesViewModel._upgradesStorage);
    }

    private void SetupBindings()
    {
        _upgradesViewModel.Name
            .Subscribe(name => _nameUpgradeView.text = name)
            .AddTo(_disposables);

        _upgradesViewModel.Description
            .Subscribe(description => _descriptionUpgradeView.text = description)
            .AddTo(_disposables);

        _upgradesViewModel.Price
            .Subscribe(price => _priceUpgradeView.text = price)
            .AddTo(_disposables);

        _upgradesViewModel.Icon
            .Subscribe(icon => _iconUpgradeView.sprite = icon)
            .AddTo(_disposables);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
