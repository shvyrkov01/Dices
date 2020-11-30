using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreList : MonoBehaviour
{
    /// <summary>
    /// Скорость пемещения строки вверх
    /// </summary>
    [SerializeField] private float _topMovingSpeed;
    /// <summary>
    /// Скорость перемещения во внутрь контейнера
    /// </summary>
    [SerializeField] private float _rightMovingSpeed;

    /// <summary>
    /// Расстояние между строками
    /// </summary>
    [SerializeField] private float _spacing;

    /// <summary>
    /// Максимальное количество строк
    /// </summary>
    [SerializeField] private int _maxCountRows = 5;

    [Space(15)]

    /// <summary>
    /// Стартовая позиции новой строки (за контейнером)
    /// </summary>
    [SerializeField] private Transform _startPosition;
    /// <summary>
    /// Контейнер всех строк
    /// </summary>
    [SerializeField] private Transform _container;

    [Space(15)]

    /// <summary>
    /// Отображение выброшенных очков для игрока
    /// </summary>
    [SerializeField] private ScoreRowPresenter _playerScoreRowPresenterTemplate;
    /// <summary>
    /// Отображение выброшенных очков для противника
    /// </summary>
    [SerializeField] private ScoreRowPresenter _enemyScoreRowPresenterTemplate;

    [Space(15)]
    
    
    [SerializeField] private List<ScoreRowData> _scoreRowDatas = new List<ScoreRowData>();

    

    /// <summary>
    /// Отобразить выброшенное количество очков
    /// </summary>
    /// <param name="scoreValue">Количество выброшенных очков</param>
    /// <param name="entityType">Сущность, которая бросала кости</param>
    public void ShowThrowScore(int scoreValue, EntityType entityType = EntityType.Player)
    {
        bool isShowedUsername = GameplayManager.SelectedGameModeType == GameModeType.Throw_Online;

        ScoreRowPresenter scoreRowPresenter = GetScoreRow(entityType);
        scoreRowPresenter.Init(scoreValue, isShowedUsername);

        Vector3 rowTargetPosition = new Vector3(0, -70, 0);

        for (int i = _scoreRowDatas.Count-1; i >= 0; i--)
        {
            Transform row = _scoreRowDatas[i].ScoreRowPresenter.transform;

            float movingSpeed = _scoreRowDatas.Count - 1 == i ? _rightMovingSpeed : _topMovingSpeed;

            StartCoroutine(MoveTo(row, rowTargetPosition, movingSpeed));
            rowTargetPosition.y += _spacing;
        }
    }


    public void ClearList()
    {
        foreach (Transform scoreRow in _container)
            Destroy(scoreRow.gameObject);

        _scoreRowDatas.Clear();
    }


    /// <summary>
    /// Возвращает нужную строку
    /// Если нужная строка есть за границами контейнера, переиспользует ее
    /// </summary>
    /// <param name="entityType"></param>
    /// <returns></returns>
    private ScoreRowPresenter GetScoreRow(EntityType entityType)
    {
        ScoreRowPresenter scoreRowPresenterReference = entityType == EntityType.Player ? _playerScoreRowPresenterTemplate : _enemyScoreRowPresenterTemplate;
        ScoreRowPresenter scoreRowPresenter = null;

        if (_scoreRowDatas.Count >= _maxCountRows)
        {
            if(_scoreRowDatas[0].EntityType == entityType)
            {
                scoreRowPresenter = _scoreRowDatas[0].ScoreRowPresenter;
                scoreRowPresenter.transform.localPosition = _startPosition.localPosition;

                _scoreRowDatas.RemoveAt(0);
            }
        }

        if (ReferenceEquals(scoreRowPresenter, null))
            scoreRowPresenter = Instantiate(scoreRowPresenterReference, _startPosition.position, Quaternion.identity, _container);

        _scoreRowDatas.Add(new ScoreRowData(entityType, scoreRowPresenter));

        return scoreRowPresenter;
    }


    /// <summary>
    /// Передвинуть строку на нужное место
    /// </summary>
    /// <param name="row"></param>
    /// <param name="targetPosition"></param>
    /// <param name="movingSpeed"></param>
    /// <returns></returns>
    private IEnumerator MoveTo(Transform row, Vector3 targetPosition, float movingSpeed)
    {
        while(row.localPosition != targetPosition)
        {
            row.localPosition = Vector3.MoveTowards(row.localPosition, targetPosition, movingSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}

/// <summary>
/// Описывает объект для удобства хранения и обращения при поиске нужной строки по типу сущности
/// </summary>
[System.Serializable]
public class ScoreRowData
{
    public EntityType EntityType;
    public ScoreRowPresenter ScoreRowPresenter;


    public ScoreRowData(EntityType entityType, ScoreRowPresenter scoreRowPresenter)
    {
        EntityType = entityType;
        ScoreRowPresenter = scoreRowPresenter;
    }
}