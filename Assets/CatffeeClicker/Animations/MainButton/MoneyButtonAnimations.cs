using DG.Tweening;
using R3;
using System;
using UnityEngine;

public class MoneyButtonAnimations : MonoBehaviour
{
    private MoneyClickButton _moneyClickButton;
    private IDisposable;

    private Tween _tapAnimation;
    private Tween _idleAnimation;

    private void OnEnable()
    {
        _moneyClickButton = GetComponent<MoneyClickButton>();
        _disposable = _moneyClickButton.OnClicked.Subscribe(_ => TapAnimation());

        StartIdleAnimation();
    }

    private void StartIdleAnimation() 
    {
        Sequence sequence = DOTween.Sequence();

        _idleAnimation = sequence.Append(_moneyClickButton.transform.DORotate(new Vector3(0, 0, -10), 2f).SetEase(Ease.InOutSine))
            .Append(_moneyClickButton.transform.DORotate(new Vector3(0, 0, 10), 2f).SetEase(Ease.InOutSine))
            .SetLoops(-1, LoopType.Yoyo);
    }


    private void TapAnimation() 
    {
        _tapAnimation?.Kill();

        Sequence sequence = DOTween.Sequence();

        _tapAnimation = sequence.Append(_moneyClickButton.transform.DOScale(1.5f, 0.5f).From(2).SetEase(Ease.OutSine))
            .Append(_moneyClickButton.transform.DOScale(2, 0.5f).From(1.5f).SetEase(Ease.OutSine));
    }

    private void OnDisable()
    {
        _idleAnimation.Kill();
        _tapAnimation.Kill();

        _disposable.Dispose();
    }
}
