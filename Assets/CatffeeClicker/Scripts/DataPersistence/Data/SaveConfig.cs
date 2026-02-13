using UnityEngine;

[CreateAssetMenu(fileName = "SaveConfig", menuName = "Game/Save Config")]
public class SaveConfig : ScriptableObject
{
    [Header("File Settings")]
    public string fileName = "game.save";
    public bool useEncryption = true;

    [Header("Default Game Values")]
    public long defaultMoney = 0;
    public int defaultMoneyPerClick = 1;
    public int defaultMoneyPerSecond = 0;
    public long defaultMaxMonies = 100000;

    public int[] ExperienceForLevel;

    public int MaxLevel;
    public int defaultLevel = 1;
    public int defaultExperienceLevel = 0;
    public int defaultExperiencePerClick = 1;
}
