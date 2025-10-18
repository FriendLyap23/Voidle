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
        if (_moneyStorage.CanSpendMoney(storage.CurrentPrice)) 
        {
            _moneyStorage.SpendMoney(storage.CurrentPrice);

            storage.ApplyUpgrade(_moneyStorage);
            storage.RecalculationCurrentPrice();
        }
    }
}
