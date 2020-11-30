using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Основа режима "Кто больше выбросит"
/// </summary>
public abstract class ThrowMoreModeBase : GameMode
{
    public override event Action OnRestartGame;

    public abstract event Action<int, EntityType> OnDroppedValue;
    /// <summary>
    /// Вызывается при выбросе костей пользователем.
    /// В качестве параметра передается кол-во оставшихся бросков у пользователя
    /// </summary>

    [SerializeField] protected BotEntity _botEntity;



    protected override void InitGameMode()
    {
        base.InitGameMode();

        RestartGame();
        TryBotThrowing();

        GameplayManager.ChangeGameplayState(GameplayState.Started);
    }


    /// <summary>
    /// Вызывается по событию OnDroppedNumber
    /// </summary>
    /// <param name="droppedNumber"></param>
    protected override void OnDroppedNumber(int droppedNumber)
    {
        ApplyDroppedValue(droppedNumber);
        ApplyCompletedThrow();
    }


    protected abstract void TryBotThrowing();


    /// <summary>
    /// Обрабатывается логика, после броска сущности
    /// </summary>
    /// <param name="droppedNumber"></param>
    protected abstract void ApplyDroppedValue(int droppedNumber);


    /// <summary>
    /// Принять завершенность броска
    /// </summary>
    protected virtual void ApplyCompletedThrow()
    {
        DetectedEntityNextThrow(); // Определить сущность следующего броска
    }


    /// <summary>
    /// Определить сущность следующего броска
    /// </summary>
    protected virtual void DetectedEntityNextThrow()
    {
        ChangeCurrentEntity(); // Изменить текущую сущность
    }


    /// <summary>
    /// Завершить игру и определить победителя
    /// </summary>
    public virtual void ApplyEndGame()
    {
        GameplayManager.ChangeGameplayState(GameplayState.Stopped);
    }


    protected override void RestartGame()
    {
        SetRandomCurrentEntity();
    }


    /// <summary>
    /// Устанавливает сущность которая будет бросать кубики рандомно
    /// </summary>
    protected void SetRandomCurrentEntity()
    {
        var entity = Random.Range(0f, 1f) > 0.5f ? EntityType.Player : EntityType.Enemy;
        GameplayManager.ChangeCurrentEntity(entity);
    }


    /// <summary>
    /// Изменяет текущую сущность
    /// Если активен игрок, следующая сущность будет противник и наоборот
    /// </summary>
    protected void ChangeCurrentEntity()
    {
        var entity = GameplayManager.CurrentEntity == EntityType.Player ? EntityType.Enemy : EntityType.Player;
        GameplayManager.ChangeCurrentEntity(entity);
    }
}
