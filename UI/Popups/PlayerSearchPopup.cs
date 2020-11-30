using UnityEngine;
using System;
using System.Collections;

public class PlayerSearchPopup : Popup
{
    [SerializeField] private RangeValueFloat _rangeValueFloat;

    private Action _handler;



    public void Init(Action handler)
    {
        _handler = handler;

        if(ReferenceEquals(_handler, null))
        {
            Close();
            return;
        }

        StartCoroutine(PlayerSearch());
    }


    public void OnCancelButtonPressed()
    {
        StopAllCoroutines();
        Close();
    }


    private IEnumerator PlayerSearch()
    {
        float delay = UnityEngine.Random.Range(_rangeValueFloat.Min, _rangeValueFloat.Max);

        yield return new WaitForSeconds(delay);

        _handler?.Invoke();

        Close();
    }
}
