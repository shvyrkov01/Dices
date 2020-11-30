using System;
using System.IO;
using UnityEngine;

public abstract class DataLoader
{
    public static void SaveData<T>(T obj)
    {
        Type type = typeof(T);
        string json = JsonUtility.ToJson(obj);
        PlayerPrefs.SetString(type.ToString(), json);
        PlayerPrefs.Save();
    }


    public static void SaveData<T>(T obj, string additionalKey)
    {
        Type type = typeof(T);
        string json = JsonUtility.ToJson(obj);
        PlayerPrefs.SetString(additionalKey + "/" + type.ToString(), json);
        PlayerPrefs.Save();
    }


    public static T LoadData<T>()
    {
        Type type = typeof(T);

        if (PlayerPrefs.HasKey(type.ToString()) == false) return default;

        string json = PlayerPrefs.GetString(type.ToString());
        return JsonUtility.FromJson<T>(json);
    }
    

    public static T LoadData<T>(string additionalKey)
    {
        Type type = typeof(T);

        if (PlayerPrefs.HasKey(additionalKey + "/" + type.ToString()) == false) return default;

        string json = PlayerPrefs.GetString(additionalKey + "/" + type.ToString());
        return JsonUtility.FromJson<T>(json);
    }
}