using UnityEngine;

/// <summary>
/// Хранит минимальное и максимальное значение
/// </summary>
[System.Serializable]
public struct RangeValueInt : IRangeValue
{
    public int Min;
    public int Max;


    public RangeValueInt(int min = 0, int max = 0)
    {
        Min = 0;
        Max = 0;
    }


    public int GetRandomValue() => Random.Range(Min, Max + 1);
}
