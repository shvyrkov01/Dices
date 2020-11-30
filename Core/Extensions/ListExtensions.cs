using System.Collections.Generic;

namespace Extensions.Collection.Generic
{
    public static class ListExtensions
    {
        public static void Swap<T>(this List<T> list, T obj1, T obj2)
        {
            int obj1Index = list.IndexOf(obj1);
            int obj2Index = list.IndexOf(obj2);

            list[obj1Index] = obj2;
            list[obj2Index] = obj1;
        }
    }
}