using R3;
using System;
using Zenject;

public sealed class MoneyViewModel : IInitializable, IDisposable
{
    private readonly MoneyStorage _moneyStorage;
    private readonly CompositeDisposable _disposables = new();

    public ReactiveProperty<string> Money { get; } = new();

    public MoneyViewModel(MoneyStorage moneyStorage)
    {
        _moneyStorage = moneyStorage;
    }

    public void Initialize()
    {
        _moneyStorage.Money.Subscribe(MoneyFormatter).AddTo(_disposables);
    }
     
    public void AddMoneyPerClick()
    {
        _moneyStorage.AddMoneyPerClick();
    }

    private void MoneyFormatter(long money)
    {
        Money.Value = CurrencyFormatter.Format(money);
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}
