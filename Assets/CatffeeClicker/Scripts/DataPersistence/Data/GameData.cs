using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public long Money = 0;
    public int MoneyPerClick = 1;
    public int MoneyPerSecond = 0;

    public int Level = 0;
    public int ExperienceLevel = 0;
    public int ExperiencePerClick = 1;

    public List<UpgradeSaveData> UpgradesSaveData = new List<UpgradeSaveData>();
}

[System.Serializable]
public class UpgradeSaveData
{
    public string Name;
    public long CurrentPrice;
}