namespace DataAccess.BaseImplementation.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;


public class TransferChangeHistoryRepository: IRepository<TransferChangeHistoryRecord>
{
    private readonly DatabaseContext _context;
    
    public TransferChangeHistoryRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<TransferChangeHistoryRecord> GetAsync(Expression<Func<TransferChangeHistoryRecord, bool>> condition)
    {
        return await _context.TransferChangeHistoryRecords
            .Include(item => item.TransportationOrder)
            .FirstOrDefaultAsync(condition);
    }

    public async Task<List<TransferChangeHistoryRecord>> GetAllAsync()
    {
        return await _context.TransferChangeHistoryRecords
            .Include(item => item.TransportationOrder)
            .ToListAsync();
    }

    public async Task<List<TransferChangeHistoryRecord>> GetAllAsync(Expression<Func<TransferChangeHistoryRecord, bool>> condition)
    {
        return await _context.TransferChangeHistoryRecords
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<TransferChangeHistoryRecord>> GetLastAsync(Expression<Func<TransferChangeHistoryRecord, bool>> condition, int limit)
    {
        return await _context.TransferChangeHistoryRecords
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .TakeLast(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<TransferChangeHistoryRecord>> GetFirstAsync(Expression<Func<TransferChangeHistoryRecord, bool>> condition, int limit)
    {
        return await _context.TransferChangeHistoryRecords
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .Take(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task AddAsync(List<TransferChangeHistoryRecord> items)
    {
        await _context.TransferChangeHistoryRecords.AddRangeAsync(items);
    }

    public Task UpdateAsync(TransferChangeHistoryRecord item)
    {
        _context.TransferChangeHistoryRecords.Update(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Expression<Func<TransferChangeHistoryRecord, bool>> condition)
    {
        IEnumerable<TransferChangeHistoryRecord> removeItems = _context.TransferChangeHistoryRecords.Where(condition);
        _context.RemoveRange(removeItems);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}