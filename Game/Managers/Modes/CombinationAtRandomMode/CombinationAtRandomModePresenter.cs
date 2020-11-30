using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CombinationAtRandomMode))]
public class CombinationAtRandomModePresenter : MonoBehaviour
{
    [SerializeField] private Image _selectedBoneImage;



    private void OnEnable()
    {
        GetComponent<CombinationAtRandomMode>().OnSelectedBone += ShowSelectedBone;
    }


    private void OnDisable()
    {
        GetComponent<CombinationAtRandomMode>().OnSelectedBone -= ShowSelectedBone;
    }


    private void ShowSelectedBone(AssetBone assetBone)
    {
        _selectedBoneImage.sprite = assetBone.BoneSprite;
        _selectedBoneImage.gameObject.SetActive(true);
    }
}
