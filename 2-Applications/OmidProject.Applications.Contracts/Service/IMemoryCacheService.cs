using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Contracts.Service;

public interface IMemoryCacheService : IService
{
    void Set(string key, string value);
    string? Get(string key);
    void Delete(string key);
    void Update(string key, string value);
    TResult? GetObject<TResult>(string key);
    void SetObject<TResult>(string key, TResult value);
    void SetObject<TResult>(string key, TResult value, int expirationMinutes);
    void UpdateObject<TResult>(string key, TResult value);
}