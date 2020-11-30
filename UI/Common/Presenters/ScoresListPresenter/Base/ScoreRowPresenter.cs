using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public class ScoreRowPresenter : MonoBehaviour
{
    [SerializeField] private Text _scoreField;

    [SerializeField] protected Text _entityUsernameField;



    public virtual void Init(int scoreValue, bool isShowedUsername = false)
    {
        if(scoreValue == 1)
            _scoreField.text = $"{scoreValue} {LeanLocalization.GetTranslationText("Score/OnePoint")}";
        else if(scoreValue > 1 && scoreValue < 5)
            _scoreField.text = $"{scoreValue} {LeanLocalization.GetTranslationText("Score/Points")}";
        else
            _scoreField.text = $"{scoreValue} {LeanLocalization.GetTranslationText("Score/MorePoints")}";
    }
}
