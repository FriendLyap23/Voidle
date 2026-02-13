using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public long Money;
    public int MoneyPerClick;
    public int MoneyPerSecond;
    public long MaxMonies;

    public int Level;
    public int ExperienceLevel;
    public int ExperiencePerClick;

    public List<UpgradeSaveData> UpgradesSaveData = new List<UpgradeSaveData>();
}

[System.Serializable]
public class UpgradeSaveData
{
    public string Name;
    public long CurrentPrice;
}