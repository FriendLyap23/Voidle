using R3;
using System;
using UnityEngine;

public class UpgradesStorage: IDisposable
{
    private ReactiveProperty<long> _currentPrice = new();

    public ReadOnlyReactiveProperty<long> CurrentPrice => _currentPrice;
    public string Name { get; private set; }
    public string Description { get; private set; }
    public float PriceMultiplier { get; private set; }
    public int Value { get; private set; }
    public UpgradeType Type { get; private set; }
    public Sprite Icon { get; private set; }

    public UpgradesStorage(string name, string description , long currentPrice, float priceMultiplier
        , int value, UpgradeType type, Sprite icon)
    {
        Name = name;
        Description = description;
        _currentPrice.Value = currentPrice;
        PriceMultiplier = priceMultiplier;
        Value = value;
        Type = type;
        Icon = icon;
    }

    public void ApplyUpgrade(MoneyStorage moneyStorage)
    {
        switch (Type)
        {
            case UpgradeType.MoneyPerClick:
                moneyStorage.SetNewValueMoneyPerClick(Value);
                break;
            case UpgradeType.MoneyPerSecond:
                moneyStorage.SetNewValueMoneyPerSecond(Value);
                break;
            default:
                break;
        }
    }

    public void PriceChanged(long newPrice) 
    {
        _currentPrice.Value = newPrice;
    }

    public void RecalculationCurrentPrice()
    {
        double calculatedPrice = (double)_currentPrice.Value * PriceMultiplier;

        long newPrice = (long)Math.Round(calculatedPrice);

        if (newPrice <= _currentPrice.Value)
        {
            newPrice = _currentPrice.Value + 1;
        }

        PriceChanged(newPrice);
    }

    public void LoadFromSaveData(UpgradeSaveData saveData)
    {
        if (saveData != null)
            _currentPrice.Value = saveData.CurrentPrice;
    }

    public void PopulateSaveData(UpgradeSaveData saveData)
    {
        if (saveData != null)
        {
            saveData.Name = Name;
            saveData.CurrentPrice = _currentPrice.Value;
        }
    }

    public void Dispose()
    {
        _currentPrice.Dispose();
    }
}