using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelView;
    [SerializeField] private Image _experienceBar;

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
            .Subscribe(exp =>
            {
                _experienceBar.fillAmount = exp;
            }).AddTo(_disposables);
    }

    private void OnDestroy()
    {
        _disposables.Dispose();
    }
}
