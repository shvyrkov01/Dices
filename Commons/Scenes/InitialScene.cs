using UnityEngine;
using System;

public class InitialScene : MonoBehaviour
{
    [SerializeField] private LoadingPopup _loadingPopup;



    private void Start()
    {
        Init();
        ApplyGameLaunch();

        _loadingPopup.LoadingScene("Home");
    }


    private void Init()
    {
        if (!PlayerPrefs.HasKey("FirstStart"))
            InitialFirstStart();
    }


    private void ApplyGameLaunch()
    {
        TimeData lastGameLaunchDate = DataLoader.LoadData<TimeData>("LastGameLaunch");

        if (lastGameLaunchDate != null)
        {
            if (lastGameLaunchDate.Day == DateTime.Now.Day)
                return;
        }

        Mediator.Instance.CustomStatisticsManager.UserData.GameLaunch();

        DataLoader.SaveData(new TimeData(DateTime.Now), "LastGameLaunch");
    }


    private void InitialFirstStart()
    {
        PlayerPrefs.SetString("FirstStart", "true");
        PlayerPrefs.SetString("SelectedDiceID", Mediator.Instance.CustomStatisticsManager.UserData.PurchasedDices[0]);

        Mediator.Instance.ResourcesStorage.Energy.AddResources(10);
        Mediator.Instance.ResourcesStorage.SurpriseBox.AddResources(1);
    }
}
