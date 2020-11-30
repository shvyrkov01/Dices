using UnityEngine;

public class BoneModel : MonoBehaviour
{
    public Material BodyMaterial => _bodyMaterial;
    public Material PointsMaterial => _pointsMaterial;


    [SerializeField] private Material _bodyMaterial;
    [SerializeField] private Material _pointsMaterial;
}
