using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Описывает режим игры
/// </summary>
public abstract class GameMode : MonoBehaviour
{
    public abstract event Action OnRestartGame;
    
    protected BoneGenerator _boneGenerator;



    private void Awake()
    {
        _boneGenerator = FindObjectOfType<BoneGenerator>();
    }


    protected virtual void Start()
    {
        OpenSetRatePopup();
    }


    protected virtual void OnEnable()
    {
        _boneGenerator.OnDroppedNumber += OnDroppedNumber;
    }


    protected virtual void OnDisable()
    {
        _boneGenerator.OnDroppedNumber -= OnDroppedNumber;
    }


    public bool TryStartGame(int currentRate)
    {
        if (!Mediator.Instance.ResourcesStorage.Chips.TrySpendResources(currentRate))
        {
            AlertPopup alertPopup = Mediator.Instance.PopupsManager.CreatePopup<AlertPopup>();
            alertPopup.Init(AlertType.Lack_Chips, "Установите ставку меньше!");

            return false;
        }

        return true;
    }


    /// <summary>
    /// Сыграть еще раз
    /// </summary>
    /// <param name="isSetNewRate">Будет ли новая ставка</param>
    public virtual void PlayAgain(bool isSetNewRate)
    {
        if (isSetNewRate)
        {
            OpenSetRatePopup();
        }
        else
        {
            if (TryStartGame(GameplayManager.SettedRate))
                InitGameMode();
        }
    }


    public virtual void OnExitGameFromPausePopup()
    {

    }


    /// <summary>
    /// Открыть popup установки ставки
    /// </summary>
    protected virtual void OpenSetRatePopup()
    {
        Mediator.Instance.PopupsManager.CreatePopup<RatePopup>().OnClose.AddListener(InitGameMode);
    }


    /// <summary>
    /// Открыть popup победы игрока
    /// </summary>
    protected virtual void ApplyPlayerWin()
    {
        int winChips = Mathf.RoundToInt(GameplayManager.SettedRate * Mediator.Instance.GameConfig.GetCoefficient());

        Mediator.Instance.ResourcesStorage.Chips.AddResources(winChips);

        Mediator.Instance.CustomStatisticsManager.UserData.ApplyOutcomeGame(OutcomeState.Win);
        Mediator.Instance.PopupsManager.CreatePopup<WinPopup>().ShowData(winChips);
    }


    /// <summary>
    /// Открыть popup проигрыша
    /// </summary>
    protected virtual void ApplyPlayerLoss()
    {
        Mediator.Instance.CustomStatisticsManager.UserData.ApplyOutcomeGame(OutcomeState.Loss);
        Mediator.Instance.PopupsManager.CreatePopup<LossPopup>().ShowData(GameplayManager.SettedRate);
    }


    /// <summary>
    /// Открыть popup ничьи
    /// </summary>
    protected virtual void ApplyDraw()
    {
        Mediator.Instance.ResourcesStorage.Chips.AddResources(GameplayManager.SettedRate);

        Mediator.Instance.CustomStatisticsManager.UserData.ApplyOutcomeGame(OutcomeState.Draw);
        Mediator.Instance.PopupsManager.CreatePopup<DrawPopup>().ShowData(GameplayManager.SettedRate);
    }


    /// <summary>
    /// Инициализация игрового режима
    /// Вызывается, после установки ставки
    /// </summary>
    protected virtual void InitGameMode()
    {
        Mediator.Instance.CustomStatisticsManager.UserData.AddModeLaunch(GameplayManager.SelectedGameModeType);
    }


    /// <summary>
    /// Вызывается при определении выброшенных очков
    /// </summary>
    /// <param name="droppedNumber"></param>
    protected virtual void OnDroppedNumber(int droppedNumber)
    {
        GameplayManager.ChangeGameplayState(GameplayState.Stopped);
    }


    /// <summary>
    /// Выполнить инструкцию с задержкой
    /// </summary>
    /// <param name="action">Инструкция</param>
    /// <param name="delay">Задержка</param>
    /// <returns></returns>
    protected IEnumerator ActionWithDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }


    protected abstract void RestartGame();
}
