using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Описывает статистику игра
/// </summary>
[System.Serializable]
public class UserData
{
    /// <summary>
    /// Уникальный индентификатор пользователя
    /// </summary>
    public string UserID => _userID;
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Username => _username;
    /// <summary>
    /// Промокод партнера, через которого пришел пользователь
    /// </summary>
    public string PromoCode => _promoCode;

    /// <summary>
    /// Количество запусков игры
    /// </summary>
    public int GamesRunning => _gamesRunning;
    /// <summary>
    /// Количество фишек полученных на протяжении всех игр
    /// </summary>
    public int ReceivedChips => _receivedChips;
    /// <summary>
    /// Количество полученных сюрпризов на протяжении всех игр
    /// </summary>
    public int ReceivedSurpriseBoxs => _receivedSurpriseBoxs;

    /// <summary>
    /// Количество выигранных игр
    /// </summary>
    public int GamesWin => _gamesWin;
    /// <summary>
    /// Количество проигранных игр
    /// </summary>
    public int GamesLoss => _gamesLoss;
    /// <summary>
    /// Количество игр с исходом - ничья
    /// </summary>
    public int GamesDraw => _gamesDraw;


    /// <summary>
    /// Суммарное количество запусков игровых режимов
    /// </summary>
    public int ModesRunning => _modesRunning;

    /// <summary>
    /// Количество запусков режима "Ставка на угад"
    /// </summary>
    public int CombinationAtRandomModeLaunch => _combinationAtRandomModeLaunch;
    /// <summary>
    /// Количество запусков режима "Кто больше бросит"
    /// </summary>
    public int ThrowModeOffileModeLaunch => _throwMoreOfflineModeLaunch;
    /// <summary>
    /// Количество запусков режима "Кто больше бросит"
    /// </summary>
    public int ThrowModeOnlineModeLaunch => _throwMoreOnlineModeLaunch;
    /// <summary>
    /// Количество запусков режима "21"
    /// </summary>
    public int TwentyOneModeLaunch => _twentyOneModeLaunch;

    public List<string> PurchasedDices => _purchasedDices;



    [SerializeField] private string _userID;
    [SerializeField] private string _username;
    [SerializeField] private string _promoCode;
    [Space(10)]
    [SerializeField] private int _receivedChips;
    [SerializeField] private int _receivedSurpriseBoxs;
    [Space(10)]
    [SerializeField] private int _gamesRunning;
    [Space(10)]
    [SerializeField] private int _gamesWin;
    [SerializeField] private int _gamesLoss;
    [SerializeField] private int _gamesDraw;
    [Space(10)]
    [SerializeField] private int _modesRunning;
    [SerializeField] private int _combinationAtRandomModeLaunch;
    [SerializeField] private int _throwMoreOfflineModeLaunch;
    [SerializeField] private int _throwMoreOnlineModeLaunch;
    [SerializeField] private int _twentyOneModeLaunch;
    [Space(10)]
    [SerializeField] private List<string> _purchasedDices = new List<string>();



    public UserData()
    {
        _userID = UserIDGenerator.GetUserID();
        _purchasedDices.Add("WB");
    }


    public void InitUsernameAndPromoCode(string username, string promocode)
    {
        _username = username;
        _promoCode = promocode;
    }


    public void AddReceivedResources(ResourceType resourceType, int receivedCount)
    {
        switch(resourceType)
        {
            case ResourceType.Chips: _receivedChips += receivedCount; break;
            case ResourceType.SurpriseBox: _receivedSurpriseBoxs += receivedCount; break;
        }
    }


    /// <summary>
    /// Засчитать запуск приложения
    /// </summary>
    public void GameLaunch()
    {
        _gamesRunning++;
    }


    /// <summary>
    /// Принять исход игры
    /// </summary>
    /// <param name="outcomeState">Исход игры</param>
    public void ApplyOutcomeGame(OutcomeState outcomeState)
    {
        switch (outcomeState)
        {
            case OutcomeState.Win: _gamesWin++; break;
            case OutcomeState.Loss: _gamesLoss++; break;
            case OutcomeState.Draw: _gamesDraw++; break;
        }
    }


    /// <summary>
    /// Засчитать запуск игрового режима
    /// </summary>
    /// <param name="gameModeType">Игровой режим</param>
    public void AddModeLaunch(GameModeType gameModeType)
    {
        _modesRunning++;

        switch (gameModeType)
        {
            case GameModeType.BetAtRandom: _combinationAtRandomModeLaunch++; break;
            case GameModeType.Throw_Offline: _throwMoreOfflineModeLaunch++; break;
            case GameModeType.Throw_Online: _throwMoreOnlineModeLaunch++; break;
            case GameModeType.TwentyOne: _twentyOneModeLaunch++; break;
        }

        AppMetricaManager.SendGameAndModeLaunchCountData();
    }


    public void ApplyPurchasedDice(string diceID)
    {
        if (_purchasedDices.Contains(diceID)) return;

        _purchasedDices.Add(diceID);
    }


    public bool HasPurchasedDice(string diceID)
    {
        return _purchasedDices.Contains(diceID);
    }
}