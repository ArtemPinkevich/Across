

namespace DataAccess.BaseImplementation.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Interfaces;
    using Entities;

    public class WorkScheduleRepository : IRepository<WorkSchedule>
    {
        private readonly DatabaseContext _context;

        public WorkScheduleRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddAsync(List<WorkSchedule> items)
        {
            await _context.WorkSchedules.AddRangeAsync(items);
        }

        public Task DeleteAsync(Expression<Func<WorkSchedule, bool>> condition)
        {
            IEnumerable<WorkSchedule> removeItems = _context.WorkSchedules.Where(condition);
            _context.WorkSchedules.RemoveRange(removeItems);
            return Task.CompletedTask;
        }

        public async Task<WorkSchedule> GetAsync(Expression<Func<WorkSchedule, bool>> condition)
        {
            return await _context.WorkSchedules
                .Include(item => item.CarWash)
                .FirstOrDefaultAsync(condition);
        }

        public async Task<List<WorkSchedule>> GetAllAsync()
        {
            return await _context.WorkSchedules
                .Include(item => item.CarWash)
                .ToListAsync();
        }

        public async Task<List<WorkSchedule>> GetAllAsync(Expression<Func<WorkSchedule, bool>> condition)
        {
            return await _context.WorkSchedules
                .Include(item => item.CarWash)
                .Where(condition)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<WorkSchedule>> GetFirstAsync(Expression<Func<WorkSchedule, bool>> condition, int limit)
        {
            return await _context.WorkSchedules
                .Include(item => item.CarWash)
                .Where(condition)
                .Take(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<List<WorkSchedule>> GetLastAsync(Expression<Func<WorkSchedule, bool>> condition, int limit)
        {
            return await _context.WorkSchedules
                .Include(item => item.CarWash)
                .Where(condition)
                .TakeLast(limit)
                .AsQueryable()
                .ToListAsync();
        }

        public Task UpdateAsync(WorkSchedule item)
        {
            _context.WorkSchedules.Update(item);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
