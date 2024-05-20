namespace DataAccess.BaseImplementation.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;


public class TransferChangeHistoryRepository: IRepository<TransferChangeStatusRecord>
{
    private readonly DatabaseContext _context;
    
    public TransferChangeHistoryRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<TransferChangeStatusRecord> GetAsync(Expression<Func<TransferChangeStatusRecord, bool>> condition)
    {
        return await _context.TransferChangeHistoryRecords
            .Include(item => item.TransportationOrder)
            .FirstOrDefaultAsync(condition);
    }

    public async Task<List<TransferChangeStatusRecord>> GetAllAsync()
    {
        return await _context.TransferChangeHistoryRecords
            .Include(item => item.TransportationOrder)
            .ToListAsync();
    }

    public async Task<List<TransferChangeStatusRecord>> GetAllAsync(Expression<Func<TransferChangeStatusRecord, bool>> condition)
    {
        return await _context.TransferChangeHistoryRecords
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<TransferChangeStatusRecord>> GetLastAsync(Expression<Func<TransferChangeStatusRecord, bool>> condition, int limit)
    {
        return await _context.TransferChangeHistoryRecords
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .TakeLast(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<TransferChangeStatusRecord>> GetFirstAsync(Expression<Func<TransferChangeStatusRecord, bool>> condition, int limit)
    {
        return await _context.TransferChangeHistoryRecords
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .Take(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task AddAsync(List<TransferChangeStatusRecord> items)
    {
        await _context.TransferChangeHistoryRecords.AddRangeAsync(items);
    }

    public Task UpdateAsync(TransferChangeStatusRecord item)
    {
        _context.TransferChangeHistoryRecords.Update(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Expression<Func<TransferChangeStatusRecord, bool>> condition)
    {
        IEnumerable<TransferChangeStatusRecord> removeItems = _context.TransferChangeHistoryRecords.Where(condition);
        _context.RemoveRange(removeItems);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}