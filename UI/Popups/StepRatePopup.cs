using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;
using System;

public class StepRatePopup : Popup
{
    private int _currentRateValue;

    [SerializeField] private int _minRateValue;

    [Space(15)]

    [SerializeField] private Text _currentRateValueField;

    private Action<bool> _isSettedRateCallback;


    private void Start()
    {
        _currentRateValue = _minRateValue;
        OnChangeRateValue();
    }


    public void Init(Action<bool> isSettedRateCallback)
    {
        _isSettedRateCallback = isSettedRateCallback;
    }


    public void OnQuickRateButtonPressed(int rateValue)
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);

        _currentRateValue = rateValue;

        OnChangeRateValue();
    }


    public void OnConfirmationButtonPressed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);

        GameMode gameMode = FindObjectOfType<GameMode>();

        if (Mediator.Instance.ResourcesStorage.Chips.Count < _currentRateValue)
        {
            _isSettedRateCallback?.Invoke(false);
            return;
        }

        AppMetricaManager.SendSetRate(_currentRateValue);
        GameplayManager.SettedRate = _currentRateValue;

        _isSettedRateCallback?.Invoke(true);

        Close();
    }


    public void OnCancelButtonPressed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);
        Mediator.Instance.PopupsManager.CreatePopup<LoadingPopup>().LoadingScene("Modes");
    }


    private void OnChangeRateValue()
    {
        _currentRateValueField.text = _currentRateValue.ToString();
    }
}
