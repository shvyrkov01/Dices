using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public abstract class ThrowMoreModeBasePresenter : MonoBehaviour
{
    [SerializeField] private Text _stepInfoField;

    [SerializeField] protected Text _playerScoreField;
    [SerializeField] protected Text _botScoreField;
    /// <summary>
    /// Оставшееся количество бросков у игрока
    /// </summary>

    [SerializeField] private ScoreList _scoreList;

    [SerializeField] private Color _playerColorText;
    [SerializeField] private Color _enemyColorText;

    [Space(10)]

    [SerializeField] private Animator _enemyStepTextAnimator;

    protected ThrowMoreModeBase _gameMode;



    private void Awake()
    {
        InitComponents();
    }


    protected abstract void InitComponents();


    protected virtual void OnEnable()
    {
        _gameMode.OnDroppedValue += OnDroppedValue;
        _gameMode.OnRestartGame += OnRestartGame;
        GameplayManager.OnChangedCurrentEntity += OnChangeEntity;
    }


    protected virtual void OnDisable()
    {
        _gameMode.OnDroppedValue -= OnDroppedValue;
        _gameMode.OnRestartGame -= OnRestartGame;
        GameplayManager.OnChangedCurrentEntity -= OnChangeEntity;
    }


    protected virtual void OnDroppedValue(int droppedValue, EntityType entityType)
    {
        _scoreList.ShowThrowScore(droppedValue, entityType);
    }


    private void OnChangeEntity(EntityType entityType)
    {
        Color targetColor = entityType == EntityType.Player ? _playerColorText : _enemyColorText;
        string text = entityType == EntityType.Player ? LeanLocalization.GetTranslationText("Step/You") : LeanLocalization.GetTranslationText("Step/Enemy");

        if (entityType == EntityType.Enemy)
        {
            if (_enemyStepTextAnimator.gameObject.activeSelf == false)
                _enemyStepTextAnimator.gameObject.SetActive(true);

            _enemyStepTextAnimator.Play(0);
        }

        _stepInfoField.text = text;
        _stepInfoField.color = targetColor;
    }


    private void OnRestartGame()
    {
        _playerScoreField.text = "0";
        _botScoreField.text = "0";
        _scoreList.ClearList();
    }
}
