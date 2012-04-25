using System;
using System.Collections.Generic;

namespace Common.Controllers.Database.Couch.CouchSecurity
{
    internal class CookieDictionary<X, Y>
    {
        private readonly Dictionary<X, Y> items;
        private readonly Dictionary<X, DateTime> expiration;

        public CookieDictionary()
        {
            items = new Dictionary<X, Y>();
            expiration = new Dictionary<X, DateTime>();
        }

        public void Add(X key, Y value, TimeSpan ttl)
        {
            if (items.ContainsKey(key))
            {
                items.Remove(key);
                expiration.Remove(key);
            }
            items.Add(key, value);
            expiration.Add(key, DateTime.Now.Add(ttl));
            RemoveExpiredKeys();
        }

        private void RemoveExpiredKeys()
        {
            foreach (var key in expiration.Keys)
            {
                if (expiration[key] < DateTime.Now)
                {
                    expiration.Remove(key);
                    items.Remove(key);
                }
            }
        }
        public Y this[X key]
        {
            get
            {
                if (expiration.ContainsKey(key) && expiration[key] > DateTime.Now)
                {
                    return items[key];
                }
                else
                {
                    return default(Y);
                }
            }
        }

        public void clearAll()
        {
           expiration.Clear();
           items.Clear();
          // Console.WriteLine("Clear all cookie completed");
        }

    }
}
