using R3;
using System;
using Zenject;

public sealed class MoneyStorage : IDataPersistence, IDisposable
{
    private ReactiveProperty<long> _money = new(0);
    private ReactiveProperty<int> _moneyPerClick = new(0);
    private ReactiveProperty<int> _moneyPerSecond = new(0);

    public ReadOnlyReactiveProperty<long> Money => _money;
    public ReadOnlyReactiveProperty<int> MoneyPerClick => _moneyPerClick;
    public ReadOnlyReactiveProperty<int> MoneyPerSecond => _moneyPerSecond;

    private long MaxMoney;

    private SaveConfig _saveConfig;

    [Inject]
    private void Constructor(SaveConfig saveConfig)
    {
        _saveConfig = saveConfig;
    }

    public bool CanAddMoney(long amount) => _money.Value + amount <= MaxMoney;
    public bool CanSpendMoney(long amount) => _money.Value >= amount;

    public void AddMoney(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount),
                "Cannot add a negative amount of money");

        if (!CanAddMoney(amount))
        {
            long possibleAmount = MaxMoney - _money.Value;

            if (possibleAmount > 0)
                _money.Value += possibleAmount;

            return;
        }

        _money.Value += amount;   
    }

    public void SpendMoney(long amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount),
                "Cannot spend a negative amount of money");

        if (!CanSpendMoney(amount))
            return;

        _money.Value -= amount;
    }

    public void AddMoneyPerClick()
    {
        AddMoney(_moneyPerClick.Value);
    }

    public void AddMoneyPerSecond()
    {
        AddMoney(_moneyPerSecond.Value);
    }

    public void SetNewValueMoneyPerSecond(int addedAmount)
    {
        if (addedAmount < 0)
            throw new ArgumentOutOfRangeException(nameof(addedAmount),
                "Money per second cannot be negative");

   
        _moneyPerSecond.Value += addedAmount;
    }

    public void SetNewValueMoneyPerClick(int addedAmount)
    {
        if (addedAmount < 0)
            throw new ArgumentOutOfRangeException(nameof(addedAmount),
                "Money per click cannot be negative");

        _moneyPerClick.Value += addedAmount;
    }

    public void LoadData(GameData data)
    {
        _money.Value = data.Money;
        _moneyPerClick.Value = data.MoneyPerClick;
        _moneyPerSecond.Value = data.MoneyPerSecond;

        MaxMoney = _saveConfig.MaxMoney; 
    }

    public void SaveData(ref GameData data)
    {
        data.Money = _money.Value;
        data.MoneyPerClick = _moneyPerClick.Value;
        data.MoneyPerSecond = _moneyPerSecond.Value;
    }

    public void Dispose()
    {
        _money.Dispose();
        _moneyPerClick.Dispose();
        _moneyPerSecond.Dispose();
    }
}
