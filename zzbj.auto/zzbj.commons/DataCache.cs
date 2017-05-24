using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzbj.commons
{
    /// <summary>
    /// DataCache
    /// </summary>
    public class DataCache
    {
        /// <summary>
        /// new
        /// </summary>
        private DataCache() { }

        /// <summary>
        /// 从缓存中获取一个 Cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        static public System.Object GetCache(string key)
        {
            return System.Web.HttpRuntime.Cache[key];
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="key">标识的Key</param>
        /// <param name="obj">需要缓存的对象</param>
        static public void SetCache(string key, System.Object obj)
        {
            System.Web.HttpRuntime.Cache.Insert(key, obj);
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="key">标识的Key</param>
        /// <param name="obj">需要缓存的对象</param>
        /// <param name="dependency">跟踪项</param>
        static public void SetCache(string key, System.Object obj, System.Web.Caching.CacheDependency dependency)
        {
            System.Web.HttpRuntime.Cache.Insert(key, obj, dependency);
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="key">标识的Key</param>
        /// <param name="obj">需要缓存的对象</param>
        /// <param name="dependency">跟踪项</param>
        /// <param name="absoluteExpiration">缓存过期的绝对时间</param>
        /// <param name="slidingExpiration">从最后一次访问指定缓存算起，超过此时间都将自动移除</param>
        static public void SetCache(string key, System.Object obj, System.Web.Caching.CacheDependency dependency, System.DateTime absoluteExpiration, System.TimeSpan slidingExpiration)
        {
            System.Web.HttpRuntime.Cache.Insert(key, obj, dependency, absoluteExpiration, slidingExpiration);
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="key">标识的Key</param>
        /// <param name="obj">需要缓存的对象</param>
        /// <param name="slidingExpiration">从最后一次访问指定缓存算起，超过此时间都将自动移除</param>
        static public void SetCache(string key, System.Object obj, System.TimeSpan slidingExpiration)
        {
            System.Web.HttpRuntime.Cache.Insert(key, obj, null, System.Web.Caching.Cache.NoAbsoluteExpiration, slidingExpiration);
        }
        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="key">标识的Key</param>
        /// <param name="obj">需要缓存的对象</param>
        /// <param name="absoluteExpiration">缓存过期的绝对时间</param>
        static public void SetCache(string key, System.Object obj, System.DateTime absoluteExpiration)
        {
            System.Web.HttpRuntime.Cache.Insert(key, obj, null, absoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
        }
        /// <summary>
        /// 从缓存中移除指定key 对象
        /// </summary>
        /// <param name="key">标识的Key</param>
        static public void RemoveCache(string key)
        {
            System.Web.HttpRuntime.Cache.Remove(key);
        }
    }
}
