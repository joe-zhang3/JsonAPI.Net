using System;
namespace JsonAPI.Net.Cache
{
    public interface ICache<T>
    {
        T GetItem(string Key);

        void SetItem(string key, T t);
    }
}
