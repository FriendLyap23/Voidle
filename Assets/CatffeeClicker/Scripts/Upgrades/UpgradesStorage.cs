using System;
using UnityEngine;

public class UpgradesStorage
{
    private string _name;
    private string _description;

    private int _basePrice;
    private int _currentPrice;
    private int _value;

    private UpgradeType _type;
    private Sprite _icon;

    public string Name => _name;
    public string Description => _description;

    public int BasePrice
    {
        get { return _basePrice; }
        set
        {
            if (value < 0)
                throw new Exception("Price cannot be a negative");

            _basePrice = value;
        }
    }

    public int Value => _value;
    public UpgradeType Type => _type;
    public Sprite Icon => _icon;

    public UpgradesStorage(string name, string description, int basePrice, int value, UpgradeType type, Sprite icon)
    {
        _name = name;
        _description = description;
        _basePrice = basePrice;
        _value = value;
        _type = type;
        _icon = icon;
    }

    public void ApplyUpgrade(MoneyStorage moneyStorage)
    {
        switch (_type)
        {
            case UpgradeType.MoneyPerClick:
                moneyStorage.SetNewValueMoneyPerClick(_value);
                break;
            case UpgradeType.MoneyPerSecond:
                moneyStorage.SetNewValueMoneyPerSecond(_value);
                break;
            default:
                break;
        }
    }

    public void LoadFromSaveData(UpgradeSaveData saveData)
    {
        if (saveData != null)
        {
            BasePrice = saveData.CurrentPrice;
        }
    }

    public void PopulateSaveData(UpgradeSaveData saveData)
    {
        if (saveData != null)
        {
            saveData.Name = Name;
            saveData.CurrentPrice = BasePrice;
        }
    }
}