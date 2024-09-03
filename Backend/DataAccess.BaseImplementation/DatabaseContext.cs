namespace DataAccess.BaseImplementation
{
    using Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public abstract class DatabaseContext : IdentityDbContext<User>
    {
        protected DatabaseContext(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<Driver> Drivers { set; get; }
        
        public DbSet<Shipper> Shippers { set; get; }

        public DbSet<Truck> Trucks { set; get; }
        
        public DbSet<Cargo> Cargos { set; get; }
        
        public DbSet<TransportationOrder> TransportationOrders { set; get; }
        
        public DbSet<TruckRequirements> TruckRequirements { set; get; }
        
        public DbSet<DriverRequest> DriverRequests { set; get; }
        
        public DbSet<Transportation> Transportations { set; get; }
        
        public DbSet<RoutePoint> RoutePoints { set; get; }

        public DbSet<TransportationStatusRecord> TransportationStatusRecords { set; get; }
        
        public DbSet<Document> Documents { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipper>()
                .HasMany(user => user.TransportationOrders)
                .WithOne(transportationOrder => transportationOrder.Shipper)
                .HasForeignKey(transportationOrder => transportationOrder.ShipperId)
                .IsRequired();

            modelBuilder.Entity<Driver>()
                .HasMany(user => user.Trucks)
                .WithOne(truck => truck.Driver)
                .HasForeignKey(truck => truck.DriverId)
                .IsRequired();
            
            modelBuilder.Entity<Driver>()
                .HasMany(x => x.DriverRequests)
                .WithOne(x => x.Driver)
                .HasForeignKey(x => x.DriverId)
                .IsRequired();

            modelBuilder.Entity<TransportationOrder>()
                .HasMany(x => x.DriverRequests)
                .WithOne(x => x.TransportationOrder)
                .HasForeignKey(x => x.TransportationOrderId)
                .IsRequired();

            modelBuilder.Entity<Transportation>()
                .HasOne(x => x.TransportationOrder)
                .WithOne()
                .HasForeignKey<Transportation>(x => x.TransportationOrderId)
                .IsRequired();
            
            modelBuilder.Entity<Transportation>()
                .HasOne(x => x.Driver)
                .WithOne()
                .HasForeignKey<Transportation>(x => x.DriverId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
