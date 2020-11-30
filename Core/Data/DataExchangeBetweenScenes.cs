using System.Collections.Generic;

public class DataExchangeBetweenScenes
{
    private static Dictionary<ExchangeDataType, object> Datas = new Dictionary<ExchangeDataType, object>();



    public static void SetData(params ContainerData[] datas)
    {
        foreach (ContainerData containerData in datas)
        {
            Datas.Add(containerData.Key, containerData.Data);
        }
    }


    public static object TryGetDataByKey(ExchangeDataType key)
    {
        if (!Datas.ContainsKey(key)) return null;

        object data = Datas[key];

        Datas.Remove(key);

        return data;
    }
}

[System.Serializable]
public struct ContainerData
{
    public ExchangeDataType Key;
    public object Data;



    public ContainerData(ExchangeDataType key, object data)
    {
        Key = key;
        Data = data;
    }
}