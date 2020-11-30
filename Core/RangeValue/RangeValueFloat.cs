using UnityEngine;

/// <summary>
/// Хранит минимальное и максимальное значение
/// </summary>
[System.Serializable]
public struct RangeValueFloat : IRangeValue
{
    public float Min;
    public float Max;


    public RangeValueFloat(float min = 0, float max = 0)
    {
        Min = 0;
        Max = 0;
    }


    public float GetRandomValue() => Random.Range(Min, Max + 1);
}
