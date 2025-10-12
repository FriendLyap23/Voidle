using System;

public sealed class MoneyStorage : IDataPersistence
{
    public int Money { get; private set; }
    public int MoneyPerClick { get; private set; }
    public int MoneyPerSecond { get; private set; }

    public int MaxMonies { get; private set; }

    public event Action<int> OnMoneyChanged;
    public event Action<int> OnMoneyPerClickChanged;
    public event Action<int> OnMoneyPerSecondChanged;

    public void SetMoney(int money)
    {
        if (money < 0)
            throw new ArgumentOutOfRangeException(nameof(money),
                "Money cannot be negative");

        if (money > MaxMonies)
            throw new ArgumentOutOfRangeException(nameof(money),
                $"The amount of money cannot exceed the maximum amount set - {MaxMonies}");

        if (Money != money)
        {
            Money = money;
            OnMoneyChanged?.Invoke(Money);
        }
    }

    public void SetMoneyPerSecond(int moneyPerSecond)
    {
        if (moneyPerSecond < 0)
            throw new ArgumentOutOfRangeException(nameof(moneyPerSecond),
                "Money per second cannot be negative");

        if (MoneyPerSecond != moneyPerSecond)
        {
            MoneyPerSecond += moneyPerSecond;
            OnMoneyPerSecondChanged?.Invoke(moneyPerSecond);
        }
    }

    public void SetMoneyPerClick(int moneyPerClick)
    {
        if (moneyPerClick < 0)
            throw new ArgumentOutOfRangeException(nameof(moneyPerClick),
                "Money per click cannot be negative");

        if (MoneyPerClick != moneyPerClick)
        {
            MoneyPerClick += moneyPerClick;
            OnMoneyPerClickChanged?.Invoke(moneyPerClick);
        }
    }

    public bool IsEnoughSpace(int countMoney) => Money + countMoney <= MaxMonies;

    public void AddMoneyPerClick(int countMoney)
    {
        if (countMoney < 0)
            throw new ArgumentOutOfRangeException(nameof(countMoney),
                "Cannot add a negative amount of money");

        if (!IsEnoughSpace(countMoney))
            return;

        Money += MoneyPerClick;
        OnMoneyChanged?.Invoke(Money);
    }

    public void SpendMoney(int countMoney)
    {
        if (countMoney < 0)
            throw new ArgumentOutOfRangeException(nameof(countMoney),
                "Cannot spend a negative amount of money");

        if (Money - countMoney < 0)
            return;

        Money -= countMoney;
        OnMoneyChanged?.Invoke(Money);
    }

    public void LoadData(GameData data)
    {
        Money = data.Money;
        MoneyPerClick = data.MoneyPerClick;
        MoneyPerSecond = data.MoneyPerSecond;
        MaxMonies = data.MaxMonies; 

        OnMoneyChanged?.Invoke(Money);
        OnMoneyPerClickChanged?.Invoke(MoneyPerClick);
        OnMoneyPerSecondChanged?.Invoke(MoneyPerSecond);
    }

    public void SaveData(ref GameData data)
    {
        data.Money = Money;
        data.MoneyPerClick = MoneyPerClick;
        data.MoneyPerSecond = MoneyPerSecond;
        data.MaxMonies = MaxMonies;
    }
}
