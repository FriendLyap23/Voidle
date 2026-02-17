using Zenject;

public class PurchaseService
{
    private readonly MoneyStorage _moneyStorage;

    [Inject]
    public PurchaseService(MoneyStorage moneyStorage)
    {
        _moneyStorage = moneyStorage;
    }

    public void TryPurchaseUpgrade(UpgradesStorage storage)
    {
        if (_moneyStorage.CanSpendMoney(storage.CurrentPrice.CurrentValue)) 
        {
            _moneyStorage.SpendMoney(storage.CurrentPrice.CurrentValue);

            storage.ApplyUpgrade(_moneyStorage);
            storage.RecalculationCurrentPrice();
        }
    }
}
