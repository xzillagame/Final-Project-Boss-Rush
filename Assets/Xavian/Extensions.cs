using System.Collections.Generic;
using UnityEngine;

namespace Xavian
{
    public static class Extensions
    {
        public static T GetRandomFromList<T>(this List<T> list)
        {
            int randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }


        public static T RandomPopFromList<T>(this List<T> list)
        {
            int randomIndex = Random.Range(0, list.Count);

            T itemToReturn = list[randomIndex];

            list.RemoveAt(randomIndex);

            return itemToReturn;

        }




    }
}