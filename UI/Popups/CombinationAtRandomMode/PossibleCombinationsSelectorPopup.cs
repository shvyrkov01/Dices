using System;
using UnityEngine;
using UnityEngine.UI;

public class PossibleCombinationsSelectorPopup : Popup
{
    public event Action<AssetBone> OnSelectedAssetBone;

    [SerializeField] private Button _continueButton;

    public AssetBone SelectedAssetBone => _currentSelectedBone.AssetBone;
    private BoneButton _currentSelectedBone;



    public void OnSelectedBone(BoneButton boneButton)
    {
        if (_continueButton.interactable == false)
            _continueButton.interactable = true;

        if (!ReferenceEquals(_currentSelectedBone, null))
            _currentSelectedBone.ChangeSelectedState(false);

        _currentSelectedBone = boneButton;
        _currentSelectedBone.ChangeSelectedState(true);

        OnSelectedAssetBone?.Invoke(SelectedAssetBone);
    }


    public override void Close()
    {
        if (ReferenceEquals(_currentSelectedBone, null))
            return;

        base.Close();
    }
}
