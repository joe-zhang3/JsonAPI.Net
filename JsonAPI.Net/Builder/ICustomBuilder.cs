using System;
namespace JsonAPI.Net
{
    public interface ICustomBuilder : IBuilder
    {
        Type CustomType { get; }
    }
}
