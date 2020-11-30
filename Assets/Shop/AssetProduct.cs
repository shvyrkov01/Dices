using UnityEngine;

public abstract class AssetProduct : ScriptableObject, IProduct
{
    public Sprite Icon => _icon;
    public string Signature => _signature;
    public float Price => _price;
    public CurrencyType CurrencyType => _currencyType;
    public ShopProductNames ShopProductName => _shopProductName;
    
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _signature;
    [SerializeField] private float _price;
    [SerializeField] private CurrencyType _currencyType;
    [SerializeField] private ShopProductNames _shopProductName;
}
