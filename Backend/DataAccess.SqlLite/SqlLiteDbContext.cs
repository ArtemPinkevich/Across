using DataAccess.BaseImplementation;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.SqlLite
{
    public class SqlLiteDbContext : DatabaseContext
    {
        public SqlLiteDbContext(DbContextOptions<SqlLiteDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
