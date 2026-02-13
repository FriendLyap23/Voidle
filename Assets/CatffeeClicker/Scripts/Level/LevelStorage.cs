using System;
using Zenject;

public class LevelStorage : IDataPersistence
{
    private int _maxLevel;
    private int[] _experienceForLevel;

    public int CurrentLevel { get; private set; }
    public int CurrentExperienceLevel { get; private set; }
    public int ExperiencePerClick { get; private set; }

    public event Action<int> OnLevelChanged;
    public event Action<int> OnExperienceChanged;
    public event Action<int> OnExperiencePerClickChanged;

    private SaveConfig _saveConfig;

    [Inject]
    private void Constructor(SaveConfig saveConfig) 
    {
        _saveConfig = saveConfig;
    }

    public void ChangeExperiencePerClick(int newValue) 
    {
        if (newValue < 0)
            throw new ArgumentOutOfRangeException(nameof(newValue), 
                "Experience per click cannot be negative");

        ExperiencePerClick = newValue;
        OnExperiencePerClickChanged?.Invoke(newValue);
    }

    public void AddExperience(int experience)
    {
        if (experience < 0)
            throw new ArgumentOutOfRangeException(nameof(experience),
                "Cannot add a negative amount of experience");

        CurrentExperienceLevel += experience;

        OnExperienceChanged?.Invoke(CurrentExperienceLevel);

        CheckForLevelUp();
    }

    public void AddExperiencePerClick()
    {
        AddExperience(ExperiencePerClick);
    }

    public void LevelUp()
    {
        if (CurrentLevel < _maxLevel)
        {
            CurrentLevel++;
            OnLevelChanged?.Invoke(CurrentLevel);
        }
    }

    private void CheckForLevelUp()
    {
        while (CurrentLevel < _maxLevel &&
               CurrentLevel + 1 <= _experienceForLevel.Length &&
               CurrentExperienceLevel >= _experienceForLevel[CurrentLevel])
        {
            CurrentExperienceLevel -= _experienceForLevel[CurrentLevel];
            CurrentLevel++;
            OnLevelChanged?.Invoke(CurrentLevel);
            OnExperienceChanged?.Invoke(CurrentExperienceLevel);
        }
    }

    public void LoadData(GameData data)
    {
        CurrentLevel = data.Level;
        CurrentExperienceLevel = data.ExperienceLevel;
        ExperiencePerClick = data.ExperiencePerClick;

        _experienceForLevel = _saveConfig.ExperienceForLevel;
        _maxLevel = _saveConfig.MaxLevel;

        OnLevelChanged?.Invoke(CurrentLevel);
        OnExperienceChanged?.Invoke(CurrentExperienceLevel);
        OnExperiencePerClickChanged?.Invoke(ExperiencePerClick);
    }

    public void SaveData(ref GameData data)
    {
        data.Level = CurrentLevel;
        data.ExperienceLevel = CurrentExperienceLevel;
        data.ExperiencePerClick = ExperiencePerClick;
    }
}
