using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess.BaseImplementation.Repositories;

public class TransportationOrderRepository: IRepository<TransportationOrder>
{
    public Task<TransportationOrder> GetAsync(Expression<Func<TransportationOrder, bool>> condition)
    {
        throw new NotImplementedException();
    }

    public Task<List<TransportationOrder>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<TransportationOrder>> GetAllAsync(Expression<Func<TransportationOrder, bool>> condition)
    {
        throw new NotImplementedException();
    }

    public Task<List<TransportationOrder>> GetLastAsync(Expression<Func<TransportationOrder, bool>> condition, int limit)
    {
        throw new NotImplementedException();
    }

    public Task<List<TransportationOrder>> GetFirstAsync(Expression<Func<TransportationOrder, bool>> condition, int limit)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(List<TransportationOrder> items)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TransportationOrder item)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Expression<Func<TransportationOrder, bool>> condition)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}