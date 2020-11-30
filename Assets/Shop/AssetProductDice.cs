using UnityEngine;

[CreateAssetMenu(fileName = "New asset dice", menuName = "Shop/New Dice")]
public class AssetProductDice : AssetProduct
{
    public string DiceID => _diceID;

    public string Specifications => _specifications;

    [SerializeField] private string _diceID;
    [SerializeField] private string _specifications;
}
