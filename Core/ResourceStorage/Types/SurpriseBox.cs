using UnityEngine;

[System.Serializable]
public class SurpriseBox : Resource
{
    public override void Init()
    {
        base.Init();
    }


    protected override void LoadResource()
    {
        Count = PlayerPrefs.GetInt("SurpriseBox");
    }


    protected override void SaveResource()
    {
        PlayerPrefs.SetInt("SurpriseBox", Count);
    }
}
