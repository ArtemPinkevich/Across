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

    public class PriceGroupRepository : IRepository<PriceGroup>
    {
        private readonly DatabaseContext _context;

        public PriceGroupRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(List<PriceGroup> items)
        {
            await _context.PriceGroups.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Expression<Func<PriceGroup, bool>> condition)
        {
            IEnumerable<PriceGroup> removeItems = _context.PriceGroups
                .Include(item => item.CarWash)
                .Include(item => item.CarBodies)
                .Where(condition);
            _context.RemoveRange(removeItems);
            await _context.SaveChangesAsync();
        }

        public async Task<PriceGroup> GetAsync(Expression<Func<PriceGroup, bool>> condition)
        {
            return await _context.PriceGroups
                .Include(item => item.CarWash)
                .Include(item => item.CarBodies)
                .FirstOrDefaultAsync(condition);
        }

        public async Task<List<PriceGroup>> GetAllAsync()
        {
            return await _context.PriceGroups
                .Include(item => item.CarWash)
                .Include(item => item.CarBodies)
                .ToListAsync();
        }

        public async Task<List<PriceGroup>> GetAllAsync(Expression<Func<PriceGroup, bool>> condition)
        {
            return await _context.PriceGroups
                .Include(item => item.CarWash)
                .Include(item => item.CarBodies)
                .Where(condition)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<PriceGroup>> GetFirstAsync(Expression<Func<PriceGroup, bool>> condition, int limit)
        {
            return await _context.PriceGroups
                .Include(item => item.CarWash)
                .Include(item => item.CarBodies)
                .Where(condition)
                .Take(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<PriceGroup>> GetLastAsync(Expression<Func<PriceGroup, bool>> condition, int limit)
        {
            return await _context.PriceGroups
                .Include(item => item.CarWash)
                .Include(item => item.CarBodies)
                .Where(condition)
                .TakeLast(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task UpdateAsync(PriceGroup item)
        {
            _context.PriceGroups.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
