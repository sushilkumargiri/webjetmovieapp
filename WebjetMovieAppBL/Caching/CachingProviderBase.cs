using System;
using System.Runtime.Caching;

namespace WebjetMovieAppBL.Caching
{
    public class CachingProviderBase
    {

        protected MemoryCache cache = new MemoryCache("CachingProvider");

        static readonly object padlock = new object();
        //absolute expiration to 10 minutes
        CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.UtcNow.AddMinutes(Constants.CacheMinuteDuration) };
        /// <summary>
        /// Add item in cache with policy defined above
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected virtual void AddItem(string key, object value)
        {
            lock (padlock)
            {
                if (cache[key] != null)
                {
                    UpdateItem(key, value);
                }
                else
                {
                    cache.Add(key, value, policy);
                }
            }
        }
        /// <summary>
        /// Update cache item
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected virtual void UpdateItem(string key, object value)
        {
            lock (padlock)
            {
                RemoveItem(key);
                cache.Add(key, value, policy);
            }
        }
        /// <summary>
        /// remove item from cache
        /// </summary>
        /// <param name="key"></param>
        protected virtual void RemoveItem(string key)
        {
            lock (padlock)
            {
                cache.Remove(key);
            }
        }
        /// <summary>
        /// get item from cache for supplied key and remove if necessary
        /// </summary>
        /// <param name="key"></param>
        /// <param name="remove"></param>
        /// <returns></returns>
        protected virtual object GetItem(string key, bool remove)
        {
            lock (padlock)
            {
                var res = cache[key];

                if (res != null)
                {
                    if (remove == true)
                        cache.Remove(key);
                }

                return res;
            }
        }
    }
}
