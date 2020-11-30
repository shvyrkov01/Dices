using UnityEngine;
using System;

namespace Aledice.Extensions
{
    public static class ComponentExtensions
    {
        public static bool TryExecute<T>(this Component component, Action<T> callback)
        {
            if (component.TryGetComponent(out T gettedComponent))
            {
                callback?.Invoke(gettedComponent);
                return true;
            }

            return false;
        }
    }


    public class PlayerPrefsAdvanced : PlayerPrefs
    {
        public static void SetBool(string key, bool value)
        {
            SetInt(key, value ? 1 : 0);
        }


        public static bool GetBool(string key, bool defaultValue = false)
        {
            return GetInt(key, defaultValue ? 1 : 0) == 1 ? true : false;
        }
    }
}