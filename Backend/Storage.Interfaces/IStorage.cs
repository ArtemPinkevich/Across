namespace Storage.Interfaces;

public interface IStorage
{
    Task Add(object obj);

    Task<object> Get(string id);

    void Update(string id, object obj);

    void Remove(string id);
}