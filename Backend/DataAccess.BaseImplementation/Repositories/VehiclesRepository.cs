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

    public class VehiclesRepository : IRepository<Vehicle>
    {
        private readonly DatabaseContext _context;

        public VehiclesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(List<Vehicle> items)
        {
            await _context.Vehicles.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Expression<Func<Vehicle, bool>> condition)
        {
            IEnumerable<Vehicle> removeItems = _context.Vehicles.Where(condition);
            _context.RemoveRange(removeItems);
            await _context.SaveChangesAsync();
        }

        public async Task<Vehicle> GetAsync(Expression<Func<Vehicle, bool>> condition)
        {
            return await _context.Vehicles
                .Include(item => item.User)
                .FirstOrDefaultAsync(condition);
        }

        public async Task<List<Vehicle>> GetAllAsync()
        {
            return await _context.Vehicles
                .Include(item => item.User)
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetAllAsync(Expression<Func<Vehicle, bool>> condition)
        {
            return await _context.Vehicles
                .Include(item => item.User)
                .Where(condition)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetFirstAsync(Expression<Func<Vehicle, bool>> condition, int limit)
        {
            return await _context.Vehicles
                .Include(item => item.User)
                .Where(condition)
                .Take(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetLastAsync(Expression<Func<Vehicle, bool>> condition, int limit)
        {
            return await _context.Vehicles
                .Include(item => item.User)
                .Where(condition)
                .TakeLast(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task UpdateAsync(Vehicle item)
        {
            _context.Vehicles.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
