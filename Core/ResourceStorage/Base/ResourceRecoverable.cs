using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public abstract class ResourceRecoverable : Resource
{
    [HideInInspector] public OnChangedRecoveryTimeEvent OnChangedRecoveryTime = new OnChangedRecoveryTimeEvent();

    /// <summary>
    /// Максимальное ограничение количества
    /// </summary>
    public int MaxCount { get; protected set; }

    [SerializeField] protected float _recoveryTime;

    /// <summary>
    /// Следующее время восстановления
    /// </summary>
    private DateTime _nextRecoveryTime;

    private Coroutine _recoveryTimerCoroutine;



    public override void Init()
    {
        LoadResource();
        CheckFullnessAtStart();
    }


    private void OnDisable()
    {
        if (_recoveryTimerCoroutine == null) return;

        DataLoader.SaveData(new TimeData(_nextRecoveryTime), "NextRecoveryTimeEnergy");
    }


    public override bool TrySpendResources(int resourcesCount)
    {
        if (Count < resourcesCount)
            return false;

        Count -= resourcesCount;
        OnChangedResourcesCount?.Invoke(Count);

        SaveResource();

        StartResourceRecovery();

        return true;
    }


    /// <summary>
    /// Запустить восстановление ресурса
    /// </summary>
    private void StartResourceRecovery()
    {
        if (!ReferenceEquals(_recoveryTimerCoroutine, null))
            return;

        _nextRecoveryTime = DateTime.Now.AddSeconds(_recoveryTime);
        _recoveryTimerCoroutine = StartCoroutine(RecoveryTimer());
    }


    private void ChangeRecoveryTime()
    {
        TimeSpan recoveryTimeSpan = GetRecoveryTimeSpan();

        if (recoveryTimeSpan.TotalSeconds <= 0)
        {
            AddResources(1);
            _nextRecoveryTime = _nextRecoveryTime.AddSeconds(_recoveryTime);
        }

        OnChangedRecoveryTime?.Invoke(GetRecoveryTimeSpan());
    }


    private IEnumerator RecoveryTimer()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);

        while(Count < MaxCount)
        {
            ChangeRecoveryTime();
            yield return waitForSeconds;
        }

        _recoveryTimerCoroutine = null;
    }


    /// <summary>
    /// Получить разницу время до восстановления ресурса
    /// </summary>
    /// <returns></returns>
    private TimeSpan GetRecoveryTimeSpan()
    {
        return _nextRecoveryTime - DateTime.Now;
    }


    private void CheckFullnessAtStart()
    {
        if (Count >= MaxCount) return;

        var nextRecoveryTimeDataSaved = DataLoader.LoadData<TimeData>("NextRecoveryTimeEnergy");

        if (nextRecoveryTimeDataSaved == null) return;

        var nextRecoveryTimeSaved = nextRecoveryTimeDataSaved.GetDateTime();


        while((nextRecoveryTimeSaved - DateTime.Now).TotalSeconds <= 0)
        {
            if (Count >= MaxCount)
                break;

            Count++;
            nextRecoveryTimeSaved = nextRecoveryTimeSaved.AddSeconds(_recoveryTime);
        }

        if (Count < MaxCount)
        {
            _nextRecoveryTime = nextRecoveryTimeSaved;
            _recoveryTimerCoroutine = StartCoroutine(RecoveryTimer());
            OnChangedRecoveryTime?.Invoke(GetRecoveryTimeSpan());
        }

    }


    [System.Serializable]
    public class OnChangedRecoveryTimeEvent : UnityEvent<TimeSpan> { }
}
