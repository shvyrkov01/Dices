using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using Extensions.Mathf;

public class DatabaseExchanger : MonoBehaviour
{
    public void TryRegisterUser(Action<bool, string> onDataSentStateCallback = null)
    {
        var userData = Mediator.Instance.CustomStatisticsManager.UserData;

        WWWForm form = new WWWForm();
        form.AddField("userID", userData.UserID);
        form.AddField("username", userData.Username);
        form.AddField("promoCode", userData.PromoCode);

        StartCoroutine(SendToDatabase("http://s323635.smrtp.ru/registerUser.php", form, onDataSentStateCallback, true));
    }


    public void TrySubmitPurchase(string productName, float price, bool isEnabledWaiting = false) // TODO: Исправить отправку цены покупки
    {
        var userData = Mediator.Instance.CustomStatisticsManager.UserData;

        WWWForm form = new WWWForm();
        form.AddField("idUser", userData.UserID);
        form.AddField("promoCode", userData.PromoCode);
        form.AddField("purchaseAmount", price.ToString());
        form.AddField("date", DateFormater.GetDateNowFormatDatabase());
        form.AddField("productName", productName);

        StartCoroutine(SendToDatabase("http://s323635.smrtp.ru/userPurchaseHandler.php", form, isEnableWaiting: isEnabledWaiting));
    }


    public void TryGetRandomUsername(Action<bool, string> onDataSentStateCallback)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", Mediator.Instance.CustomStatisticsManager.UserData.Username);
        StartCoroutine(SendToDatabase("http://s323635.smrtp.ru/getRandomUsername.php", form, onDataSentStateCallback));
    }


    private IEnumerator SendToDatabase(string uri, WWWForm form, Action<bool, string> onDataSentStateCallback = null, bool isEnableWaiting = false)
    {
        WaitingPopup waitingPopup = null;

        if (isEnableWaiting)
            waitingPopup = Mediator.Instance.PopupsManager.CreatePopup<WaitingPopup>();

        using (var webRequest = UnityWebRequest.Post(uri, form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log($"Error: {webRequest.error}");
            }
            else
            {
                //print($"<color=green>{webRequest.downloadHandler.text}</color>");
                //print($"<color=green>Данные отправлены успешно!</color>");
            }

            if(!ReferenceEquals(onDataSentStateCallback, null))
                onDataSentStateCallback?.Invoke(webRequest.isNetworkError == false && webRequest.isHttpError == false, webRequest.downloadHandler.text);
        }

        if (!ReferenceEquals(waitingPopup, null))
            Destroy(waitingPopup.gameObject);
    }

 




    //private IEnumerator SendToDatabase(string uri, WWWForm form, Action<bool> onDataSentStateCallback = null, bool isEnableWaiting = false)
    //{
    //    WaitingPopup waitingPopup = null;

    //    if (isEnableWaiting)
    //        waitingPopup = Mediator.Instance.PopupsManager.CreatePopup<WaitingPopup>();

    //    using (var webRequest = UnityWebRequest.Post(uri, form))
    //    {
    //        yield return webRequest.SendWebRequest();

    //        if (webRequest.isNetworkError || webRequest.isHttpError)
    //        {
    //            Debug.Log($"Error: {webRequest.error}");
    //        }
    //        else
    //        {
    //            print($"<color=green>{webRequest.downloadHandler.text}</color>");
    //            //print($"<color=green>Данные отправлены успешно!</color>");
    //        }

    //        if (!ReferenceEquals(onDataSentStateCallback, null))
    //            onDataSentStateCallback?.Invoke(webRequest.isNetworkError == false && webRequest.isHttpError == false);
    //    }

    //    if (!ReferenceEquals(waitingPopup, null))
    //        Destroy(waitingPopup.gameObject);
    //}
}
