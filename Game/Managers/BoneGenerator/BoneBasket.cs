using UnityEngine;
using Extensions;

public class BoneBasket : MonoBehaviour
{
    [SerializeField] private BoneGenerator _boneGenerator;



    private void OnTriggerEnter(Collider collider)
    {
        collider.CheckComponent<Bone>((bone) => _boneGenerator.SendBoneToPool(bone));
    }
}
