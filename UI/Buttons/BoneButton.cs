using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Button))]
public class BoneButton : MonoBehaviour
{
    public AssetBone AssetBone => _currentAssetBone;
    [SerializeField] private AssetBone _currentAssetBone;

    [SerializeField] private GameObject _selectedEffectImage;



    private void Start()
    {
        Init();
    }


    public void Init()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GetComponentInParent<PossibleCombinationsSelectorPopup>().OnSelectedBone(this);
        });
    }


    public void ChangeSelectedState(bool isSelected)
    {
        _selectedEffectImage.SetActive(isSelected);
    }
}
