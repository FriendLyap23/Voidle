using R3;
using System;
using Zenject;

public class LevelViewModel : IInitializable, IDisposable
{
    private LevelStorage _levelStorage;

    private CompositeDisposable _disposables = new();

    public readonly ReactiveProperty<string> Level = new();
    public readonly ReactiveProperty<float> Experience = new();
    public readonly ReactiveProperty<string> ExperiencePerClick = new();

    public LevelViewModel(LevelStorage levelStorage)
    {
        _levelStorage = levelStorage;
    }

    public void Initialize()
    {
        _levelStorage.CurrentLevel.Subscribe(LevelChanged).AddTo(_disposables);
        _levelStorage.ExperiencePerClick.Subscribe(ExperiencePerClickChanged).AddTo(_disposables);

        Observable.CombineLatest(
            _levelStorage.CurrentLevel,
            _levelStorage.CurrentExperienceLevel,
                (level, experience) =>
                    {
                        float requiredExperience = _levelStorage.GetExpForCurrentLevel();

                        return experience / requiredExperience;
                    }
                ).Subscribe(progress => Experience.Value = progress)
                .AddTo(_disposables);
    }

    private void LevelChanged(int level)
    {
        Level.Value = level.ToString();
    }
    
    public void AddExperience()
    {
        _levelStorage.AddExperiencePerClick();
    }

    private void ExperiencePerClickChanged(int experience)
    {
        ExperiencePerClick.Value = experience.ToString();
    }

    public void Dispose()
    {
        _disposables.Dispose();

        Level.Dispose();
        Experience.Dispose();
        ExperiencePerClick.Dispose();
    }
}
