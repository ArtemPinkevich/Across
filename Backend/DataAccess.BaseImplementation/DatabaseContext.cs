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
        
        //public DbSet<Driver> Drivers { set; get; }
        
        //public DbSet<Shipper> Shippers { set; get; }
        
        public DbSet<LegalInformation> LegalInformations { set; get; }

        public DbSet<Truck> Trucks { set; get; }
        
        public DbSet<Cargo> Cargos { set; get; }
        
        public DbSet<TransportationOrder> TransportationOrders { set; get; }
        
        public DbSet<TruckRequirements> TruckRequirements { set; get; }
        
        public DbSet<ContactInformation> ContactInformations { set; get; }

        public DbSet<TransportationInfo> TransportationInfos { set; get; }
        
        public DbSet<DriverRequest> DriverRequests { set; get; }
        
        public DbSet<Transportation> Transportations { set; get; }
        
        public DbSet<RoutePoint> RoutePoints { set; get; }

        public DbSet<TransportationStatusRecord> TransportationStatusRecords { set; get; }
        
        public DbSet<Document> Documents { set; get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(user => user.TransportationOrders)
                .WithOne(transportationOrder => transportationOrder.Shipper)
                .HasForeignKey(transportationOrder => transportationOrder.ShipperId)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(user => user.Trucks)
                .WithOne(truck => truck.Driver)
                .HasForeignKey(truck => truck.DriverId)
                .IsRequired();
            
            modelBuilder.Entity<User>()
                .HasMany(x => x.DriverRequests)
                .WithOne(x => x.Driver)
                .HasForeignKey(x => x.DriverId)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasOne(x => x.LegalInformation)
                .WithOne(x => x.Shipper)
                .HasForeignKey<LegalInformation>(info => info.ShipperId)
                .IsRequired();

            modelBuilder.Entity<TransportationOrder>()
                .HasMany(x => x.DriverRequests)
                .WithOne(x => x.TransportationOrder)
                .HasForeignKey(x => x.TransportationOrderId)
                .IsRequired();

            modelBuilder.Entity<Transportation>()
                .HasOne(x => x.TransportationOrder)
                .WithMany()
                .HasForeignKey(x => x.TransportationOrderId)
                .IsRequired();
            
            modelBuilder.Entity<Transportation>()
                .HasOne(x => x.Driver)
                .WithMany()
                .HasForeignKey(x => x.DriverId)
                .IsRequired();
            
            modelBuilder.Entity<Transportation>()
                .HasOne(x => x.Truck)
                .WithMany()
                .HasForeignKey(x => x.TruckId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
