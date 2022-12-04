using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace ProxyCache
{
    internal class GenericProxyCache<T> where T : class
    {
        public DateTimeOffset dt_default = ObjectCache.InfiniteAbsoluteExpiration;
        Dictionary<string, T> dict = new Dictionary<string, T>();
        DateTimeOffset expirationTime = DateTimeOffset.Now.AddMinutes(1);


        public T Get(string CacheItemName, object[] args) {
            return useCache(CacheItemName, dt_default, args);

        }
        public T Get(string CacheItemName, double dt_seconds, object[] args) {
            DateTimeOffset dt = DateTime.Now.AddSeconds(dt_seconds); //In this case, the Expiration Time is "dt_default"
            return useCache(CacheItemName, dt, args);

        }
        public T Get(string CacheItemName, DateTimeOffset dt, object[] args) {
             return useCache(CacheItemName, dt, args);
        }


        private T useCache(string CacheItemName, DateTimeOffset dt, object[] args) 
        {
            ObjectCache cache = MemoryCache.Default;
            T fileContents = cache[CacheItemName] as T;
            if (fileContents == null)
            {
                Console.WriteLine("update Cache");
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = dt;
                fileContents = (T)Activator.CreateInstance(typeof(T), args);
                cache.Set(CacheItemName, fileContents, policy);
            }
            return fileContents;
        }


    }
}
