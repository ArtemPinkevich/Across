using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess.BaseImplementation.Repositories;

public class PaymentRepository : IRepository<Payment>
{
    private readonly DatabaseContext _context;
    
    public PaymentRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<Payment> GetAsync(Expression<Func<Payment, bool>> condition)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(condition);
    }

    public async Task<List<Payment>> GetAllAsync()
    {
        return await _context.Payments
            .ToListAsync();
    }

    public async Task<List<Payment>> GetAllAsync(Expression<Func<Payment, bool>> condition)
    {
        return await _context.Payments
            .Where(condition)
            .AsQueryable()
            .ToListAsync();
    }
    
    public async Task<List<Payment>> GetAllAsync(List<Expression<Func<Payment, bool>>> conditions)
    {
        IQueryable<Payment> trucks = _context.Payments;
        
        foreach (var condition in conditions)
        {
            trucks = trucks.Where(condition);
        }

        return await trucks.AsQueryable().ToListAsync();
    }

    public async Task<List<Payment>> GetLastAsync(Expression<Func<Payment, bool>> condition, int limit)
    {
        return await _context.Payments
            .Where(condition)
            .TakeLast(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<Payment>> GetFirstAsync(Expression<Func<Payment, bool>> condition, int limit)
    {
        return await _context.Payments
            .Where(condition)
            .Take(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task AddAsync(List<Payment> items)
    {
        await _context.Payments.AddRangeAsync(items);
    }

    public Task UpdateAsync(Payment item)
    {
        _context.Payments.Update(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Expression<Func<Payment, bool>> condition)
    {
        IEnumerable<Payment> removeItems = _context.Payments.Where(condition);
        _context.RemoveRange(removeItems);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}