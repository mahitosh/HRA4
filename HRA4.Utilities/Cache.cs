using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
namespace HRA4.Utilities
{
    public class Cache
    {
        

            #region PUBLIC METHOD(S)
            public static T GetCache<T>(string key) where T : class
            {
                T retValue = null;
                 
                if (HttpRuntime.Cache[key] != null)
                    retValue = HttpRuntime.Cache[key] as T;

                return retValue;
            }

            public static void SetCacheWithSlidingExpiration<T>(string key, T value,int seconds)where T : class
            {
                HttpRuntime.Cache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                                                TimeSpan.FromSeconds(seconds), CacheItemPriority.Default, null);
            }

            public static void SetCache<T>(string key, T value) where T : class
            {
                HttpRuntime.Cache[key] = value;
            }

            public static void RemoveCache<T>(string key) where T : class
            {
                T retValue = GetCache<T>(key);

                if (retValue != null)
                    HttpRuntime.Cache.Remove(key);
            }
            #endregion  //PUBLIC METHOD(S)

            #region PRIVATE METHOD(S)
            #endregion  //PRIVATE METHOD(S)

           
    }
    
}
