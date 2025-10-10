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
        _moneyStorage.SpendMoney(storage.BasePrice);
        storage.ApplyUpgrade(_moneyStorage);
    }
}
