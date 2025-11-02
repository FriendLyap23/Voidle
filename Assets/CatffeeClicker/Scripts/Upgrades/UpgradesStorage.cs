using System;
using UnityEngine;

public class UpgradesStorage
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public long BasePrice { get; private set; }
    public long CurrentPrice { get; private set; }
    public float PriceMultiplier { get; private set; }
    public int Value { get; private set; }
    public UpgradeType Type { get; private set; }
    public Sprite Icon { get; private set; }

    public event Action<long> OnPriceUpgradeChanged;

    public UpgradesStorage(string name, string description, long basePrice, float priceMultiplier
        , int value, UpgradeType type, Sprite icon)
    {
        Name = name;
        Description = description;
        BasePrice = basePrice;
        PriceMultiplier = priceMultiplier;
        Value = value;
        Type = type;
        Icon = icon;

        CurrentPrice = BasePrice;
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
        CurrentPrice = newPrice;
        OnPriceUpgradeChanged?.Invoke(CurrentPrice);
    }

    public void RecalculationCurrentPrice()
    {
        long newPrice = CurrentPrice * ((long)PriceMultiplier);
        PriceChanged(newPrice);
    }

    public void LoadFromSaveData(UpgradeSaveData saveData)
    {
        if (saveData != null)
        {
            CurrentPrice = saveData.CurrentPrice;
            OnPriceUpgradeChanged?.Invoke(saveData.CurrentPrice);
        }
    }

    public void PopulateSaveData(UpgradeSaveData saveData)
    {
        if (saveData != null)
        {
            saveData.Name = Name;
            saveData.CurrentPrice = CurrentPrice;
        }
    }
}