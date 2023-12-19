using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.BaseImplementation.Repositories
{
    public class CarBodiesRepository : IRepository<CarBody>
    {
        private readonly DatabaseContext _context;

        public CarBodiesRepository(DatabaseContext context)
        {
            _context = context;
        }
        
        public async Task<CarBody> GetAsync(Expression<Func<CarBody, bool>> condition)
        {
            return await _context.CarBodies
                .Include(item => item.PriceGroups)
                .FirstOrDefaultAsync(condition);
        }

        public async Task<List<CarBody>> GetAllAsync()
        {
            return await _context.CarBodies
                .Include(item => item.PriceGroups)
                .ToListAsync();
        }

        public async Task<List<CarBody>> GetAllAsync(Expression<Func<CarBody, bool>> condition)
        {
            return await _context.CarBodies
                .Include(item => item.PriceGroups)
                .Where(condition)
                .ToListAsync();
        }

        public async Task<List<CarBody>> GetLastAsync(Expression<Func<CarBody, bool>> condition, int limit)
        {
            return await _context.CarBodies
                .Include(item => item.PriceGroups)
                .Where(condition)
                .TakeLast(limit)
                .ToListAsync();
        }

        public async Task<List<CarBody>> GetFirstAsync(Expression<Func<CarBody, bool>> condition, int limit)
        {
            return await _context.CarBodies
                .Include(item => item.PriceGroups)
                .Where(condition)
                .Take(limit)
                .ToListAsync();
        }

        public async Task AddAsync(List<CarBody> items)
        {
            await _context.CarBodies.AddRangeAsync(items);
        }

        public Task UpdateAsync(CarBody item)
        {
            _context.CarBodies.Update(item);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Expression<Func<CarBody, bool>> condition)
        {
            IEnumerable<CarBody> removeItems = _context.CarBodies.Where(condition);
            _context.RemoveRange(removeItems);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}