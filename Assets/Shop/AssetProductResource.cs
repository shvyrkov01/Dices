using UnityEngine;

[CreateAssetMenu(fileName = "New asset resource", menuName = "Shop/New Resource")]
public class AssetProductResource : AssetProduct, IProductConsumable
{
    public ResourceType ResourceType;

    public int AddingProduct => _addingValue;

    [SerializeField] private int _addingValue;
}
