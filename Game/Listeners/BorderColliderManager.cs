using UnityEngine;
using System.Collections.Generic;

public class BorderColliderManager : MonoBehaviour
{
    private List<Collider> _borderColliders = new List<Collider>();

    private BoneGenerator _boneGenerator;



    private void Awake()
    {
        _boneGenerator = FindObjectOfType<BoneGenerator>();

        foreach (Transform border in transform)
        {
            if (border.TryGetComponent(out BoxCollider boxCollider))
                _borderColliders.Add(boxCollider);
        }
    }


    private void OnEnable()
    {
        _boneGenerator.OnUpdateBonesState += UpdateStateColliders;
    }


    private void OnDisable()
    {
        _boneGenerator.OnUpdateBonesState -= UpdateStateColliders;
    }


    private void UpdateStateColliders(BonesState bonesState)
    {
        _borderColliders.ForEach(border =>
        {
            border.isTrigger = bonesState == BonesState.Sleep;
        });
    }
}
