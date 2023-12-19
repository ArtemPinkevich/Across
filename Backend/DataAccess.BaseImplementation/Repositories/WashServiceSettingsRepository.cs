using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.BaseImplementation.Repositories;

public class WashServiceSettingsRepository:IRepository<WashServiceSettings>
{
        private readonly DatabaseContext _context;

        public WashServiceSettingsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(List<WashServiceSettings> items)
        {
            await _context.WashServiceSettings.AddRangeAsync(items);
        }

        public Task DeleteAsync(Expression<Func<WashServiceSettings, bool>> condition)
        {
            IEnumerable<WashServiceSettings> removeItems = _context.WashServiceSettings
                .Include(item => item.WashService)
                .Include(item => item.PriceGroup)
                .Where(condition);
            _context.WashServiceSettings.RemoveRange(removeItems);
            return Task.CompletedTask;
        }

        public async Task<WashServiceSettings> GetAsync(Expression<Func<WashServiceSettings, bool>> condition)
        {
            return await _context.WashServiceSettings
                .Include(item => item.WashService)
                .Include(item => item.PriceGroup)
                .FirstOrDefaultAsync(condition);
        }

        public async Task<List<WashServiceSettings>> GetAllAsync()
        {
            return await _context.WashServiceSettings
                .Include(item => item.WashService)
                .Include(item => item.PriceGroup)
                .ToListAsync();
        }

        public async Task<List<WashServiceSettings>> GetAllAsync(Expression<Func<WashServiceSettings, bool>> condition)
        {
            return await _context.WashServiceSettings
                .Include(item => item.WashService)
                .Include(item => item.PriceGroup)
                .Where(condition)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<WashServiceSettings>> GetFirstAsync(Expression<Func<WashServiceSettings, bool>> condition, int limit)
        {
            return await _context.WashServiceSettings
                .Include(item => item.WashService)
                .Include(item => item.PriceGroup)
                .Where(condition)
                .Take(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<WashServiceSettings>> GetLastAsync(Expression<Func<WashServiceSettings, bool>> condition, int limit)
        {
            return await _context.WashServiceSettings
                .Include(item => item.WashService)
                .Include(item => item.PriceGroup)
                .Where(condition)
                .TakeLast(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public Task UpdateAsync(WashServiceSettings item)
        {
            _context.WashServiceSettings.Update(item);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
}