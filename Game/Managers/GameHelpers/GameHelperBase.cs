using UnityEngine;
using System.Collections;

/// <summary>
/// Основа игрового помощника
/// </summary>
public class GameHelperBase : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animationCurve;
    [Space(10)]
    [SerializeField] private bool _hasTargetValue;

    private GameMode _gameMode;



    private void Start()
    {
        _gameMode = FindObjectOfType<GameMode>();
    }


    public int GetTargetDroppedValue()
    {
        if (GameplayManager.CurrentEntity == EntityType.Enemy) return 0;


        if (Mediator.Instance.CustomStatisticsManager.UserData.GamesWin < Mediator.Instance.GameConfig.RequiredWinsCount)
        {
            return DetermineWinValue();
        }
        else
        {
            if(GameplayManager.SettedRate >= Mediator.Instance.GameConfig.LossBet)
            {
                return DetermineLossValue();
            }
        }

        return 0;
    }


    /// <summary>
    /// Определить нужное значение для выигрыша
    /// </summary>
    /// <returns></returns>
    private int DetermineWinValue()
    {
        int targetValue = 0;

        switch (GameplayManager.SelectedGameModeType)
        {
            case GameModeType.BetAtRandom:
                targetValue = ((CombinationAtRandomMode)_gameMode).SelectedBoneValue;
            break;

            case GameModeType.Throw_Offline:
                int differentValues = ((ThrowMoreOfflineMode)_gameMode).PlayerScore - ((ThrowMoreOfflineMode)_gameMode).EnemyScore;
                targetValue = differentValues <= 6 ? Mathf.Clamp(Mathf.Abs(differentValues) + Random.Range(1, 6), 1, 6) : 6;
            break;

            case GameModeType.Throw_Online:

            break;

            case GameModeType.TwentyOne: 
                if(((TwentyOneMode)_gameMode).Score >= 15)
                    targetValue = Mathf.Abs(((TwentyOneMode)_gameMode).Score - 21);
            break;
        }

        return targetValue;
    }


    private int DetermineLossValue()
    {
        int targetValue = 0;

        switch (GameplayManager.SelectedGameModeType)
        {
            case GameModeType.BetAtRandom:
                targetValue = GetRandomNumberWithExcluding(((CombinationAtRandomMode)_gameMode).SelectedBoneValue);
            break;

            case GameModeType.Throw_Offline:
                int differentValues = ((ThrowMoreOfflineMode)_gameMode).PlayerScore - ((ThrowMoreOfflineMode)_gameMode).EnemyScore;
                targetValue = differentValues <= 6 ? Mathf.Clamp(Mathf.Abs(differentValues) + Random.Range(1, 6), 1, 6) : 6;
            break;

            case GameModeType.Throw_Online:

            break;

            case GameModeType.TwentyOne:
                if (((TwentyOneMode)_gameMode).Score >= 15)
                    targetValue = GetRandomNumberWithExcluding(Mathf.Abs(((TwentyOneMode)_gameMode).Score - 21));
            break;
        }

        return targetValue;
    }


    /// <summary>
    /// Получить рандомное число, исключая определенное
    /// </summary>
    /// <param name="exceptNumber">Число которое нужно исключить</param>
    /// <returns></returns>
    private int GetRandomNumberWithExcluding(int exceptNumber)
    {
        int resultNumber;

        do
        {
            resultNumber = Random.Range(1, 7);
        }
        while (resultNumber == exceptNumber);

        return resultNumber;
    }
}