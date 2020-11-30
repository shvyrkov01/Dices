using UnityEngine;
using System;

namespace Extensions
{
    public static class CollisionsHandler
    {
        public static void CheckComponent<T>(this Collider collider, Action<T> handler) where T : Component
        {
            T component;
            
            component = collider.GetComponentInParent<T>();

            if (ReferenceEquals(component, null))
                component = collider.GetComponentInChildren<T>();

            if (ReferenceEquals(component, null)) return;

            handler?.Invoke(component);
        }


        public static void CheckComponent<T>(this Collision component, Action<T> handler) where T : Component
        {
            if(component.transform.TryGetComponent<T>(out T componentScript))
                handler?.Invoke(componentScript);
        }
    }
}