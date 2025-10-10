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

    private bool _isPurchased;

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

    public bool IsPurchased
    {
        get => _isPurchased;
        set
        {
            _isPurchased = value;
            OnPurchaseStateChanged?.Invoke(_isPurchased);
        }
    }

    public int Value => _value;

    public UpgradeType Type => _type;
    public Sprite Icon => _icon;

    public event Action<int> OnPriceChanged;
    public event Action<bool> OnPurchaseStateChanged;

    public UpgradesStorage(string name, string description, int basePrice, int value, UpgradeType type, Sprite icon)
    {
        _name = name;
        _description = description;
        _basePrice = basePrice;
        _value = value;
        _type = type;
        _icon = icon;

        _isPurchased = false;
    }

    public void ApplyUpgrade(MoneyStorage moneyStorage)
    {
        switch (_type)
        {
            case UpgradeType.MoneyPerClick:
                moneyStorage.SetMoneyPerClick(_value); _isPurchased = true;
                break;
            case UpgradeType.MoneyPerSecond:
                moneyStorage.SetMoneyPerSecond(_value); _isPurchased = true;
                break;
            default:
                break;
        }
    }

    public void RecalculationPrice() 
    {
        int newPrice = _basePrice + 5;

        _basePrice = newPrice;
        OnPriceChanged?.Invoke(newPrice);
    }

    public void LoadFromSaveData(UpgradeSaveData saveData)
    {
        if (saveData != null)
        {
            IsPurchased = saveData.IsPurchased;
            BasePrice = saveData.CurrentPrice;
        }
    }

    public void PopulateSaveData(UpgradeSaveData saveData)
    {
        if (saveData != null)
        {
            saveData.Name = Name;
            saveData.IsPurchased = IsPurchased;
            saveData.CurrentPrice = BasePrice;
        }
    }
}
