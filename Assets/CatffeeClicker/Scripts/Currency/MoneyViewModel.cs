using R3;
using System;
using Zenject;

public sealed class MoneyViewModel : IInitializable, IDisposable
{
    private readonly MoneyStorage _moneyStorage;

    public readonly ReactiveProperty<string> Money = new();

    public MoneyViewModel(MoneyStorage moneyStorage)
    {
        _moneyStorage = moneyStorage;
    }

    public void Initialize()
    {
        MoneyChanged(_moneyStorage.Money);
        _moneyStorage.OnMoneyChanged += MoneyChanged;
    }
     
    public void AddMoneyPerClick()
    {
        _moneyStorage.AddMoneyPerClick(_moneyStorage.Money);
    }

    public void AddMoneyPerSecond() 
    {
        //_moneyStorage.SetMoneyPerSecond();
    }

    public void SpendMoney(int amount) 
    {
        _moneyStorage.SpendMoney(amount);
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
