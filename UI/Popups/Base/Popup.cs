using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using Lean.Localization;

public class Popup : MonoBehaviour
{
    public UnityEvent OnOpen;
    public UnityEvent OnClose;


    protected virtual void OnEnable()
    {
    }


    protected virtual void OnDisable()
    {
        
    }


    private void OnDestroy()
    {
        OnClose?.Invoke();
        OnClose.RemoveAllListeners();
    }


    public virtual void Close()
    {
        Mediator.Instance.SoundManager.PlaySound(SoundType.Click);
        Destroy(gameObject);
    }


    public void Open()
    {
        OnOpen?.Invoke();
    }


    protected string GetLocalizationTextByKey(string key)
    {
        return LeanLocalization.GetTranslationText(key);
    }


    protected IEnumerator ActionWithDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}
