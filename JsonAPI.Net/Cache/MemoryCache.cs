using System;
namespace JsonAPI.Net
{
    public class MemoryCache : ICache<ICacheable>
    {
        public ICacheable GetItem(String key){
            return null;
        }

        public void SetItem(String key, ICacheable resource){
            
        }
    }
}
