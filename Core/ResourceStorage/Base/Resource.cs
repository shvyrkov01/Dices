using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class Resource : MonoBehaviour
{
    [HideInInspector] public OnChangedResourcesCountEvent OnChangedResourcesCount = new OnChangedResourcesCountEvent();

    public int Count { get; protected set; }



    public virtual void Init()
    {
        LoadResource();
        OnChangedResourcesCount?.Invoke(Count);
    }


    public virtual void AddResources(int resourcesCount)
    {
        Count += resourcesCount;

        OnChangedResourcesCount?.Invoke(Count);

        SaveResource();
    }


    public virtual bool TrySpendResources(int resourcesCount)
    {
        if (Count < resourcesCount)
            return false;

        Count -= resourcesCount;

        OnChangedResourcesCount?.Invoke(Count);
        SaveResource();

        return true;
    }


    /// <summary>
    /// Списать штраф.
    /// Количество ресурсов может уйти в минус.
    /// </summary>
    public void CollectFine(int count)
    {
        Count -= Mathf.Abs(count);
        OnChangedResourcesCount?.Invoke(Count);
        SaveResource();
    }


    protected abstract void SaveResource();


    protected abstract void LoadResource();



    [System.Serializable]
    public class OnChangedResourcesCountEvent : UnityEvent<int> { }
}
