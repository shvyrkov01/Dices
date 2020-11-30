using UnityEngine;
using System;

[RequireComponent(typeof(TwentyOneModePresenter))]
public class TwentyOneMode : GameMode
{
    /// <summary>
    /// Вызывается, после получения выброшенных очков.
    /// 1args - Суммарное количество выброшенных очков.
    /// 2args - Выброшенное количество очков.
    /// </summary>
    public event Action<int, int> OnDroppedNumberEvent;

    public override event Action OnRestartGame;

    public int Score { get; private set; }
    


    protected override void InitGameMode()
    {
        base.InitGameMode();

        RestartGame();

        GameplayManager.ChangeGameplayState(GameplayState.Started);
    }


    protected override void OnDroppedNumber(int droppedNumber)
    {
        Score += droppedNumber;

        OnDroppedNumberEvent?.Invoke(Score, droppedNumber);

        CheckCountScore();
    }


    /// <summary>
    /// Проверить суммарное количество выброшенных очков
    /// </summary>
    private void CheckCountScore()
    {
        if (Score < 21) return;

        GameplayManager.ChangeGameplayState(GameplayState.Stopped);

        if (Score == 21)
            ApplyPlayerWin();
        else if(Score > 21)
            ApplyPlayerLoss();
    }


    protected override void RestartGame()
    {
        Score = 0;
        OnRestartGame?.Invoke();
    }
}
