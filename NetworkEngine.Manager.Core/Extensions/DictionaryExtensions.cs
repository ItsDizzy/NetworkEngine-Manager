using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEngine.Manager.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : new()
        {
            if (!dictionary.TryGetValue(key, out TValue value))
            {
                value = new TValue();

                if (dictionary is ConcurrentDictionary<TKey, TValue> concurrentDictionary)
                {
                    concurrentDictionary.TryAdd(key, value);
                }
                else
                {
                    dictionary.Add(key, value);
                }
            }

            return value;
        }
    }
}
