using System.Collections.Generic;

public static class AppMetricaManager
{
    public static void SendUserData(UserData userData)
    {
        Dictionary<string, object> dataToSend = new Dictionary<string, object>();
        Dictionary<string, object> userDatas = new Dictionary<string, object>();

        userDatas.Add("Received Chips", userData.ReceivedChips);
        userDatas.Add("Received Surprise Boxs", userData.ReceivedSurpriseBoxs);
        
        userDatas.Add("Games Running", userData.GamesRunning);
        
        userDatas.Add("Games Win", userData.GamesWin);
        userDatas.Add("Game Loss", userData.GamesLoss);
        userDatas.Add("Game Draw", userData.GamesDraw);

        userDatas.Add("Modes Running", userData.ModesRunning);
        userDatas.Add("Combination At Random Mode Launch", userData.CombinationAtRandomModeLaunch);
        userDatas.Add("Throw Mode Offile Mode Launch", userData.ThrowModeOffileModeLaunch);
        userDatas.Add("Throw Mode Online Mode Launch", userData.ThrowModeOnlineModeLaunch);
        userDatas.Add("TwentyOneModeLaunch", userData.TwentyOneModeLaunch);

        dataToSend.Add(userData.UserID, userDatas);

        AppMetrica.Instance.ReportEvent("User Datas", dataToSend);
    }


    /// <summary>
    /// Отправить общие данные для всех пользователей
    /// </summary>
    public static void SendGameAndModeLaunchCountData()
    {
        UserData userData = Mediator.Instance.CustomStatisticsManager.UserData;
        Dictionary<string, object> userDatas = new Dictionary<string, object>();

        userDatas.Add("Games Running", userData.GamesRunning);
        userDatas.Add("Modes Running", userData.ModesRunning);

        userDatas.Add("Combination At Random Mode Launch", userData.CombinationAtRandomModeLaunch);
        userDatas.Add("Throw Mode Offile Mode Launch", userData.ThrowModeOffileModeLaunch);
        userDatas.Add("Throw Mode Online Mode Launch", userData.ThrowModeOnlineModeLaunch);
        userDatas.Add("TwentyOneModeLaunch", userData.TwentyOneModeLaunch);

        AppMetrica.Instance.ReportEvent("Game&Modes Launch", userDatas);
    }


    /// <summary>
    /// Отправить наименование открывшейся сцены
    /// </summary>
    /// <param name="sceneName"></param>
    public static void SendOpenScene(string sceneName)
    {
        Dictionary<string, object> sceneDatas = new Dictionary<string, object>();
        sceneDatas.Add("Scene", sceneName);
        AppMetrica.Instance.ReportEvent("Open Scenes", sceneDatas);
    }


    /// <summary>
    /// Отправить нажатие на продукт
    /// </summary>
    /// <param name="productId">Индентификатор продукта</param>
    public static void SendProductClick(string productId)
    {
        Dictionary<string, object> productDatas = new Dictionary<string, object>();
        productDatas.Add("Product purchase Button", productId);
        AppMetrica.Instance.ReportEvent("Shop", productDatas);
    }


    /// <summary>
    /// Отправить событие открытого popup подписки
    /// </summary>
    public static void SendSubscriptionPopupOpen()
    {
        Dictionary<string, object> subscriptionDatas = new Dictionary<string, object>();
        subscriptionDatas.Add("Subscription Popup Open", "Open");
        AppMetrica.Instance.ReportEvent("Subscription Popup", subscriptionDatas);
    }


    /// <summary>
    /// Отправить событие нажатия на кнопку оформления подписки
    /// </summary>
    public static void SendSubscriptionButtonClick()
    {
        Dictionary<string, object> subscriptionDatas = new Dictionary<string, object>();
        subscriptionDatas.Add("Subscription Purchase Button", "Click");
        AppMetrica.Instance.ReportEvent("Subscription Popup", subscriptionDatas);
    }


    public static void SendOpenFromPushNotification()
    {
        Dictionary<string, object> pushDatas = new Dictionary<string, object>();
        pushDatas.Add("Open app from push", "True");
        AppMetrica.Instance.ReportEvent("Push Notification Statistics", pushDatas);
    }


    public static void SendSetRate(int settedRate)
    {
        Dictionary<string, object> rateDatas = new Dictionary<string, object>();
        rateDatas.Add("Setted Rate", settedRate);
        AppMetrica.Instance.ReportEvent("Setted Rate Datas", rateDatas);
    }
}
