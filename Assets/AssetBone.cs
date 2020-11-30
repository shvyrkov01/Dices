using UnityEngine;

[CreateAssetMenu(fileName = "New Bone Data", menuName = "Data/New Bone Data")]
public class AssetBone : ScriptableObject
{
    public Sprite BoneSprite => _boneSprite;
    public int Value => _value;

    [SerializeField] private Sprite _boneSprite;
    [SerializeField] private int _value;
}
