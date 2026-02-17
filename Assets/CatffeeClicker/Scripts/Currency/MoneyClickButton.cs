using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(MoneyButtonAnimations))]
public class MoneyClickButton : MonoBehaviour
{
    private Button _moneyClickButton;

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
        _moneyClickButton = GetComponent<Button>();

        _moneyClickButton.onClick.AddListener(OnMoneyButtonClick);
        _moneyClickButton.onClick.AddListener(AddExperienceButton);
    }

    public void OnMoneyButtonClick()
    {
        _moneyViewModel.AddMoneyPerClick();
        _onClicked.OnNext(Unit.Default);
    }

    public void AddExperienceButton()
    {
        _levelViewModel.AddExperience();
    }

    private void OnDisable()
    {
        _moneyClickButton.onClick.RemoveListener(OnMoneyButtonClick);
        _moneyClickButton.onClick.RemoveListener(AddExperienceButton);
    }
}
