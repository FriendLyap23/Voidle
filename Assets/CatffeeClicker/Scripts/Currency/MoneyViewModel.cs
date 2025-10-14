using R3;
using System;
using Zenject;

public sealed class MoneyViewModel : IInitializable, IDisposable
{
    private readonly MoneyStorage _moneyStorage;
    private readonly CompositeDisposable _disposables = new();

    public ReactiveProperty<string> Money { get; }

    public MoneyViewModel(MoneyStorage moneyStorage)
    {
        _moneyStorage = moneyStorage;
        Money = new ReactiveProperty<string>();

    }

    public void Initialize()
    {
        MoneyChanged(_moneyStorage.Money);
        _moneyStorage.OnMoneyChanged += MoneyChanged;
    }
     
    public void AddMoneyPerClick()
    {
        _moneyStorage.AddMoneyPerClick();
    }

    private void MoneyChanged(int money)
    {
        Money.Value = money + "$";
    }

    public void Dispose()
    {
        _moneyStorage.OnMoneyChanged -= MoneyChanged;
    }
}
