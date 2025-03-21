﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccess.Interfaces;
using Entities;

namespace DataAccess.BaseImplementation.Repositories;

public class TrucksRepository:IRepository<Truck>
{
    private readonly DatabaseContext _context;
    
    public TrucksRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<Truck> GetAsync(Expression<Func<Truck, bool>> condition)
    {
        return await _context.Trucks
            .Include(item => item.Driver)
            .FirstOrDefaultAsync(condition);
    }

    public async Task<List<Truck>> GetAllAsync()
    {
        return await _context.Trucks
            .Include(item => item.Driver)
            .ToListAsync();
    }

    public async Task<List<Truck>> GetAllAsync(Expression<Func<Truck, bool>> condition)
    {
        return await _context.Trucks
            .Include(item => item.Driver)
            .Where(condition)
            .AsQueryable()
            .ToListAsync();
    }
    
    public async Task<List<Truck>> GetAllAsync(List<Expression<Func<Truck, bool>>> conditions)
    {
        IQueryable<Truck> trucks = _context.Trucks.Include(item => item.Driver);
        
        foreach (var condition in conditions)
        {
            trucks = trucks.Where(condition);
        }

        return await trucks.AsQueryable().ToListAsync();
    }

    public async Task<List<Truck>> GetLastAsync(Expression<Func<Truck, bool>> condition, int limit)
    {
        return await _context.Trucks
            .Include(item => item.Driver)
            .Where(condition)
            .TakeLast(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task<List<Truck>> GetFirstAsync(Expression<Func<Truck, bool>> condition, int limit)
    {
        return await _context.Trucks
            .Include(item => item.Driver)
            .Where(condition)
            .Take(limit)
            .AsQueryable()
            .ToListAsync();
    }

    public async Task AddAsync(List<Truck> items)
    {
        await _context.Trucks.AddRangeAsync(items);
    }

    public Task UpdateAsync(Truck item)
    {
        _context.Trucks.Update(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Expression<Func<Truck, bool>> condition)
    {
        IEnumerable<Truck> removeItems = _context.Trucks.Where(condition);
        _context.RemoveRange(removeItems);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}