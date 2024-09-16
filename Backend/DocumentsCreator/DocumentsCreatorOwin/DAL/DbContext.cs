using DocumentsCreatorOwin.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentsCreatorOwin.DAL
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<AspNetUser> AspNetUsers { set; get; }
        
        public DbSet<LegalInformation> LegalInformations { set; get; }
        
        public DbSet<Cargo> Cargos { set; get; }
        
        public DbSet<Transportation> Transportations { set; get; }

        public DbSet<TransportationOrder> TransportationOrders { set; get; }
        
        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
            
        }
    }
}