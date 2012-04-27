using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;


namespace CouchConsoleApp.couch
{
   public class ConcurrentCookieDictionary <X, Y>
    {

        private readonly Dictionary<X, Y> items;
        private readonly Dictionary<X, DateTime> expiration;
        private readonly ILog log = LogManager.GetLogger(typeof(ConcurrentCookieDictionary<X,Y>));

        public ConcurrentCookieDictionary()
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
            Dictionary<X, string> tempDict=new Dictionary<X, string>();

            foreach (var key in expiration.Keys)
            {
                if (expiration[key] < DateTime.Now)
                {
                    tempDict.Add(key,"");
                    //expiration.Remove(key);
                    //items.Remove(key);
                }
            }

            foreach (var VARIABLE in tempDict.Keys)
            {
                expiration.Remove(VARIABLE);
                items.Remove(VARIABLE);
            }
            tempDict.Clear();
        }

        public Y this[X key]
        {
            get
            {
                if (expiration.ContainsKey(key) && expiration[key] > DateTime.Now)
                {
                   // log.Debug("Sending cached value:" + items[key]);
                    return items[key];
                }
                else
                {
                    //log.Debug("Expired sending null:" + key);
                    return default(Y);
                }
            }
        }


       public void removeCookie(X key)
       {
           if (items.ContainsKey(key))
           {
            items.Remove(key);
            expiration.Remove(key);
           }
       }

        public void clearAll()
        {
           expiration.Clear();
           items.Clear();
          // Console.WriteLine("Clear all cookie completed");
        }

        public int size()
        {
            return this.items.Count;
        }
    }
}
