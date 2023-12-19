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

    public class RecordsRepository : IRepository<Record>
    {
        private readonly DatabaseContext _context;

        public RecordsRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(List<Record> items)
        {
            await _context.Records.AddRangeAsync(items);
        }

        public Task DeleteAsync(Expression<Func<Record, bool>> condition)
        {
            IEnumerable<Record> removeItems = _context.Records.Where(condition);
            _context.RemoveRange(removeItems);
            return Task.CompletedTask;
        }

        public async Task<Record> GetAsync(Expression<Func<Record, bool>> condition)
        {
            return await _context.Records
                .Include(item => item.User)
                .Include(item => item.CarWash)
                .Include(item => item.Vehicle)
                .Include(item => item.WashServices)
                .FirstOrDefaultAsync(condition);
        }

        public async Task<List<Record>> GetAllAsync()
        {
            return await _context.Records
                .Include(item => item.User)
                .Include(item => item.CarWash)
                .Include(item => item.Vehicle)
                .Include(item => item.WashServices)
                .ToListAsync();
        }

        public async Task<List<Record>> GetAllAsync(Expression<Func<Record, bool>> condition)
        {
            return await _context.Records
                .Include(item => item.User)
                .Include(item => item.CarWash)
                .Include(item => item.Vehicle)
                .Include(item => item.WashServices)
                .Where(condition)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<Record>> GetFirstAsync(Expression<Func<Record, bool>> condition, int limit)
        {
            return await _context.Records
                .Include(item => item.User)
                .Include(item => item.CarWash)
                .Include(item => item.Vehicle)
                .Include(item => item.WashServices)
                .Where(condition)
                .Take(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<Record>> GetLastAsync(Expression<Func<Record, bool>> condition, int limit)
        {
            return await _context.Records
                .Include(item => item.User)
                .Include(item => item.CarWash)
                .Include(item => item.Vehicle)
                .Include(item => item.WashServices)
                .Where(condition)
                .TakeLast(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public Task UpdateAsync(Record item)
        {
            _context.Records.Update(item);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
