using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public long Money;
    public int MoneyPerClick;
    public int MoneyPerSecond;
    public long MaxMonies;

    public int Level;
    public float ExperienceLevel;
    public int ExperiencePerClick;

    public List<UpgradeSaveData> UpgradesSaveData = new List<UpgradeSaveData>();

    public GameData()
    {
        Money = 0;
        MoneyPerClick = 1;
        MoneyPerSecond = 0;

        Level = 1;
        ExperienceLevel = 0;
        ExperiencePerClick = 1;
        MaxMonies = 100000;
    }
}

[System.Serializable]
public class UpgradeSaveData
{
    public string Name;
    public long BasePrice;
    public long CurrentPrice;
}