using R3;
using System;
using Zenject;

public class LevelStorage : IDataPersistence, IDisposable
{
    private SaveConfig _saveConfig;

    private int _maxLevel;
    private int[] _experienceForLevel;

    private ReactiveProperty<int> _currentLevel = new(0);
    private ReactiveProperty<float> _currentExperienceLevel = new(0);
    private ReactiveProperty<int> _experiencePerClick = new(0);

    public ReadOnlyReactiveProperty<int> CurrentLevel => _currentLevel;
    public ReadOnlyReactiveProperty<float> CurrentExperienceLevel => _currentExperienceLevel;
    public ReadOnlyReactiveProperty<int> ExperiencePerClick => _experiencePerClick;

    [Inject]
    private void Constructor(SaveConfig saveConfig) 
    {
        _saveConfig = saveConfig;
    }

    public float GetExpForCurrentLevel() 
    {
        float exp = _experienceForLevel[_currentLevel.Value];
        return exp;
    }

    public void ChangeExperiencePerClick(int newValue) 
    {
        if (newValue < 0)
            throw new ArgumentOutOfRangeException(nameof(newValue), 
                "Experience per click cannot be negative");

        _experiencePerClick.Value = newValue;
    }

    public void AddExperience(float experience)
    {
        if (experience < 0)
            throw new ArgumentOutOfRangeException(nameof(experience),
                "Cannot add a negative amount of experience");

        _currentExperienceLevel.Value += experience;

        CheckForLevelUp();
    }

    public void AddExperiencePerClick()
    {
        AddExperience(_experiencePerClick.Value);
    }

    public void LevelUp()
    {
        if (_currentLevel.Value < _maxLevel)
        {
            _currentLevel.Value++;
        }
    }

    private void CheckForLevelUp()
    {
        while (_currentLevel.Value < _maxLevel &&
               _currentLevel.Value + 1 <= _experienceForLevel.Length &&
               _currentExperienceLevel.Value >= _experienceForLevel[_currentLevel.Value])
        {
            _currentExperienceLevel.Value -= _experienceForLevel[_currentLevel.Value];
            _currentLevel.Value++;
        }
    }

    public void LoadData(GameData data)
    {
        _currentLevel.Value = data.Level;
        _currentExperienceLevel.Value = data.ExperienceLevel;
        _experiencePerClick.Value = data.ExperiencePerClick;

        _experienceForLevel = _saveConfig.ExperienceForLevel;
        _maxLevel = _saveConfig.MaxLevel;
    }

    public void SaveData(ref GameData data)
    {
        data.Level = _currentLevel.Value;
        data.ExperienceLevel = _currentExperienceLevel.Value;
        data.ExperiencePerClick = _experiencePerClick.Value;
    }

    public void Dispose()
    {
        _currentLevel.Dispose();
        _currentExperienceLevel.Dispose();
        _experiencePerClick.Dispose();
    }
}
