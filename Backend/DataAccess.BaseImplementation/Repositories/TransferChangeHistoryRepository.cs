namespace DataAccess.BaseImplementation.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;


public class TransferChangeHistoryRepository: IRepository<TransportationOrderStatusRecord>
{
    private readonly DatabaseContext _context;
    
    public TransferChangeHistoryRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<TransportationOrderStatusRecord> GetAsync(Expression<Func<TransportationOrderStatusRecord, bool>> condition)
    {
        return await _context.TransferChangeHistoryRecords
            .Include(item => item.TransportationOrder)
            .FirstOrDefaultAsync(condition);
    }

    public async Task<List<TransportationOrderStatusRecord>> GetAllAsync()
    {
        return await _context.TransferChangeHistoryRecords
            .Include(item => item.TransportationOrder)
            .ToListAsync();
    }

    public async Task<List<TransportationOrderStatusRecord>> GetAllAsync(Expression<Func<TransportationOrderStatusRecord, bool>> condition)
    {
        return await _context.TransferChangeHistoryRecords
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .AsQueryable()
            .ToListAsync();
    }
    
    public Task<List<TransportationOrderStatusRecord>> GetAllAsync(List<Expression<Func<TransportationOrderStatusRecord, bool>>> conditions)
    {
        return null;
    }

    public async Task<List<TransportationOrderStatusRecord>> GetLastAsync(Expression<Func<TransportationOrderStatusRecord, bool>> condition, int limit)
    {
        return await _context.TransferChangeHistoryRecords
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .TakeLast(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<TransportationOrderStatusRecord>> GetFirstAsync(Expression<Func<TransportationOrderStatusRecord, bool>> condition, int limit)
    {
        return await _context.TransferChangeHistoryRecords
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .Take(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task AddAsync(List<TransportationOrderStatusRecord> items)
    {
        await _context.TransferChangeHistoryRecords.AddRangeAsync(items);
    }

    public Task UpdateAsync(TransportationOrderStatusRecord item)
    {
        _context.TransferChangeHistoryRecords.Update(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Expression<Func<TransportationOrderStatusRecord, bool>> condition)
    {
        IEnumerable<TransportationOrderStatusRecord> removeItems = _context.TransferChangeHistoryRecords.Where(condition);
        _context.RemoveRange(removeItems);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}