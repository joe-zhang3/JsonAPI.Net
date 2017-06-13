using System;
namespace JsonAPI.Net
{
    public interface ICache<T>
    {
        T GetItem(string Key);

        void SetItem(string key, T t);
    }
}
