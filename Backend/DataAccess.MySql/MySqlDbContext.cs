using DataAccess.BaseImplementation;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.MySql
{
    public class MySqlDbContext: DatabaseContext
    {
        public MySqlDbContext(DbContextOptions<MySqlDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
