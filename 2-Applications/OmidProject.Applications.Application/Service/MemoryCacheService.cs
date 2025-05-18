using OmidProject.Applications.Contracts.Service;
using Microsoft.Extensions.Caching.Memory;

namespace OmidProject.Applications.Application.Service;

public class MemoryCacheService : IMemoryCacheService
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public void Set(string key, string value)
    {
        _memoryCache.Set(key, value);
    }

    public string? Get(string key)
    {
        var result = _memoryCache.Get(key);
        return result?.ToString();
    }

    public void Delete(string key)
    {
        _memoryCache.Remove(key);
    }

    public void Update(string key, string value)
    {
        _memoryCache.Remove(key);
        _memoryCache.Set(key, value);
    }

    public TResult GetObject<TResult>(string key)
    {
        var value = _memoryCache.Get(key);
        return (TResult) value!;
    }

    public void SetObject<TResult>(string key, TResult value)
    {
        _memoryCache.Set(key, value);
    }

    public void SetObject<TResult>(string key, TResult value, int expirationMinutes)
    {
        _memoryCache.Set(key, value, TimeSpan.FromMinutes(expirationMinutes));
    }

    public void UpdateObject<TResult>(string key, TResult value)
    {
        _memoryCache.Remove(key);
        _memoryCache.Set(key, value);
    }
}