using UnityEngine;
using System;

public class ConfirmationPopup : Popup
{
    private Action _actionHandler;



    public void Init(Action actionHandler)
    {
        _actionHandler = actionHandler;
    }


    public void OnExitButtonPressed()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);

        if (!ReferenceEquals(_actionHandler, null))
            _actionHandler?.Invoke();
    }
}
