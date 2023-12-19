using System.Collections.Immutable;

namespace DataAccess.BaseImplementation.Repositories
{
    using DataAccess.Interfaces;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class WashServicesRepository : IRepository<WashService>
    {
        private readonly DatabaseContext _context;

        public WashServicesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(List<WashService> items)
        {
            await _context.WashServices.AddRangeAsync(items);
        }

        public Task DeleteAsync(Expression<Func<WashService, bool>> condition)
        {
            IEnumerable<WashService> removeItems = _context.WashServices
                .Include(item => item.CarWash)
                .Include(item => item.ComplexWashServices)
                .Include(item => item.WashServiceSettingsList)
                .Where(condition);
            _context.RemoveRange(removeItems);
            return Task.CompletedTask;
        }

        public async Task<WashService> GetAsync(Expression<Func<WashService, bool>> condition)
        {
            return await _context.WashServices
                .Include(item => item.CarWash)
                .Include(item => item.ComplexWashServices)
                .Include(item => item.WashServiceSettingsList)
                .FirstOrDefaultAsync(condition);
        }

        public async Task<List<WashService>> GetAllAsync()
        {
            return await _context.WashServices
                .Include(item => item.CarWash)
                .Include(item => item.ComplexWashServices)
                .Include(item => item.WashServiceSettingsList)
                .ToListAsync();
        }

        public async Task<List<WashService>> GetAllAsync(Expression<Func<WashService, bool>> condition)
        {
            return await _context.WashServices
                .Include(item => item.CarWash)
                .Include(item => item.ComplexWashServices)
                .Include(item => item.WashServiceSettingsList)
                .ThenInclude(i => i.PriceGroup)
                .ThenInclude(i => i.CarBodies)
                .Where(condition)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<WashService>> GetFirstAsync(Expression<Func<WashService, bool>> condition, int limit)
        {
            return await _context.WashServices
                .Include(item => item.CarWash)
                .Include(item => item.ComplexWashServices)
                .Include(item => item.WashServiceSettingsList)
                .Where(condition)
                .Take(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<WashService>> GetLastAsync(Expression<Func<WashService, bool>> condition, int limit)
        {
            return await _context.WashServices
                .Include(item => item.CarWash)
                .Include(item => item.ComplexWashServices)
                .Include(item => item.WashServiceSettingsList)
                .Where(condition)
                .TakeLast(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public Task UpdateAsync(WashService item)
        {
            _context.WashServices.Update(item);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
