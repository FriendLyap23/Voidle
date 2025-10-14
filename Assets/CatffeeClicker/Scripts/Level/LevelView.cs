using R3;
using TMPro;
using UnityEngine;
using Zenject;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelView;
    [SerializeField] private TMP_Text _experienceView;
    [SerializeField] private TMP_Text _experiencePerClickView;

    private LevelViewModel _levelViewModel;
    private CompositeDisposable _disposables = new();

    [Inject]
    private void Constructor(LevelViewModel levelViewModel)
    {
        _levelViewModel = levelViewModel;
    }

    private void Start()
    {
        _levelViewModel.Level
            .Subscribe(level => _levelView.text = level)
            .AddTo(_disposables);

        _levelViewModel.Experience
            .Subscribe(experience => _experienceView.text = experience)
            .AddTo(_disposables);

        _levelViewModel.ExperiencePerClick
            .Subscribe(experiencePerClick => _experiencePerClickView.text = experiencePerClick)
            .AddTo(_disposables);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
