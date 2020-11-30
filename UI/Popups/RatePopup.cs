using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public class RatePopup : Popup
{
    private int _currentRateValue;
    
    [SerializeField] private int _variableValue;
    [SerializeField] private int _minRateValue;
    [SerializeField] private int _maxRateValue;

    [Space(15)]

    [SerializeField] private Text _currentRateValueField;
    [SerializeField] private Text _coefficientField;
    [SerializeField] private Text _winChipsCountField;

    [Space(15)]

    [SerializeField] private LeanToken _coefficientToken;



    private void Start()
    {
        _currentRateValue = _minRateValue;
        OnChangeRateValue();

        _coefficientToken.Value = $"{Mediator.Instance.GameConfig.GetCoefficient()}";
    }


    public void OnMinusButtonPresed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);

        _currentRateValue = Mathf.Clamp(_currentRateValue - _variableValue, _minRateValue, _maxRateValue);

        OnChangeRateValue();
    }


    public void OnPlusButtonPressed() // TODO: Сделать проверку, хватает ли фишек у пользователя на ставку
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);

        _currentRateValue = Mathf.Clamp(_currentRateValue + _variableValue, _minRateValue, _maxRateValue);

        OnChangeRateValue();
    }


    public void OnQuickRateButtonPressed(int rateValue)
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);

        _currentRateValue = Mathf.Clamp(rateValue, _minRateValue, _maxRateValue);

        OnChangeRateValue();
    }


    public void OnConfirmationButtonPressed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);

        GameMode gameMode = FindObjectOfType<GameMode>();

        if(!gameMode.TryStartGame(_currentRateValue))
            return;

        AppMetricaManager.SendSetRate(_currentRateValue);
        GameplayManager.SettedRate = _currentRateValue;
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
            
        _winChipsCountField.text = Mathf.RoundToInt(_currentRateValue * Mediator.Instance.GameConfig.GetCoefficient()).ToString();
    }
}
