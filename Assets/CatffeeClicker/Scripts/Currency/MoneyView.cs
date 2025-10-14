using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class MoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;

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
            .Subscribe(money => _moneyText.text = money)
            .AddTo(_disposables);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
