using System.Collections.Generic;
using System.Linq;
using Zenject;

public class UpgradeRegistry : IDataPersistence
{
    private readonly List<UpgradesStorage> _registeredUpgrades = new();
    private List<UpgradeSaveData> _cachedSaveData = new();

    public void RegisterUpgrade(UpgradesStorage upgrade)
    {
        if (!_registeredUpgrades.Contains(upgrade))
        {
            _registeredUpgrades.Add(upgrade);

            TryLoadUpgradeFromCache(upgrade);
        }
    }

    private void TryLoadUpgradeFromCache(UpgradesStorage upgrade) 
    {
        if (_cachedSaveData == null)
            return;

        var data = _cachedSaveData.FirstOrDefault(x => x.Name == upgrade.Name);
        if (data != null) 
        {
            upgrade.LoadFromSaveData(data);
        }
    }

    public void LoadData(GameData data)
    {
        _cachedSaveData = data.UpgradesSaveData ?? new List<UpgradeSaveData>();

        //if (data.UpgradesSaveData == null)
        //    return;

        foreach (var upgrade in _registeredUpgrades)
        {
            //var saveData = data.UpgradesSaveData.FirstOrDefault(x => x.Name == upgrade.Name);
            //if (saveData != null)
            //{
            //    upgrade.LoadFromSaveData(saveData);
            //}

            TryLoadUpgradeFromCache(upgrade);
        }
    }

    public void SaveData(ref GameData data)
    {
        //if (data.UpgradesSaveData == null)
        //    data.UpgradesSaveData = new List<UpgradeSaveData>();

        //data.UpgradesSaveData.RemoveAll(x => _registeredUpgrades.Any(u => u.Name == x.Name));

        //foreach (var upgrade in _registeredUpgrades)
        //{
        //    var saveData = new UpgradeSaveData();
        //    upgrade.PopulateSaveData(saveData);
        //    data.UpgradesSaveData.Add(saveData);
        //}

        data.UpgradesSaveData = _registeredUpgrades.Select(upgrade =>
        {
            var saveData = new UpgradeSaveData();
            upgrade.PopulateSaveData(saveData);
            return saveData;
        }).ToList();
    }
}
