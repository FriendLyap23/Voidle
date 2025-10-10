using System.Collections.Generic;
using System.Linq;

public class UpgradeRegistry : IDataPersistence
{
    private readonly List<UpgradesStorage> _registeredUpgrades = new();

    public void RegisterUpgrade(UpgradesStorage upgrade)
    {
        if (!_registeredUpgrades.Contains(upgrade))
        {
            _registeredUpgrades.Add(upgrade);
        }
    }

    public void LoadData(GameData data)
    {
        if (data.UpgradesSaveData == null)
            return;

        foreach (var upgrade in _registeredUpgrades)
        {
            var saveData = data.UpgradesSaveData.FirstOrDefault(x => x.Name == upgrade.Name);
            if (saveData != null)
            {
                upgrade.LoadFromSaveData(saveData);
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.UpgradesSaveData == null)
            data.UpgradesSaveData = new List<UpgradeSaveData>();

        data.UpgradesSaveData.RemoveAll(x => _registeredUpgrades.Any(u => u.Name == x.Name));

        foreach (var upgrade in _registeredUpgrades)
        {
            var saveData = new UpgradeSaveData();
            upgrade.PopulateSaveData(saveData);
            data.UpgradesSaveData.Add(saveData);
        }
    }
}
