using UnityEngine;
using System.Collections;

public class InternetChecker : MonoBehaviour
{
    private Coroutine _connectionTimerCoroutine;



    private void Start()
    {
        StartCoroutine(CheckConnection());    
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }


    private IEnumerator CheckConnection()
    {
        while(true)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                ConnectionStateHandler(false);
                yield break;
            }

            _connectionTimerCoroutine = StartCoroutine(ConnectionTimer());

            WWW www = new WWW("https://www.google.com");
            yield return www;

            if (!ReferenceEquals(_connectionTimerCoroutine, null))
                StopCoroutine(_connectionTimerCoroutine);

            ConnectionStateHandler(www.error == null);

            yield return new WaitForSeconds(2f);
        }
    }


    private IEnumerator ConnectionTimer()
    {
        yield return new WaitForSeconds(5f);
        ConnectionStateHandler(false);
    }


    private void ConnectionStateHandler(bool connectionState)
    {
        if (connectionState) return;

        DataExchangeBetweenScenes.SetData(new ContainerData(ExchangeDataType.Show_Alert_Popup, AlertType.No_Internet));
        Mediator.Instance.PopupsManager.CreatePopup<LoadingPopup>().LoadingScene("Modes");
    }
}
