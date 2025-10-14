using UnityEngine;
using Zenject;

public class MoneyPerSecondService : MonoBehaviour
{
    private MoneyStorage _moneyStorage;
    private float _timer;

    [Inject]
    private void Constructor(MoneyStorage moneyStorage)
    {
        _moneyStorage = moneyStorage;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= 1f)
        {
            _timer = 0f;
            if (_moneyStorage.MoneyPerSecond > 0)
                _moneyStorage.AddMoneyPerSecond();
        }
    }
}
