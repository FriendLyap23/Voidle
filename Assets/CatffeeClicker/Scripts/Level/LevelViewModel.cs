using R3;
using System;
using Zenject;

public class LevelViewModel : IInitializable, IDisposable
{
    private LevelStorage _levelStorage;

    public readonly ReactiveProperty<string> Level = new();
    public readonly ReactiveProperty<string> Experience = new();
    public readonly ReactiveProperty<string> ExperiencePerClick = new();

    public LevelViewModel(LevelStorage levelStorage)
    {
        _levelStorage = levelStorage;
    }

    public void Initialize()
    {
        OnLevelChanged(_levelStorage.CurrentLevel);
        OnExperienceChanged(_levelStorage.CurrentExperienceLevel);
        OnExperiencePerClickChanged(_levelStorage.ExperiencePerClick);

        _levelStorage.OnLevelChanged += OnLevelChanged;
        _levelStorage.OnExperienceChanged += OnExperienceChanged;
        _levelStorage.OnExperiencePerClickChanged += OnExperiencePerClickChanged;
    }

    public void AddExperience() 
    {
        _levelStorage.AddExperiencePerClick();
    }

    private void OnExperienceChanged(int experience)
    {
        Experience.Value = experience.ToString();
    }

    private void OnExperiencePerClickChanged(int experience)
    {
        ExperiencePerClick.Value = experience.ToString();
    }

    private void OnLevelChanged(int level) 
    {
        Level.Value = level.ToString();
    }

    public void Dispose()
    {
        _levelStorage.OnLevelChanged -= OnLevelChanged;
        _levelStorage.OnExperienceChanged -= OnExperienceChanged;

        Level.Dispose();
        Experience.Dispose();
    }
}
