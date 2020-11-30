using System;
using UnityEngine;

public class ThrowMoreOfflineMode : ThrowMoreModeBase
{
    public override event Action OnRestartGame;
    public override event Action<int, EntityType> OnDroppedValue;
    public event Action<int> OnChangedNumberThrows;


    /// <summary>
    /// Всего бросков на игру
    /// </summary>
    [SerializeField] private int _numberThrows;
    /// <summary>
    /// Осталось бросков
    /// </summary>
    private int _throwsLeft;
    /// <summary>
    /// Набранные очки пользователя
    /// </summary>
    public int PlayerScore { get; protected set; }
    /// <summary>
    /// Набранные очки бота
    /// </summary>
    public int EnemyScore { get; protected set; }



    protected override void InitGameMode()
    {
        base.InitGameMode();
    }


    protected override void OnDroppedNumber(int droppedNumber)
    {
        OnDroppedValue?.Invoke(droppedNumber, GameplayManager.CurrentEntity);
        base.OnDroppedNumber(droppedNumber);
    }


    /// <summary>
    /// Вызывается после получения выброшенных очков
    /// </summary>
    protected override void ApplyCompletedThrow()
    {
        _throwsLeft--;

        if (GameplayManager.CurrentEntity == EntityType.Player)
            OnChangedNumberThrows?.Invoke(_throwsLeft / 2);

        base.ApplyCompletedThrow();
    }


    /// <summary>
    /// Определить сущность следующего броска
    /// </summary>
    protected override void DetectedEntityNextThrow()
    {
        base.DetectedEntityNextThrow();

        if (_throwsLeft > 0)
            TryBotThrowing();
        else
            ApplyEndGame();
    }


    public override void ApplyEndGame()
    {
        base.ApplyEndGame();

        var scorePopup = Mediator.Instance.PopupsManager.CreatePopup<ScorePopup>();
        scorePopup.Init(PlayerScore, EnemyScore);
        scorePopup.OnClose.AddListener(() =>
        {
            if (PlayerScore > EnemyScore)
                ApplyPlayerWin();
            else if (PlayerScore < EnemyScore)
                ApplyPlayerLoss();
            else
                ApplyDraw();
        });
    }


    /// <summary>
    /// Определяет какой сущности засчитать выпавшие очки
    /// </summary>
    /// <param name="droppedValue"></param>
    protected override void ApplyDroppedValue(int droppedValue)
    {
        if (GameplayManager.CurrentEntity == EntityType.Player)
        {
            PlayerScore += droppedValue;
            GameplayManager.ChangeGameplayState(GameplayState.Stopped);
        }
        else
        {
            EnemyScore += droppedValue;
            GameplayManager.ChangeGameplayState(GameplayState.Started);
        }
    }


    /// <summary>
    /// Попробовать выбросить кости за бота
    /// </summary>
    /// <returns></returns>
    protected override void TryBotThrowing()
    {
        if (_throwsLeft > 0)
        {
            if (GameplayManager.CurrentEntity == EntityType.Enemy)
            {
                _botEntity.Throw();
            }
        }
    }



    protected override void RestartGame()
    {
        base.RestartGame();

        OnRestartGame?.Invoke();

        _throwsLeft = _numberThrows;

        PlayerScore = 0;
        EnemyScore = 0;

        OnChangedNumberThrows?.Invoke(_throwsLeft / 2);
    }
}