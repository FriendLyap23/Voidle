using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MoneyClickButton : MonoBehaviour
{
    [SerializeField] private Button _moneyPerClickButton;

    private MoneyViewModel _moneyViewModel;
    private LevelViewModel _levelViewModel;

    [Inject]
    private void Constructor(MoneyViewModel moneyViewModel, LevelViewModel levelViewModel)
    {
        _moneyViewModel = moneyViewModel;
        _levelViewModel = levelViewModel;
    }

    private void Start()
    {
        _moneyPerClickButton.onClick.AddListener(OnMoneyButtonClick);
        _moneyPerClickButton.onClick.AddListener(AddExperienceButton);
    }

    public void OnMoneyButtonClick()
    {
        _moneyViewModel.AddMoneyPerClick();
    }

    public void AddExperienceButton()
    {
        _levelViewModel.AddExperience();
    }

    private void OnDestroy()
    {
        _moneyPerClickButton.onClick.RemoveListener(OnMoneyButtonClick);
        _moneyPerClickButton.onClick.RemoveListener(AddExperienceButton);
    }
}
