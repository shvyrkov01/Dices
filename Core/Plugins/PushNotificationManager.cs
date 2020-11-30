using UnityEngine;
using System;

public class PushNotificationManager : MonoBehaviour
{
    private const string _dice = "\U0001F3B2";
    private const string _explosion = "\U0001F4A5";
    private const string _fire = "\U0001F525";



    private void Start()
    {
        GleyNotifications.Initialize();

        if (GleyNotifications.AppWasOpenFromNotification() != null)
            AppMetricaManager.SendOpenFromPushNotification();
    }


    private void OnApplicationFocus(bool focus)
    {
        if (focus == false)
        {
            GleyNotifications.SendNotification($"Dices {_dice}", GetLocalizedText(PushTextType.Basic), new TimeSpan(6, 0, 0), "icon_0", "icon_1", "from notification");
            GleyNotifications.SendNotification($"Dices {_dice}", GetLocalizedText(PushTextType.DailyReward), new TimeSpan(24, 0, 0), "icon_0", "icon_1", "from notification");
            GleyNotifications.SendNotification($"Dices {_dice}", GetLocalizedText(PushTextType.Basic), new TimeSpan(48, 0, 0), "icon_0", "icon_1", "from notification");
            GleyNotifications.SendNotification($"Dices {_dice}", GetLocalizedText(PushTextType.Basic), new TimeSpan(72, 0, 0), "icon_0", "icon_1", "from notification");
        }
    }


    private string GetLocalizedText(PushTextType pushTextType)
    {
        if(pushTextType == PushTextType.Basic)
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                return $"Вас давно не было в игре, время выигрывать {_fire}";
            else
                return $"You haven't been in the game for a long time, it's time to win {_fire}";
        }
        else
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                return $"Получите свой ежедневный бонус {_explosion}";
            else
                return $"Claim your daily bonus {_explosion}";
        }
    }


    public enum PushTextType
    {
        DailyReward,
        Basic
    }
}