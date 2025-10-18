using System;
using UnityEngine;

public class UpgradesStorage
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public int CurrentPrice { get; private set; }
    //{
    //    get { return _currentPrice; }
    //    set
    //    {
    //        if (value < 0)
    //            throw new Exception("Price cannot be a negative");

    //        _currentPrice = value;
    //        OnPriceUpgrafeChanged?.Invoke(_currentPrice);
    //    }
    //}

    public int Value { get; private set; }
    public UpgradeType Type { get; private set; }
    public Sprite Icon { get; private set; }

    public event Action<int> OnPriceUpgradeChanged;

    public UpgradesStorage(string name, string description, int basePrice, int value, 
        UpgradeType type, Sprite icon)
    {
        Name = name;
        Description = description;
        CurrentPrice = basePrice;
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

    public void PriceChanged(int newPrice) 
    {
        CurrentPrice = newPrice;
        OnPriceUpgradeChanged?.Invoke(CurrentPrice);
    }

    public void RecalculationCurrentPrice() 
    {
        int newPrice = CurrentPrice += 20;
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