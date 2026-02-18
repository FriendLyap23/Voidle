using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(MainButtonAnimations))]
public class MainButton : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickSound;

    private Button _mainButton;

    private readonly Subject<Unit> _onClicked = new Subject<Unit>();

    private MoneyViewModel _moneyViewModel;
    private LevelViewModel _levelViewModel;

    public Observable<Unit> OnClicked => _onClicked;

    [Inject]
    private void Constructor(MoneyViewModel moneyViewModel, LevelViewModel levelViewModel)
    {
        _moneyViewModel = moneyViewModel;
        _levelViewModel = levelViewModel;
    }

    private void OnEnable()
    {
        _mainButton = GetComponent<Button>();

        _mainButton.onClick.AddListener(OnMoneyButtonClick);
        _mainButton.onClick.AddListener(AddExperienceButton);
    }

    public void OnMoneyButtonClick()
    {
        _moneyViewModel.AddMoneyPerClick();
        _onClicked.OnNext(Unit.Default);

        _audioSource.PlayOneShot(_clickSound);
    }

    public void AddExperienceButton()
    {
        _levelViewModel.AddExperience();
    }

    private void OnDisable()
    {
        _mainButton.onClick.RemoveListener(OnMoneyButtonClick);
        _mainButton.onClick.RemoveListener(AddExperienceButton);
    }
}
