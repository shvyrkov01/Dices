using UnityEngine;
using System;

public class CombinationAtRandomMode : GameMode
{
    public event Action<AssetBone> OnSelectedBone;
    public override event Action OnRestartGame;

    public int SelectedBoneValue => _selectedBone.Value;

    /// <summary>
    /// Хранит ссылку на выбранный ассет кости
    /// </summary>
    private AssetBone _selectedBone;



    protected override void InitGameMode()
    {
        base.InitGameMode();

        var popup = Mediator.Instance.PopupsManager.CreatePopup<PossibleCombinationsSelectorPopup>("CombinationAtRandomMode");

        popup.OnSelectedAssetBone += (assetBone) => OnSelectBone(assetBone);
        popup.OnClose.AddListener(() => GameplayManager.ChangeGameplayState(GameplayState.Started));
    }


    protected override void OnDroppedNumber(int droppedNumber)
    {
        base.OnDroppedNumber(droppedNumber);

        if (droppedNumber == _selectedBone.Value)
            ApplyPlayerWin();
        else
            ApplyPlayerLoss();
    }


    private void OnSelectBone(AssetBone assetBone)
    {
        _selectedBone = assetBone;
        OnSelectedBone?.Invoke(_selectedBone);
    }

    protected override void RestartGame()
    {
        _selectedBone = null;
        OnRestartGame?.Invoke();
    }
}
