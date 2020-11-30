using UnityEngine;
using UnityEngine.UI;

public class ThrowMoreOnlineModePresenter : ThrowMoreModeBasePresenter
{
    [SerializeField] private Text _bankField;



    protected override void InitComponents()
    {
        _gameMode = GetComponent<ThrowMoreOnlineMode>();
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        ((ThrowMoreOnlineMode)_gameMode).OnChangedBank += OnChangedBank;
        ((ThrowMoreOnlineMode)_gameMode).OnResetThrowsPresenter += OnResetThrowsPresenter;
    }


    protected override void OnDisable()
    {
        base.OnDisable();
        ((ThrowMoreOnlineMode)_gameMode).OnChangedBank -= OnChangedBank;
        ((ThrowMoreOnlineMode)_gameMode).OnResetThrowsPresenter -= OnResetThrowsPresenter;
    }


    protected override void OnDroppedValue(int droppedValue, EntityType entityType)
    {
        base.OnDroppedValue(droppedValue, entityType);

        Text targetScoreField = entityType == EntityType.Player ? _playerScoreField : _botScoreField;
        targetScoreField.text = droppedValue.ToString();
    }


    private void OnResetThrowsPresenter()
    {
        _playerScoreField.text = "0";
        _botScoreField.text = "0";
    }


    private void OnChangedBank(int bank)
    {
        _bankField.text = bank <= 0 ? $"{bank}" : $"+{bank}";
    }
}
