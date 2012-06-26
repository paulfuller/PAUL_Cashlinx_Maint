using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common.Libraries.Utility.Collection
{
    public static class CollectionUtilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool isEmpty<T>(T[] array)
        {
            if (array == null || array.Length <= 0) return (true);
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool isEmpty<T>(ICollection<T> collection)
        {
            if (collection == null || collection.Count <= 0) return (true);
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public static bool isEmpty<U, V>(IDictionary<U, V> map)
        {
            if (map == null || map.Count <= 0) return (true);
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool isEmpty<T>(IEnumerable<T> collection)
        {
            if (collection == null || collection.Count() <= 0)
            {
                return (true);
            }
            return (false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool isNotEmpty<T>(T[] array)
        {
            if (array == null || array.Length <= 0) return (false);
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool isNotEmpty<T>(ICollection<T> collection)
        {
            if (collection == null || collection.Count <= 0) return (false);
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="map"></param>
        /// <returns></returns>
        public static bool isNotEmpty<U, V>(IDictionary<U, V> map)
        {
            //GJL - 06/15/2010 - Returning incorrect value of true when map is empty, 
            //now returning correct value of false when map is empty
            if (map == null || map.Count <= 0) return (false);
            return (true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="map"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool isNotEmptyContainsKey<U, V>(IDictionary<U, V> map, U key)
        {
            if (map == null || map.Count <= 0 || map.ContainsKey(key) == false)
                return (false);
            return (true);
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="map"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool AddIfNoEntryExists<U, V>(IDictionary<U, V> map, U key, V value)
        {
            if (map == null || map.ContainsKey(key))
            {
                return (false);
            }
            map.Add(key, value);
            return (true);
        }

        /// <summary>
        /// Convenience method for safely defaulting an invalid map entry or always
        /// returning the map entry if both the key and map are valid
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="map"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static W GetIfKeyValid<U, V, W>(IDictionary<U, V> map, U key, W defaultValue)
        {
            if (map == null || !map.ContainsKey(key))
            {
                return ((W)defaultValue);
            }
            V mapVal = map[key];
            
            if (Utilities.IsNullOrDefault<V>(mapVal))
            {
                return ((W)defaultValue);
            }
            return ((W)((object)mapVal));
        }

        /// <summary>
        /// Creates an array of specified size containing a default value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static T[] CreateDefaultValueArray<T>(T defaultValue, int size)
        {
            if (size == 0 || Utilities.IsNullOrDefault<T>(defaultValue))
            {
                return (new T[]{});
            }
            var returnArr = new T[size];
            for (int i = 0; i < returnArr.Length; ++i)
            {
                returnArr[i] = defaultValue;
            }
            return (returnArr);
        }

        /// <summary>
        /// Convenience method for safely defaulting an invalid map entry or always
        /// returning the map entry if both the key and map are valid. Special version
        /// for arrays if you need to specify a default cardinality
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <typeparam name="W"></typeparam>
        /// <param name="map"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="cardinality"></param>
        /// <returns></returns>
        /// GJL - 06/15/2010 - Made ReSharper recommended change to remove redundant type specifications
        public static W[] GetIfKeyValid<U, V, W>(IDictionary<U, V> map, U key, W defaultValue, int cardinality)
        {
            if (map == null || !map.ContainsKey(key))
            {
                return (CreateDefaultValueArray(defaultValue, cardinality));
            }
            V mapVal = map[key];

            if (Utilities.IsNullOrDefault(mapVal))
            {
                return (CreateDefaultValueArray(defaultValue, cardinality));
            }

            return ((W[])(object)mapVal);
        }

        public static IEnumerable<T> Collect<T>(this IEnumerable<T> collection) where T : class
        {
            return collection.Where(t => t != null);
        }

        public static IEnumerable<U> Map<T,U>(this IEnumerable<T> collection, Func<T,U> map)
        {
            return collection.Select(map);
        }
    }
}
