using UnityEngine;
using UnityEngine.UI;

public class TwentyOneModePresenter : MonoBehaviour
{
    [SerializeField] private Text _scoreField;

    [SerializeField] private ScoreList _scoreList;



    private void OnEnable()
    {
        TwentyOneMode twentyOneMode = GetComponent<TwentyOneMode>();

        twentyOneMode.OnDroppedNumberEvent += OnDroppedNumber;
        twentyOneMode.OnRestartGame += OnRestartGame;
    }


    private void OnDisable()
    {
        TwentyOneMode twentyOneMode = GetComponent<TwentyOneMode>();

        twentyOneMode.OnDroppedNumberEvent -= OnDroppedNumber;
        twentyOneMode.OnRestartGame -= OnRestartGame;
    }


    private void OnDroppedNumber(int countScore, int droppedNumber)
    {
        ShowScore(countScore);

        if (droppedNumber > 0)
            ShowThrowScore(droppedNumber);
    }


    /// <summary>
    /// Отобразить суммарное количество очков
    /// </summary>
    /// <param name="scoreValue"></param>
    private void ShowScore(int scoreValue)
    {
        _scoreField.text = scoreValue.ToString();
    }


    /// <summary>
    /// Отобразить выбрашенное количество очков
    /// </summary>
    /// <param name="scoreValue"></param>
    private void ShowThrowScore(int scoreValue)
    {
        _scoreList.ShowThrowScore(scoreValue);
    }


    private void OnRestartGame()
    {
        ShowScore(0);
        _scoreList.ClearList();
    }
}
