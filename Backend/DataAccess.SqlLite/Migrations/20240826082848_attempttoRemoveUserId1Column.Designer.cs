﻿// <auto-generated />
using System;
using DataAccess.SqlLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    [DbContext(typeof(SqlLiteDbContext))]
    [Migration("20240826082848_attempttoRemoveUserId1Column")]
    partial class attempttoRemoveUserId1Column
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("DriverAndOrderWishes", b =>
                {
                    b.Property<int>("OrdersOfferedForTruckId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("OrderId");

                    b.Property<int>("TrucksId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("TruckId");

                    b.HasKey("OrdersOfferedForTruckId", "TrucksId");

                    b.HasIndex("TrucksId");

                    b.ToTable("DriverAndOrderWishes");
                });

            modelBuilder.Entity("Entities.CarBodyRequirement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adjustable")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AllMetal")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AutoCarrier")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Autocart")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Autotower")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Barge")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("BeamTruckNgb")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("BitumenTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("BulkheadRefr")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Bus")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Cattle")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CementTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ChipTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ConcreteTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Container")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ContainerTrail")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Crane")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Dolly")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("DollyPlat")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Doppelstock")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("DualPurpose")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("DumpTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Flatbed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("FlourTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("FuelTank")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("FurageTuck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("GarbageTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Gas")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("GrainTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HorseTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Innloader")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Isothermal")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Jumbo")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Klyushkovoz")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Manipulator")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("MeatRailsRef")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Mega")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Microbus")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("OffRoader")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Opentop")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Opentrailer")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("PanelsTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Pickup")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("PipeTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Pyramid")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Refrigerator")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("RefrigeratorMult")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Ripetruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("RollTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ScrapTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("SlidingSemiTrailer2040")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TankContainer20")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TankContainer40")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TankerTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TentTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TimberTruck")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Tractor")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Tral")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TruckRequirementsId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Van")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Wrecker")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TruckRequirementsId")
                        .IsUnique();

                    b.ToTable("CarBodyRequirement");
                });

            modelBuilder.Entity("Entities.Cargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Diameter")
                        .HasColumnType("REAL");

                    b.Property<double?>("Height")
                        .HasColumnType("REAL");

                    b.Property<double?>("Length")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PackagingQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PackagingType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TransportationOrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Volume")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Weight")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Width")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("TransportationOrderId")
                        .IsUnique();

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("Entities.Document.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasColumnType("TEXT");

                    b.Property<int>("DocumentStatus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DocumentType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("Entities.TransferAssignedTruckRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ChangeDatetime")
                        .HasColumnType("TEXT");

                    b.Property<int>("TransportationOrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TruckId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TransportationOrderId");

                    b.HasIndex("TruckId");

                    b.ToTable("TransferAssignedDriverRecords");
                });

            modelBuilder.Entity("Entities.TransferChangeStatusRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ChangeDatetime")
                        .HasColumnType("TEXT");

                    b.Property<int>("TransportationOrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TransportationStatus")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TransportationOrderId");

                    b.ToTable("TransferChangeHistoryRecords");
                });

            modelBuilder.Entity("Entities.TransportationOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CurrentAssignedTruckId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentTransportationStatus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LoadDateFrom")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoadDateTo")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoadingAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoadingLocalityName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UnloadingAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("UnloadingLocalityName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId1")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CurrentAssignedTruckId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("TransportationOrders");
                });

            modelBuilder.Entity("Entities.Truck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr1")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr2")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr3")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr4")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr5")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr6")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr7")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr8")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr9")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BodyVolume")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CarBodyType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CarryingCapacity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Ekmt")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasLiftGate")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasLtl")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasStanchionTrailer")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InnerBodyHeight")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InnerBodyLength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InnerBodyWidth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LoadingType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RegNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Tir")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrailerType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TruckLocation")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Trucks");
                });

            modelBuilder.Entity("Entities.TruckRequirements", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr1")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr2")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr3")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr4")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr5")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr6")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr7")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr8")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Adr9")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BodyVolume")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CarryingCapacity")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Ekmt")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasLiftgate")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasLtl")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasStanchionTrailer")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InnerBodyHeight")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InnerBodyLength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InnerBodyWidth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LoadingType")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Tir")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TransportationOrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UnloadingType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TransportationOrderId")
                        .IsUnique();

                    b.ToTable("TruckRequirements");
                });

            modelBuilder.Entity("Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("Patronymic")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<int>("UserStatus")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DriverAndOrderWishes", b =>
                {
                    b.HasOne("Entities.TransportationOrder", null)
                        .WithMany()
                        .HasForeignKey("OrdersOfferedForTruckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Truck", null)
                        .WithMany()
                        .HasForeignKey("TrucksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.CarBodyRequirement", b =>
                {
                    b.HasOne("Entities.TruckRequirements", "TruckRequirements")
                        .WithOne("CarBodyRequirement")
                        .HasForeignKey("Entities.CarBodyRequirement", "TruckRequirementsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TruckRequirements");
                });

            modelBuilder.Entity("Entities.Cargo", b =>
                {
                    b.HasOne("Entities.TransportationOrder", "TransportationOrder")
                        .WithOne("Cargo")
                        .HasForeignKey("Entities.Cargo", "TransportationOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransportationOrder");
                });

            modelBuilder.Entity("Entities.Document.Document", b =>
                {
                    b.HasOne("Entities.User", "User")
                        .WithMany("Documents")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.TransferAssignedTruckRecord", b =>
                {
                    b.HasOne("Entities.TransportationOrder", "TransportationOrder")
                        .WithMany("AssignedTruckRecords")
                        .HasForeignKey("TransportationOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Truck", "Truck")
                        .WithMany()
                        .HasForeignKey("TruckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransportationOrder");

                    b.Navigation("Truck");
                });

            modelBuilder.Entity("Entities.TransferChangeStatusRecord", b =>
                {
                    b.HasOne("Entities.TransportationOrder", "TransportationOrder")
                        .WithMany("TransferChangeHistoryRecords")
                        .HasForeignKey("TransportationOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransportationOrder");
                });

            modelBuilder.Entity("Entities.TransportationOrder", b =>
                {
                    b.HasOne("Entities.Truck", "CurrentAssignedTruck")
                        .WithOne()
                        .HasForeignKey("Entities.TransportationOrder", "CurrentAssignedTruckId");

                    b.HasOne("Entities.User", "User")
                        .WithMany("TransportationOrders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.User", null)
                        .WithMany("OrdersOfferedByDriver")
                        .HasForeignKey("UserId1");

                    b.Navigation("CurrentAssignedTruck");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Truck", b =>
                {
                    b.HasOne("Entities.User", "User")
                        .WithMany("Trucks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.TruckRequirements", b =>
                {
                    b.HasOne("Entities.TransportationOrder", "TransportationOrder")
                        .WithOne("TruckRequirements")
                        .HasForeignKey("Entities.TruckRequirements", "TransportationOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransportationOrder");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.TransportationOrder", b =>
                {
                    b.Navigation("AssignedTruckRecords");

                    b.Navigation("Cargo");

                    b.Navigation("TransferChangeHistoryRecords");

                    b.Navigation("TruckRequirements");
                });

            modelBuilder.Entity("Entities.TruckRequirements", b =>
                {
                    b.Navigation("CarBodyRequirement");
                });

            modelBuilder.Entity("Entities.User", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("OrdersOfferedByDriver");

                    b.Navigation("TransportationOrders");

                    b.Navigation("Trucks");
                });
#pragma warning restore 612, 618
        }
    }
}
