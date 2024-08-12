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

    public class CarWashesRepository : IRepository<CarWash>
    {
        private readonly DatabaseContext _context;

        public CarWashesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(List<CarWash> items)
        {
            await _context.CarWashes.AddRangeAsync(items);
        }

        public Task DeleteAsync(Expression<Func<CarWash, bool>> condition)
        {
            IEnumerable<CarWash> removeItems = _context.CarWashes.Where(condition);
            _context.RemoveRange(removeItems);
            return Task.CompletedTask;
        }

        public async Task<CarWash> GetAsync(Expression<Func<CarWash, bool>> condition)
        {
            return await _context.CarWashes
                .Include(item => item.Users)
                .Include(item => item.SelectedByUsers)
                .FirstOrDefaultAsync(condition);
        }

        public async Task<List<CarWash>> GetAllAsync()
        {
            return await _context.CarWashes
                .Include(item => item.Users)
                .Include(item => item.SelectedByUsers)
                .ToListAsync();
        }
        
        public Task<List<CarWash>> GetAllAsync(List<Expression<Func<CarWash, bool>>> conditions)
        {
            return null;
        }

        public async Task<List<CarWash>> GetAllAsync(Expression<Func<CarWash, bool>> condition)
        {
            return await _context.CarWashes
                .Include(item => item.Users)
                .Include(item => item.SelectedByUsers)
                .Where(condition)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<CarWash>> GetFirstAsync(Expression<Func<CarWash, bool>> condition, int limit)
        {
            return await _context.CarWashes
                .Include(item => item.Users)
                .Include(item => item.SelectedByUsers)
                .Where(condition)
                .Take(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<CarWash>> GetLastAsync(Expression<Func<CarWash, bool>> condition, int limit)
        {
            return await _context.CarWashes
                .Include(item => item.Users)
                .Include(item => item.SelectedByUsers)
                .Where(condition)
                .TakeLast(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public Task UpdateAsync(CarWash item)
        {
            _context.CarWashes.Update(item);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
