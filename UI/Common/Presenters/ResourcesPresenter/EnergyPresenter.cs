using System;
using UnityEngine;
using UnityEngine.UI;

public class EnergyPresenter : ResourcePresenterBase
{
    [SerializeField] private Text _recoveryTimeField;



    private void Start()
    {
        ShowAmount(Mediator.Instance.ResourcesStorage.Energy.Count);
    }


    private void OnEnable()
    {
        if (ReferenceEquals(Mediator.Instance.ResourcesStorage.Energy, null)) return;

        Mediator.Instance.ResourcesStorage.Energy.OnChangedResourcesCount.AddListener(ShowAmount);
        Mediator.Instance.ResourcesStorage.Energy.OnChangedRecoveryTime.AddListener(ShowRecoveryTime);
    }


    protected void OnDisable()
    {
        if (ReferenceEquals(Mediator.Instance.ResourcesStorage.Energy, null)) return;

        Mediator.Instance.ResourcesStorage.Energy.OnChangedResourcesCount.RemoveListener(ShowAmount);
        Mediator.Instance.ResourcesStorage.Energy.OnChangedRecoveryTime.RemoveListener(ShowRecoveryTime);
    }


    protected override void ShowAmount(int count)
    {
        _walletField.text = $"{count}/{Mediator.Instance.ResourcesStorage.Energy.MaxCount}";
        CheckFullnesEnergy();
    }


    private void ShowRecoveryTime(TimeSpan recoveryTimeSpan)
    {
        _recoveryTimeField.text = string.Format("{0:D2}:{1:D2}", recoveryTimeSpan.Minutes, recoveryTimeSpan.Seconds+1);

        CheckFullnesEnergy();
    }


    private void CheckFullnesEnergy()
    {
        bool isFullnesEnergy = Mediator.Instance.ResourcesStorage.Energy.Count >= Mediator.Instance.ResourcesStorage.Energy.MaxCount;
        _recoveryTimeField.gameObject.SetActive(!isFullnesEnergy);
    }
}
