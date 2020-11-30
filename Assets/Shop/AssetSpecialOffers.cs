using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New asset special offer", menuName = "Shop/New special offer")]
public class AssetSpecialOffers : ScriptableObject
{
    public List<OfferData> OfferDatas = new List<OfferData>();
}

[System.Serializable]
public class OfferData
{
    public ShopProductNames ShopProductName;
    public SpecialOfferType SpecialOfferType;

    public Sprite Icon;

    public float OldPrice;
    public float NewPrice;

    public int AddingValue;
}