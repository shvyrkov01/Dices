using UnityEngine;

public interface IProduct 
{
    Sprite Icon { get; }
    string Signature { get; }
    float Price { get; }
    CurrencyType CurrencyType { get; }
}
