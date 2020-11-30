using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контроллер потока объектов
/// </summary>
/// <typeparam name="T">Тип объекта для пула</typeparam>
public class PoolController<T> where T : Component
{
    protected Pool<T> _pool = new Pool<T>();


    public void InqueueToPool(T obj)
    {
        _pool.Accept(obj);
    }


    public T DequeueFromPool()
    {
        if (_pool.PoolSize == 0)
            return null;

        T poolObject = _pool.Get();

        return poolObject;
    }


    public class Pool<T1>
    {
        public int PoolSize => ObjectsPool.Count;

        private Queue<T1> ObjectsPool = new Queue<T1>();


        /// <summary>
        /// Занести объект в пулл
        /// </summary>
        /// <param name="obj">Объект пулла</param>
        public void Accept(T1 obj)
        {
            ObjectsPool.Enqueue(obj);
        }

        /// <summary>
        /// Занести объект в пулл
        /// </summary>
        /// <param name="obj">Transform объекта, который имеет тип класса пуллинга</param>
        public void AcceptTransform(Transform obj)
        {
            if (obj.TryGetComponent(out T1 component))
            {
                Accept(component);
            }
        }

        /// <summary>
        /// Получить объект из пулла
        /// </summary>
        /// <returns>Объект типа класса</returns>
        public T1 Get()
        {
            T1 obj = ObjectsPool.Dequeue();
            return obj;
        }
    }
}
