using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebjetMovieAppBL.Caching
{
    public class GlobalCachingProvider : CachingProviderBase, IGlobalCachingProvider
    {
        /// <summary>
        /// Only one cache object is required. Singleton is used to use oop features
        /// </summary>
        #region Singleton

        internal static readonly GlobalCachingProvider instance = new GlobalCachingProvider();
        protected GlobalCachingProvider()
        {
        }

        public static GlobalCachingProvider Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        #region ICachingProvider

        public virtual new void AddItem(string key, object value)
        {
            base.AddItem(key, value);
        }

        public virtual new void UpdateItem(string key, object value)
        {
            base.UpdateItem(key, value);
        }

        public virtual object GetItem(string key, bool remove = false)
        {
            return base.GetItem(key, remove);//Remove default is false
        }

        #endregion

    }
}
