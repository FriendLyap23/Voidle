using R3;
using UnityEngine;
using Zenject;

public sealed class MoneyUIBinder : MonoBehaviour
{
    [SerializeField] private TextView _currencyView;
    [SerializeField] private ButtonView _moneyPerClickButton;

    private MoneyViewModel _moneyViewModel;
    private CompositeDisposable _disposables = new();

    [Inject]
    private void Constructor(MoneyViewModel moneyViewModel) 
    {
        _moneyViewModel = moneyViewModel;
    }

    private void Start()
    {
        _moneyViewModel.Money
            .Subscribe(money => _currencyView.CurrencyText.text = money)
            .AddTo(_disposables);

        _moneyPerClickButton.OnClick += OnMoneyButtonClick;
    }

    public void Update()
    {
        _moneyViewModel.AddMoneyPerSecond();
    }

    private void OnMoneyButtonClick() 
    {
        _moneyViewModel.AddMoneyPerClick();
    }

    private void OnDestroy()
    {
        _moneyPerClickButton.OnClick -= OnMoneyButtonClick;
        _disposables.Dispose();
    }
}
