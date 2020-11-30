using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameConfig : ScriptableObject
{
    public RangeValueInt RangeChipValue;
    public RangeValueInt RangeEnergyValue;

    /// <summary>
    /// Максимальное количество энергии
    /// </summary>
    public int MaxAmountEnergy;
    /// <summary>
    /// Время восстановления энергии
    /// </summary>
    public int EnergyRecoveryTime;
    /// <summary>
    /// Количество запусков игры для получения сюрприза
    /// </summary>
    public int GameLaunchCountForReceiptSurprise;
    /// <summary>
    /// Количество запусков игры для открытия popup подписки
    /// </summary>
    public int GameLaunchCountForOpenSubscriptionPopup;

    /// <summary>
    /// Количество нужных выигрышей
    /// </summary>
    public int RequiredWinsCount;
    /// <summary>
    /// Ставка при которой пользователь должен проиграть
    /// </summary>
    public int LossBet;

    /// <summary>
    /// Награда за промокод
    /// </summary>
    public int PromoCodeReward;

    /// <summary>
    /// Шанс выпадения энергии в коробке сюрпризе
    /// От 0 до 1
    /// </summary>
    public float EnergyDropChance;

    public bool IsDebug = false;

    public string TermsOfUseURL;
    public string PrivacyPolicyURL;


    public List<GameModeSettings> GameModeSettings = new List<GameModeSettings>();
    public List<DiceParameter> DiceParameters = new List<DiceParameter>();

    public List<string> PromoCodes = new List<string>();



    public float GetCoefficient()
    {
        return GameModeSettings.Find(mode => mode.GameModeType == GameplayManager.SelectedGameModeType).WinCoefficient;
    }
}
