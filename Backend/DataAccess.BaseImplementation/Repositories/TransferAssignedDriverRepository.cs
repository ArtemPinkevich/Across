using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.BaseImplementation.Repositories;

public class TransferAssignedDriverRepository: IRepository<TransferAssignedDriverRecord>
{
    private readonly DatabaseContext _context;
    
    public TransferAssignedDriverRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<TransferAssignedDriverRecord> GetAsync(Expression<Func<TransferAssignedDriverRecord, bool>> condition)
    {
        return await _context.TransferAssignedDriverRecords
            .Include(item => item.TransportationOrder)
            .FirstOrDefaultAsync(condition);
    }

    public async Task<List<TransferAssignedDriverRecord>> GetAllAsync()
    {
        return await _context.TransferAssignedDriverRecords
            .Include(item => item.TransportationOrder)
            .ToListAsync();
    }
    
    public Task<List<TransferAssignedDriverRecord>> GetAllAsync(List<Expression<Func<TransferAssignedDriverRecord, bool>>> conditions)
    {
        return null;
    }

    public async Task<List<TransferAssignedDriverRecord>> GetAllAsync(Expression<Func<TransferAssignedDriverRecord, bool>> condition)
    {
        return await _context.TransferAssignedDriverRecords
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<TransferAssignedDriverRecord>> GetLastAsync(Expression<Func<TransferAssignedDriverRecord, bool>> condition, int limit)
    {
        return await _context.TransferAssignedDriverRecords
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .TakeLast(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<TransferAssignedDriverRecord>> GetFirstAsync(Expression<Func<TransferAssignedDriverRecord, bool>> condition, int limit)
    {
        return await _context.TransferAssignedDriverRecords
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .Take(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task AddAsync(List<TransferAssignedDriverRecord> items)
    {
        await _context.TransferAssignedDriverRecords.AddRangeAsync(items);
    }

    public Task UpdateAsync(TransferAssignedDriverRecord item)
    {
        _context.TransferAssignedDriverRecords.Update(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Expression<Func<TransferAssignedDriverRecord, bool>> condition)
    {
        IEnumerable<TransferAssignedDriverRecord> removeItems = _context.TransferAssignedDriverRecords.Where(condition);
        _context.RemoveRange(removeItems);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}