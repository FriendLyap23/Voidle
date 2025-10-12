using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int Money;
    public int MoneyPerClick;
    public int MoneyPerSecond;

    public int Level;
    public float ExperienceLevel;
    public int ExperiencePerClick;
    public int MaxMonies;

    public List<UpgradeSaveData> UpgradesSaveData = new List<UpgradeSaveData>();

    public GameData()
    {
        Money = 0;
        MoneyPerClick = 1;
        MoneyPerSecond = 0;

        Level = 1;
        ExperienceLevel = 0;
        ExperiencePerClick = 1;
        MaxMonies = 1000;
    }
}

[System.Serializable]
public class UpgradeSaveData
{
    public string Name;
    public int CurrentPrice;
}