using System;
using System.Collections;
using UnityEngine;

public class ThrowMoreOnlineMode : ThrowMoreModeBase
{
    public override event Action<int, EntityType> OnDroppedValue;
    public event Action<int> OnChangedBank;
    public event Action OnResetThrowsPresenter;

    public int Bank { get; private set; }

    private int _numberThrows;

    private int _playerLastThrowValue;
    private int _enemyLastThrowValue;

    private int _chipsRate;

    [Space(10)]

    [SerializeField] private float _smoothSpeed;

    private Coroutine _smoothChangeBank;



    protected override void Start()
    {
        InitGameMode();
        StartCoroutine(SettingEnemyUsername());
    }


    protected override void InitGameMode()
    {
        base.InitGameMode();

        _chipsRate = GameplayManager.SettedRate;
        _smoothSpeed = Mathf.Clamp(_chipsRate, 100, 5000);
    }


    protected override void OnDroppedNumber(int droppedNumber)
    {
        OnDroppedValue?.Invoke(droppedNumber, GameplayManager.CurrentEntity);
        base.OnDroppedNumber(droppedNumber);
    }


    protected override void TryBotThrowing()
    {
        if (GameplayManager.CurrentEntity != EntityType.Enemy) return;
        
        _botEntity.Throw();
    }


    protected override void ApplyDroppedValue(int droppedNumber)
    {
        if (GameplayManager.CurrentEntity == EntityType.Player)
            _playerLastThrowValue = droppedNumber;
        else
            _enemyLastThrowValue = droppedNumber;
    }


    protected override void ApplyCompletedThrow()
    {
        base.ApplyCompletedThrow();
        _numberThrows++;
        CalculateBank();
    }


    protected override void DetectedEntityNextThrow()
    {
        base.DetectedEntityNextThrow();

        if (GameplayManager.CurrentEntity == EntityType.Enemy)
            TryBotThrowing();
    }


    public override void ApplyEndGame()
    {
        base.ApplyEndGame();

        if (Bank > 0)
            ApplyPlayerWin();
        else if (Bank < 0)
            ApplyPlayerLoss();
        else
            ApplyDraw();
    }


    private void CalculateBank()
    {
        if (_numberThrows % 2 != 0) return;

        int targetBank = Bank;

        if (_playerLastThrowValue > _enemyLastThrowValue)
            targetBank += _chipsRate;
        else if (_playerLastThrowValue < _enemyLastThrowValue)
            targetBank -= _chipsRate;

        if (!ReferenceEquals(_smoothChangeBank, null))
            StopCoroutine(_smoothChangeBank);

        _smoothChangeBank = StartCoroutine(ShowSmoothChangeBank(targetBank));
    }


    private IEnumerator ShowSmoothChangeBank(int targetBank)
    {
        while (Bank != targetBank)
        {
            yield return new WaitForEndOfFrame();

            Bank = (int)Mathf.MoveTowards(Bank, targetBank, _smoothSpeed * Time.fixedDeltaTime);

            OnChangedBank?.Invoke(Bank);
        }

        OnResetThrowsPresenter?.Invoke();

        _smoothChangeBank = null;
    }


    protected override void RestartGame()
    {
        base.RestartGame();

        Bank = 0;
        _playerLastThrowValue = 0;
        _enemyLastThrowValue = 0;
        _numberThrows = 0;

        OnResetThrowsPresenter?.Invoke();
        OnChangedBank?.Invoke(Bank);
    }


    protected override void ApplyPlayerWin()
    {
        Mediator.Instance.ResourcesStorage.Chips.AddResources(Bank);

        Mediator.Instance.CustomStatisticsManager.UserData.ApplyOutcomeGame(OutcomeState.Win);
        Mediator.Instance.PopupsManager.CreatePopup<WinPopup>().ShowData(Bank, true, false);
    }


    protected override void ApplyPlayerLoss()
    {
        Mediator.Instance.ResourcesStorage.Chips.CollectFine(Bank);
        
        Mediator.Instance.CustomStatisticsManager.UserData.ApplyOutcomeGame(OutcomeState.Loss);
        Mediator.Instance.PopupsManager.CreatePopup<LossPopup>().ShowData(Bank, true, false);
    }


    protected override void ApplyDraw()
    {
        Mediator.Instance.CustomStatisticsManager.UserData.ApplyOutcomeGame(OutcomeState.Draw);
        Mediator.Instance.PopupsManager.CreatePopup<DrawPopup>().ShowData(0, true, false);
    }


    public override void PlayAgain(bool isSetNewRate)
    {
        DataExchangeBetweenScenes.SetData(new ContainerData(ExchangeDataType.Open_Game_Mode, GameModeType.Throw_Online));
        Mediator.Instance.PopupsManager.CreatePopup<LoadingPopup>().LoadingScene("Modes");
    }


    public override void OnExitGameFromPausePopup()
    {
        base.OnExitGameFromPausePopup();
        
        if(Bank > 0)
            Mediator.Instance.ResourcesStorage.Chips.CollectFine(Bank);
    }


    private IEnumerator SettingEnemyUsername()
    {
        string enemyName;

        do
        {
            enemyName = string.Empty;

            Mediator.Instance.DatabaseExchanger.TryGetRandomUsername((state, header) =>
            {
                enemyName = header;
            });

            yield return new WaitUntil(() => enemyName != string.Empty);
        }
        while (enemyName == string.Empty || enemyName == Mediator.Instance.CustomStatisticsManager.UserData.Username);

        GameplayManager.EnemyUsername = enemyName;
    }
}