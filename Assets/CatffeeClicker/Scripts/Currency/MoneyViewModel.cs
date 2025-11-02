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

    private void MoneyChanged(long money)
    {
        Money.Value = FormatMoney(money);
    }

    private string FormatMoney(long amount)
    {
        string[] suffixes = { "", "Ъ", "Ь", "С", "в", "Ът", "Ъэ", "бѕ", "бя", "Ю", "Э" };

        if (amount == 0)
            return "0";

        int suffixIndex = 0;
        double formattedAmount = amount;

        while (formattedAmount >= 1000 && suffixIndex < suffixes.Length - 1)
        {
            formattedAmount /= 1000;
            suffixIndex++;
        }

        return formattedAmount.ToString("0.##") + suffixes[suffixIndex];
    }

    public void Dispose()
    {
        _moneyStorage.OnMoneyChanged -= MoneyChanged;
    }
}
