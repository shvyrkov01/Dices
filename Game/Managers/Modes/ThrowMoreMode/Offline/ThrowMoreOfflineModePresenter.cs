using UnityEngine;
using UnityEngine.UI;

public class ThrowMoreOfflineModePresenter : ThrowMoreModeBasePresenter
{
    [SerializeField] private Text _numberThrowsField;



    protected override void InitComponents()
    {
        _gameMode = GetComponent<ThrowMoreOfflineMode>();
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        ((ThrowMoreOfflineMode)_gameMode).OnChangedNumberThrows += OnChangedNumberThrows;
    }


    protected override void OnDisable()
    {
        base.OnDisable();
        ((ThrowMoreOfflineMode)_gameMode).OnChangedNumberThrows -= OnChangedNumberThrows;
    }


    protected override void OnDroppedValue(int droppedValue, EntityType entityType)
    {
        base.OnDroppedValue(droppedValue, entityType);

        Text targetScoreField = entityType == EntityType.Player ? _playerScoreField : _botScoreField;
        targetScoreField.text = (int.Parse(targetScoreField.text) + droppedValue).ToString();
    }


    private void OnChangedNumberThrows(int number)
    {
        _numberThrowsField.text = number.ToString();
    }
}
