using UnityEngine;
using UnityEngine.UI;
using System;
using Aledice.Extensions;

public class RegistrationPopup : Popup
{
    [SerializeField] private InputField _usernameField;
    [SerializeField] private InputField _promoCodeField;

    [SerializeField] private Color _incorrectColor;

    [SerializeField] private Button _sendButton;



    public void OnRegistrationButtonPressed()
    {
        _sendButton.interactable = false;

        // Проверяем корректность заполненных данных, callback возращает введенное имя пользователя и промокод
        CheckCorrectInputFields((username, promoCode) =>
        {
            // Инициализируем имя пользователя и промокод в пользовательских данных
            Mediator.Instance.CustomStatisticsManager.UserData.InitUsernameAndPromoCode(username, promoCode);

            // Отправляем данные пользователя на регистрацию в БД, callback возращает статус регистрации: True, False; И заголовок
            Mediator.Instance.DatabaseExchanger.TryRegisterUser((onDataSentState, headerText) =>
            {
                if (onDataSentState)
                {
                    PlayerPrefsAdvanced.SetBool("UserRegistered", true);
                    Close();
                }
                else
                    ShowAlertPopup(AlertType.Registration_Error);
            });
        });
    }


    /// <summary>
    /// Проверка корректности введенных данных
    /// </summary>
    /// <param name="initUsernameAndPromocodeHandler">Возращает введенные имя пользователя и промокод</param>
    private void CheckCorrectInputFields(Action<string, string> initUsernameAndPromocodeHandler)
    {
        string username = _usernameField.text;
        string promoCode = _promoCodeField.text.ToLower();


        if (_usernameField.text == string.Empty || _usernameField.text.Length <= 2)
        {
            _usernameField.textComponent.color = _incorrectColor;
            ShowAlertPopup(AlertType.Invalid_Fields);

            return;
        }


        if (_promoCodeField.text != string.Empty && !Mediator.Instance.GameConfig.PromoCodes.Contains(promoCode))
        {
            _promoCodeField.textComponent.color = _incorrectColor;
            ShowAlertPopup(AlertType.Invalid_Fields);

            return;
        }
        else if(Mediator.Instance.GameConfig.PromoCodes.Contains(promoCode))
        {
            int rewardChips = Mediator.Instance.GameConfig.PromoCodeReward;
            OnClose.AddListener(() => Mediator.Instance.PopupsManager.CreatePopup<RewardPopup>().Init(RewardType.Special, rewardChips));
        }

        initUsernameAndPromocodeHandler?.Invoke(username, promoCode);
    }


    /// <summary>
    /// Показать предупреждение некорректных полей
    /// </summary>
    private void ShowAlertPopup(AlertType alertType)
    {
        Mediator.Instance.PopupsManager.CreatePopup<AlertPopup>().Init(alertType);
        _sendButton.interactable = true;
    }
}
