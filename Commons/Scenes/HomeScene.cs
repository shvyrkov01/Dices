using UnityEngine;
using System;
using System.Collections;
using Aledice.Extensions;

public class HomeScene : MonoBehaviour
{
    public static bool _isCheckGameLaunch = false;

    [SerializeField] private Animator _boneAnimator;



    private void Start()
    {
        StartCoroutine(OpenTargetPopups());
    }


    public void OnGameButtonPressed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);
        Mediator.Instance.PopupsManager.CreatePopup<LoadingPopup>().LoadingScene("Modes");
    }


    public void OnShopButtonPressed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);
        Mediator.Instance.PopupsManager.CreatePopup<LoadingPopup>().LoadingScene("Shop");
    }


    public void OnSettingsButtonPressed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);
        Mediator.Instance.PopupsManager.CreatePopup<SettingsPopup>();
    }


    private void CheckRewards()
    {
        if(Mediator.Instance.ResourcesStorage.SurpriseBox.TrySpendResources(1))
        {
            Mediator.Instance.PopupsManager.CreatePopup<SurprisePopup>().OnClose.AddListener(() => CheckRewards());
        }
    }


    private void CheckGameLaunchCount()
    {
        if (_isCheckGameLaunch) return;

        _isCheckGameLaunch = true;

        
        if (Mediator.Instance.CustomStatisticsManager.UserData.GamesRunning % Mediator.Instance.GameConfig.GameLaunchCountForOpenSubscriptionPopup == 0)
        {
            if (Mediator.Instance.CustomStatisticsManager.UserData.GamesRunning != 0 && Mediator.Instance.CustomStatisticsManager.IsSubscriptionPurchased == false)
                Mediator.Instance.PopupsManager.CreatePopup<SubscriptionPopup>();
        }
        else if (Mediator.Instance.CustomStatisticsManager.UserData.GamesRunning % Mediator.Instance.GameConfig.GameLaunchCountForReceiptSurprise == 0)
        {
            Mediator.Instance.ResourcesStorage.SurpriseBox.AddResources(1);
        }
    }


    private void CheckDailyReward()
    {
        TimeData timeData = DataLoader.LoadData<TimeData>("DailyRewardDate");

        if(ReferenceEquals(timeData, null))
            return;

        if (timeData.Day != DateTime.Now.Day)
            GiveDailyReward();
    }


    private void GiveDailyReward()
    {
        SaveIssueDate();
        Mediator.Instance.PopupsManager.CreatePopup<RewardPopup>().Init();
    }


    private void SaveIssueDate()
    {
        TimeData timeData = new TimeData(System.DateTime.Now);
        DataLoader.SaveData(timeData, "DailyRewardDate");
    }


    private IEnumerator OpenTargetPopups()
    {
        if (!PlayerPrefsAdvanced.GetBool("TermsOfUse", false))
        {
            Mediator.Instance.PopupsManager.CreatePopup<TermsOfUsePopup>();
            yield return new WaitUntil(() => PlayerPrefsAdvanced.GetBool("TermsOfUse"));
        }
        if (!PlayerPrefsAdvanced.GetBool("UserRegistered", false))
        {
            Mediator.Instance.PopupsManager.CreatePopup<RegistrationPopup>();
            yield return new WaitUntil(() => PlayerPrefsAdvanced.GetBool("UserRegistered"));
        }

        CheckRewards();
        CheckGameLaunchCount();
        CheckDailyReward();
    }
}
