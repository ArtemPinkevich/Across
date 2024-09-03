namespace DataAccess.BaseImplementation.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;


public class TransportationStatusRecordRepository: IRepository<TransportationStatusRecord>
{
    private readonly DatabaseContext _context;
    
    public TransportationStatusRecordRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<TransportationStatusRecord> GetAsync(Expression<Func<TransportationStatusRecord, bool>> condition)
    {
        return await _context.TransportationStatusRecords
            .Include(item => item.TransportationOrder)
            .FirstOrDefaultAsync(condition);
    }

    public async Task<List<TransportationStatusRecord>> GetAllAsync()
    {
        return await _context.TransportationStatusRecords
            .Include(item => item.TransportationOrder)
            .ToListAsync();
    }

    public async Task<List<TransportationStatusRecord>> GetAllAsync(Expression<Func<TransportationStatusRecord, bool>> condition)
    {
        return await _context.TransportationStatusRecords
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .AsQueryable()
            .ToListAsync();
    }
    
    public Task<List<TransportationStatusRecord>> GetAllAsync(List<Expression<Func<TransportationStatusRecord, bool>>> conditions)
    {
        return null;
    }

    public async Task<List<TransportationStatusRecord>> GetLastAsync(Expression<Func<TransportationStatusRecord, bool>> condition, int limit)
    {
        return await _context.TransportationStatusRecords
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .TakeLast(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<TransportationStatusRecord>> GetFirstAsync(Expression<Func<TransportationStatusRecord, bool>> condition, int limit)
    {
        return await _context.TransportationStatusRecords
            .Include(item => item.TransportationOrder)
            .Where(condition)
            .Take(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task AddAsync(List<TransportationStatusRecord> items)
    {
        await _context.TransportationStatusRecords.AddRangeAsync(items);
    }

    public Task UpdateAsync(TransportationStatusRecord item)
    {
        _context.TransportationStatusRecords.Update(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Expression<Func<TransportationStatusRecord, bool>> condition)
    {
        IEnumerable<TransportationStatusRecord> removeItems = _context.TransportationStatusRecords.Where(condition);
        _context.RemoveRange(removeItems);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}