using UnityEngine;

[CreateAssetMenu(fileName = "SaveConfig", menuName = "Game/Save Config")]
public class SaveConfig : ScriptableObject
{
    public int MaxLevel;
    public long MaxMoney = 1000000000;

    public int[] ExperienceForLevel;
}
