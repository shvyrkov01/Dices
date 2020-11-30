using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;
using System;

public class ModeButton : HandlerButtonByEnumType<GameModeType>
{
    public OnClickHandlerByType OnClickHandler = new OnClickHandlerByType();

    [SerializeField] private Text _energyRequiredField;
    [SerializeField] private Text _minRateField;



    protected override void Start()
    {
        base.Start();

        Init();
    }


    private void Init()
    {
        var gameModeSettings = Mediator.Instance.GameConfig.GameModeSettings.Find(mode => mode.GameModeType == _enumType);

        _energyRequiredField.text = $"-{gameModeSettings.EnergyRequired}";

        string minRateText = $"{LeanLocalization.GetTranslationText("MinRate")} {gameModeSettings.MinRate}";
        string minRateFormattedText = minRateText.Replace('\n', ' ');
        _minRateField.text = minRateFormattedText;
    }


    protected override void OnClickHandle()
    {
        OnClickHandler?.Invoke(_enumType);
    }


    [System.Serializable] public class OnClickHandlerByType : UnityEvent<GameModeType> { }
}
