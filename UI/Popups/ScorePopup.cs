using UnityEngine;
using UnityEngine.UI;

public class ScorePopup : Popup
{
    [SerializeField] private Text _playerScoreField;
    [SerializeField] private Text _enemyScoreField;



    public void Init(int playerScore, int enemyScore)
    {
        _playerScoreField.text = playerScore.ToString();
        _enemyScoreField.text = enemyScore.ToString();
    }
}
