using System;
using Zenject;

public sealed class MoneyStorage : IDataPersistence
{
    public long Money { get; private set; }
    public int MoneyPerClick { get; private set; }
    public int MoneyPerSecond { get; private set; }

    public long MaxMoney { get; private set; }

    public event Action<long> OnMoneyChanged;
    public event Action<int> OnMoneyPerClickChanged;
    public event Action<int> OnMoneyPerSecondChanged;

    public bool CanAddMoney(long amount) => Money + amount <= MaxMoney;
    public bool CanSpendMoney(long amount) => Money >= amount;

    private SaveConfig _saveConfig;

    [Inject]
    private void Constructor(SaveConfig saveConfig)
    {
        _saveConfig = saveConfig;
    }

    public void AddMoney(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount),
                "Cannot add a negative amount of money");

        if (!CanAddMoney(amount))
        {
            long possibleAmount = MaxMoney - Money;

            if (possibleAmount > 0)
            {
                Money += possibleAmount;
                OnMoneyChanged?.Invoke(Money);
            }
            return;
        }

        Money += amount;
        OnMoneyChanged?.Invoke(Money);
    }

    public void SpendMoney(long amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount),
                "Cannot spend a negative amount of money");

        if (!CanSpendMoney(amount))
            return;

        Money -= amount;
        OnMoneyChanged?.Invoke(Money);
    }

    public void AddMoneyPerClick()
    {
        AddMoney(MoneyPerClick);
    }

    public void AddMoneyPerSecond()
    {
        AddMoney(MoneyPerSecond);
    }

    public void SetNewValueMoneyPerSecond(int addedAmount)
    {
        if (addedAmount < 0)
            throw new ArgumentOutOfRangeException(nameof(addedAmount),
                "Money per second cannot be negative");

   
        MoneyPerSecond += addedAmount;
        OnMoneyPerSecondChanged?.Invoke(MoneyPerSecond);
    }

    public void SetNewValueMoneyPerClick(int addedAmount)
    {
        if (addedAmount < 0)
            throw new ArgumentOutOfRangeException(nameof(addedAmount),
                "Money per click cannot be negative");

        MoneyPerClick += addedAmount;
        OnMoneyPerClickChanged?.Invoke(MoneyPerClick);

    }

    public void LoadData(GameData data)
    {
        Money = data.Money;
        MoneyPerClick = data.MoneyPerClick;
        MoneyPerSecond = data.MoneyPerSecond;

        MaxMoney = _saveConfig.MaxMoney; 

        OnMoneyChanged?.Invoke(Money);
        OnMoneyPerClickChanged?.Invoke(MoneyPerClick);
        OnMoneyPerSecondChanged?.Invoke(MoneyPerSecond);
    }

    public void SaveData(ref GameData data)
    {
        data.Money = Money;
        data.MoneyPerClick = MoneyPerClick;
        data.MoneyPerSecond = MoneyPerSecond;
    }
}
