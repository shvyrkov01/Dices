using UnityEngine;

[System.Serializable]
public class Energy : ResourceRecoverable
{
    public override void Init()
    {
        MaxCount = Mediator.Instance.GameConfig.MaxAmountEnergy;
        _recoveryTime = Mediator.Instance.GameConfig.EnergyRecoveryTime;

        base.Init();
    }


    protected override void LoadResource()
    {
        Count = PlayerPrefs.GetInt("Energy");
        OnChangedResourcesCount?.Invoke(Count);
    }


    protected override void SaveResource()
    {
        PlayerPrefs.SetInt("Energy", Count);
    }
}
