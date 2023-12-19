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

    public class ComplexWashServicesRepository : IRepository<ComplexWashService>
    {
        private readonly DatabaseContext _context;

        public ComplexWashServicesRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(List<ComplexWashService> items)
        {
            await _context.ComplexWashServices.AddRangeAsync(items);
        }

        public Task DeleteAsync(Expression<Func<ComplexWashService, bool>> condition)
        {
            IEnumerable<ComplexWashService> removeItems = _context.ComplexWashServices
                .Include(item => item.WashServices)
                .Include(item => item.CarWash)
                .Where(condition);
            _context.RemoveRange(removeItems);
            return Task.CompletedTask;
        }

        public async Task<ComplexWashService> GetAsync(Expression<Func<ComplexWashService, bool>> condition)
        {
            return await _context.ComplexWashServices
                .Include(item => item.WashServices)
                .Include(item => item.CarWash)
                .FirstOrDefaultAsync(condition);
        }

        public async Task<List<ComplexWashService>> GetAllAsync()
        {
            return await _context.ComplexWashServices
                .Include(item => item.WashServices)
                .Include(item => item.CarWash)
                .ToListAsync();
        }

        public async Task<List<ComplexWashService>> GetAllAsync(Expression<Func<ComplexWashService, bool>> condition)
        {
            return await _context.ComplexWashServices
                .Include(item => item.WashServices)
                .Include(item => item.CarWash)
                .Where(condition)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<ComplexWashService>> GetFirstAsync(Expression<Func<ComplexWashService, bool>> condition, int limit)
        {
            return await _context.ComplexWashServices
                .Include(item => item.WashServices)
                .Include(item => item.CarWash)
                .Where(condition)
                .Take(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<ComplexWashService>> GetLastAsync(Expression<Func<ComplexWashService, bool>> condition, int limit)
        {
            return await _context.ComplexWashServices
                .Include(item => item.WashServices)
                .Include(item => item.CarWash)
                .Where(condition)
                .TakeLast(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public Task UpdateAsync(ComplexWashService item)
        {
            _context.ComplexWashServices.Update(item);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
