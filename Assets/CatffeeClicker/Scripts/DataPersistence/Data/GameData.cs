using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int Money;
    public int MoneyPerClick;
    public int MoneyPerSecond;

    public int CurrentLevel;
    public float CurrentExperienceLevel;
    public int ExperiencePerClick;

    public List<UpgradeSaveData> UpgradesSaveData = new List<UpgradeSaveData>();

    public GameData()
    {
        Money = 0;
        MoneyPerClick = 1;
        MoneyPerSecond = 0;

        CurrentLevel = 1;
        CurrentExperienceLevel = 0;
        ExperiencePerClick = 1;
    }
}

[System.Serializable]
public class UpgradeSaveData
{
    public string Name;
    public bool IsPurchased;
    public int CurrentPrice;
}