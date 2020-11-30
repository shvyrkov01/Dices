using System;
using System.Collections;
using UnityEngine;

public class CheckInternetPopup : Popup
{
    private Action<bool> _isInternetConnectionHandler;



    private void OnDestroy()
    {
        StopAllCoroutines();
    }


    public void CheckConnectionState(Action<bool> isInternetConnectionHandler)
    {
        _isInternetConnectionHandler = isInternetConnectionHandler;

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            NotifyConnectionState(false);
            return;
        }

        StartCoroutine(CheckInternetConnection(state =>
        {
            NotifyConnectionState(state);
        }));

        StartCoroutine(ConnectionTimer());
    }


    private IEnumerator CheckInternetConnection(Action<bool> isInternerConnectionHandler)
    {
        WWW www = new WWW("https://www.google.com");
        yield return www;

        isInternerConnectionHandler?.Invoke(www.error == null);
    }


    private IEnumerator ConnectionTimer()
    {
        yield return new WaitForSeconds(5f);

        NotifyConnectionState(false);
    }


    private void NotifyConnectionState(bool connectionState)
    {
        _isInternetConnectionHandler?.Invoke(connectionState);
        Close();
    }
}
