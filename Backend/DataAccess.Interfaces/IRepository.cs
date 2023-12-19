namespace DataAccess.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<T>
    {
        Task<T> GetAsync(Expression<Func<T, bool>> condition);

        Task<List<T>> GetAllAsync();

        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> condition);

        Task<List<T>> GetLastAsync(Expression<Func<T, bool>> condition, int limit);

        Task<List<T>> GetFirstAsync(Expression<Func<T, bool>> condition, int limit);

        Task AddAsync(List<T> items);

        Task UpdateAsync(T item);

        Task DeleteAsync(Expression<Func<T, bool>> condition);

        Task SaveAsync();
    }
}
