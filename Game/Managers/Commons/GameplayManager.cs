using System;

/// <summary>
/// Менеджер игрового процесса
/// </summary>
public class GameplayManager
{
    #region Gameplay
    public static event Action<GameplayState> OnChangedGameplayState;
    public static GameplayState GameplayState { get; private set; }
    
    public static void ChangeGameplayState(GameplayState gameplayState)
    {
        GameplayState = gameplayState;
        OnChangedGameplayState?.Invoke(GameplayState);
    }
    #endregion


    #region Entity Data
    public static string EnemyUsername = string.Empty;

    public static event Action<EntityType> OnChangedCurrentEntity;
    public static EntityType CurrentEntity { get; private set; } = EntityType.Player;

    public static void ChangeCurrentEntity()
    {
        CurrentEntity = CurrentEntity == EntityType.Enemy ? CurrentEntity = EntityType.Player : CurrentEntity = EntityType.Enemy;
        OnChangedCurrentEntity?.Invoke(CurrentEntity);
    }

    public static void ChangeCurrentEntity(EntityType entityType)
    {
        CurrentEntity = entityType;
        OnChangedCurrentEntity?.Invoke(CurrentEntity);
    }
    #endregion


    #region Game data
    public static GameModeType SelectedGameModeType;
    public static int SettedRate;
    #endregion
}