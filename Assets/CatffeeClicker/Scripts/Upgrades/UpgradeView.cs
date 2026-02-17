using R3;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

public class UpgradeView : MonoBehaviour
{
    [Header("Upgrade Configuration")]
    [SerializeField] private string _upgradeName;
    [SerializeField] private string _description;
    [SerializeField] private long _currentPrice;
    [SerializeField] private float _priceMultiplier;
    [SerializeField] private int _value;
    [SerializeField] private UpgradeType _type;
    [SerializeField] private AssetReferenceSprite _icon;

    [Space]
    [Header("UI References")]
    [SerializeField] private TMP_Text _nameUpgradeView;
    [SerializeField] private TMP_Text _descriptionUpgradeView;
    [SerializeField] private TMP_Text _priceUpgradeView;

    [SerializeField] private Image _iconUpgradeView;

    [SerializeField] private Button _buyUpgradeButton;

    private UpgradesViewModelFactory _viewModelFactory;
    private UpgradeViewModel _upgradesViewModel;

    private CompositeDisposable _disposables = new();

    [Inject]
    private void Constructor(UpgradesViewModelFactory upgradesViewModelFactory)
    {
        _viewModelFactory = upgradesViewModelFactory;

        _upgradesViewModel = _viewModelFactory.Create(_upgradeName, _description, 
            _currentPrice, _priceMultiplier,_value, _type, _icon);

        _upgradesViewModel.Initialize();
    }

    private void Start()
    {
        SetupBindings();

        _buyUpgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    }

    public void OnUpgradeButtonClick()
    {
        _upgradesViewModel.Purchase();
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
