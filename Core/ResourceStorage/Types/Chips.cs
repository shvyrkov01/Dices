using UnityEngine;

[System.Serializable]
public class Chips : Resource
{
    public override void Init()
    {
        base.Init();
    }


    protected override void LoadResource()
    {
        Count = PlayerPrefs.GetInt("Chips");
    }


    protected override void SaveResource()
    {
        PlayerPrefs.SetInt("Chips", Count);
    }
}
