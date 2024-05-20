using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.BaseImplementation.Repositories;

public class TransportationOrderRepository: IRepository<TransportationOrder>
{
    private readonly DatabaseContext _context;
    
    public TransportationOrderRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<TransportationOrder> GetAsync(Expression<Func<TransportationOrder, bool>> condition)
    {
        return await _context.TransportationOrders
            .Include(item => item.User)
            .Include(item => item.Cargo)
            .Include(item => item.TransferChangeHistoryRecords)
            .Include(item => item.TransferAssignedDriverRecords)
            .Include(item => item.TruckRequirements)
            .ThenInclude(item => item.CarBodyRequirement)
            .FirstOrDefaultAsync(condition);
    }

    public async Task<List<TransportationOrder>> GetAllAsync()
    {
        return await _context.TransportationOrders
            .Include(item => item.User)
            .Include(item => item.Cargo)
            .Include(item => item.TransferChangeHistoryRecords)
            .Include(item => item.TransferAssignedDriverRecords)
            .Include(item => item.TruckRequirements)
            .ThenInclude(item => item.CarBodyRequirement)
            .ToListAsync();
    }

    public async Task<List<TransportationOrder>> GetAllAsync(Expression<Func<TransportationOrder, bool>> condition)
    {
        return await _context.TransportationOrders
            .Include(item => item.User)
            .Include(item => item.Cargo)
            .Include(item => item.TransferChangeHistoryRecords)
            .Include(item => item.TransferAssignedDriverRecords)
            .Include(item => item.TruckRequirements)
            .ThenInclude(item => item.CarBodyRequirement)
            .Where(condition)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<TransportationOrder>> GetLastAsync(Expression<Func<TransportationOrder, bool>> condition, int limit)
    {
        return await _context.TransportationOrders
            .Include(item => item.User)
            .Include(item => item.Cargo)
            .Include(item => item.TransferChangeHistoryRecords)
            .Include(item => item.TransferAssignedDriverRecords)
            .Include(item => item.TruckRequirements)
            .ThenInclude(item => item.CarBodyRequirement)
            .Where(condition)
            .TakeLast(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<TransportationOrder>> GetFirstAsync(Expression<Func<TransportationOrder, bool>> condition, int limit)
    {
        return await _context.TransportationOrders
            .Include(item => item.User)
            .Include(item => item.Cargo)
            .Include(item => item.TransferChangeHistoryRecords)
            .Include(item => item.TransferAssignedDriverRecords)
            .Include(item => item.TruckRequirements)
            .Where(condition)
            .Take(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task AddAsync(List<TransportationOrder> items)
    {
        await _context.TransportationOrders.AddRangeAsync(items);
    }

    public Task UpdateAsync(TransportationOrder item)
    {
        _context.TransportationOrders.Update(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Expression<Func<TransportationOrder, bool>> condition)
    {
        IEnumerable<TransportationOrder> removeItems = _context.TransportationOrders.Where(condition);
        _context.RemoveRange(removeItems);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}