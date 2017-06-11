using System;
namespace JsonAPI.Net.Cache
{
    public class MemoryCache : ICache<ICacheAble>
    {
        public ICacheAble GetItem(String key){
            return null;
        }

        public void SetItem(String key, ICacheAble resource){
            
        }
    }
}
