using UnityEngine;
using System;

public class ModesScene : MonoBehaviour
{
    private void Start()
    {
        CheckExchangedData();
    }


    public void OnModeButtonPressed(GameModeType gameModeType)
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);

        if(Mediator.Instance.ResourcesStorage.Chips.Count < 50)
        {
            Mediator.Instance.PopupsManager.CreatePopup<AlertPopup>().Init(AlertType.Lack_Chips);
            return;
        }

        IsCanContinue(gameModeType, isCanContinue => 
        {
            if (!isCanContinue)
            {
                ShowInternetException();
                return;
            }

            if (!TrySpendEnergy(gameModeType))
                return;


            GameplayManager.SelectedGameModeType = gameModeType;
            Mediator.Instance.PopupsManager.CreatePopup<LoadingPopup>().LoadingScene("Game");
        });
    }


    public void OnExitToMenuButtonPressed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);
        Mediator.Instance.PopupsManager.CreatePopup<LoadingPopup>().LoadingScene("Home");
    }


    public void OnHelpButtonPressed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);
        Mediator.Instance.PopupsManager.CreatePopup<HelpPopup>();
    }


    /// <summary>
    /// Можно ли продолжить игру в выбранном игровом режиме
    /// </summary>
    /// <param name="gameModeType">Выбранный игровой режим</param>
    /// <param name="isCanContinueHandler">callback возможности продолжения</param>
    private void IsCanContinue(GameModeType gameModeType, Action<bool> isCanContinueHandler)
    {
        if(Mediator.Instance.GameConfig.IsDebug)
        {
            isCanContinueHandler?.Invoke(true);
            return;
        }

        if (gameModeType != GameModeType.Throw_Online) // Если игра не онлайн, то разрешаем продолжить без проверки подключения к интернету
        {
            isCanContinueHandler?.Invoke(true);
            return;
        }
        
        //Иначе проверяем подключение
        Mediator.Instance.PopupsManager.CreatePopup<CheckInternetPopup>().CheckConnectionState((state) =>
        {
            if (state) // Если подключение к интернету есть, открываем popup выбора ставки на которую готов играть пользователь
            {
                // Прокидываем callback, который вернет состояние установленной ставки
                Mediator.Instance.PopupsManager.CreatePopup<StepRatePopup>().Init(isSettedRate =>
                {
                    //Если ставка установлена, открываем popup поиска игрока
                    if(isSettedRate)
                        Mediator.Instance.PopupsManager.CreatePopup<PlayerSearchPopup>().Init(() => isCanContinueHandler?.Invoke(true));
                });
            }
            else
                isCanContinueHandler?.Invoke(false);
        });
    }


    /// <summary>
    /// Отобразить всплывающее окно об ошибке подключения к интернету
    /// </summary>
    private void ShowInternetException()
    {
        Mediator.Instance.PopupsManager.CreatePopup<InternetExceptionPopup>();
    }


    private void CheckExchangedData()
    {
        var needPopup = DataExchangeBetweenScenes.TryGetDataByKey(ExchangeDataType.Show_Alert_Popup);

        if (!ReferenceEquals(needPopup, null))
            Mediator.Instance.PopupsManager.CreatePopup<AlertPopup>().Init((AlertType)needPopup);


        var needGameMode = DataExchangeBetweenScenes.TryGetDataByKey(ExchangeDataType.Open_Game_Mode);

        if(!ReferenceEquals(needGameMode, null))
            OnModeButtonPressed((GameModeType)needGameMode);
    }


    private bool TrySpendEnergy(GameModeType gameModeType)
    {
        int energyRequired = Mediator.Instance.GameConfig.GameModeSettings.Find(mode => mode.GameModeType == gameModeType).EnergyRequired;
        bool isSuccessSpend = Mediator.Instance.ResourcesStorage.Energy.TrySpendResources(energyRequired);

        if (!isSuccessSpend)
            Mediator.Instance.PopupsManager.CreatePopup<AlertPopup>().Init(AlertType.Lack_Energy);

        return isSuccessSpend;
    }
}
