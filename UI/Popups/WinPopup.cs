using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public class WinPopup : CompletedGamePopup
{
    [SerializeField] private Text _coefficientField;



    protected override void ShowChangeChips(int chips)
    {
        _chipsField.text = $"+{chips}";

        _coefficientField.text = Mediator.Instance.GameConfig.GetCoefficient() > 0 ? $"{LeanLocalization.GetTranslationText("CoefficientWin")} {Mediator.Instance.GameConfig.GetCoefficient()}x" : string.Empty;
    }
}
