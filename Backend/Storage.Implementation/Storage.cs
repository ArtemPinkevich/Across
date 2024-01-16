using Storage.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Storage.Implementation;

public class Storage: IStorage
{
    private readonly IDistributedCache _cache;

    public Storage(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task Add(object obj)
    {
        
        throw new NotImplementedException();
    }

    public async Task<object> Get(string id)
    {
        var obj = await _cache.GetStringAsync(id);
        if (obj == null)
        {
            return Task.FromResult<object>(null);
        }
        return JsonConvert.DeserializeObject<object>(obj);
    }

    public void Update(string id, object obj)
    {
        throw new NotImplementedException();
    }

    public void Remove(string id)
    {
        throw new NotImplementedException();
    }
}