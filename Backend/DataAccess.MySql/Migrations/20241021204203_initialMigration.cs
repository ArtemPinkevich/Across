using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.MySql.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Patronymic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    ReservePhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserStatus = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    DocumentStatus = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LegalInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShipperId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Bin = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VatSeria = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankBin = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankSwiftCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AccountNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LegalAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyCeo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalInformations_AspNetUsers_ShipperId",
                        column: x => x.ShipperId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TransportationOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ShipperId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    TransportationOrderStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationOrders_AspNetUsers_ShipperId",
                        column: x => x.ShipperId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TruckLocation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitude = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RegNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TrailerType = table.Column<int>(type: "int", nullable: false),
                    CarBodyType = table.Column<int>(type: "int", nullable: false),
                    LoadingType = table.Column<int>(type: "int", nullable: false),
                    HasLtl = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasLiftGate = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasStanchionTrailer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CarryingCapacity = table.Column<int>(type: "int", nullable: false),
                    BodyVolume = table.Column<int>(type: "int", nullable: false),
                    InnerBodyLength = table.Column<int>(type: "int", nullable: false),
                    InnerBodyWidth = table.Column<int>(type: "int", nullable: false),
                    InnerBodyHeight = table.Column<int>(type: "int", nullable: false),
                    Adr1 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr2 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr3 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr4 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr5 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr6 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr7 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr8 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr9 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Tir = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Ekmt = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DriverId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trucks_AspNetUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Volume = table.Column<int>(type: "int", nullable: false),
                    PackagingType = table.Column<int>(type: "int", nullable: false),
                    PackagingQuantity = table.Column<int>(type: "int", nullable: true),
                    Length = table.Column<double>(type: "double", nullable: true),
                    Width = table.Column<double>(type: "double", nullable: true),
                    Height = table.Column<double>(type: "double", nullable: true),
                    Diameter = table.Column<double>(type: "double", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    TransportationOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargos_TransportationOrders_TransportationOrderId",
                        column: x => x.TransportationOrderId,
                        principalTable: "TransportationOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ContactInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransportationOrderId = table.Column<int>(type: "int", nullable: false),
                    LoadingTime = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoadingContactPerson = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoadingContactPhone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnloadingContactPerson = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnloadingContactPhone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInformations_TransportationOrders_TransportationOrder~",
                        column: x => x.TransportationOrderId,
                        principalTable: "TransportationOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TransportationInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransportationOrderId = table.Column<int>(type: "int", nullable: false),
                    LoadDateFrom = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoadDateTo = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoadingLocalityName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoadingAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoadingLatitude = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoadingLongitude = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnloadingLocalityName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnloadingAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnloadingLatitude = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UnloadingLongitude = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationInfos_TransportationOrders_TransportationOrder~",
                        column: x => x.TransportationOrderId,
                        principalTable: "TransportationOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TruckRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransportationOrderId = table.Column<int>(type: "int", nullable: false),
                    LoadingType = table.Column<int>(type: "int", nullable: false),
                    UnloadingType = table.Column<int>(type: "int", nullable: false),
                    HasLtl = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasLiftgate = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HasStanchionTrailer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CarryingCapacity = table.Column<int>(type: "int", nullable: false),
                    BodyVolume = table.Column<int>(type: "int", nullable: false),
                    InnerBodyLength = table.Column<int>(type: "int", nullable: false),
                    InnerBodyWidth = table.Column<int>(type: "int", nullable: false),
                    InnerBodyHeight = table.Column<int>(type: "int", nullable: false),
                    Adr1 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr2 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr3 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr4 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr5 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr6 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr7 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr8 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adr9 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Tir = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Ekmt = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TruckRequirements_TransportationOrders_TransportationOrderId",
                        column: x => x.TransportationOrderId,
                        principalTable: "TransportationOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DriverRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DriverId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TruckId = table.Column<int>(type: "int", nullable: false),
                    TransportationOrderId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverRequests_AspNetUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverRequests_TransportationOrders_TransportationOrderId",
                        column: x => x.TransportationOrderId,
                        principalTable: "TransportationOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverRequests_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transportations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransportationOrderId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TruckId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transportations_AspNetUsers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transportations_TransportationOrders_TransportationOrderId",
                        column: x => x.TransportationOrderId,
                        principalTable: "TransportationOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transportations_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CarBodyRequirement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TruckRequirementsId = table.Column<int>(type: "int", nullable: false),
                    TentTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Container = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Van = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AllMetal = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Isothermal = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Refrigerator = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RefrigeratorMult = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BulkheadRefr = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MeatRailsRef = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Flatbed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Opentop = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Opentrailer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DumpTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Barge = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Dolly = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DollyPlat = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Adjustable = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Tral = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BeamTruckNgb = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Bus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Autocart = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Autotower = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AutoCarrier = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ConcreteTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BitumenTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FuelTank = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    OffRoader = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Gas = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    GrainTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HorseTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ContainerTrail = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FurageTuck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Crane = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TimberTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ScrapTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Manipulator = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Microbus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FlourTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PanelsTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Pickup = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Ripetruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Pyramid = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RollTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Tractor = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Cattle = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Innloader = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PipeTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CementTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TankerTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ChipTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Wrecker = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DualPurpose = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Klyushkovoz = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    GarbageTruck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Jumbo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TankContainer20 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TankContainer40 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Mega = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Doppelstock = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SlidingSemiTrailer2040 = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodyRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarBodyRequirement_TruckRequirements_TruckRequirementsId",
                        column: x => x.TruckRequirementsId,
                        principalTable: "TruckRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoutePoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransportationId = table.Column<int>(type: "int", nullable: false),
                    LocationName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Latitude = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Longitude = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateTime = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutePoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutePoints_Transportations_TransportationId",
                        column: x => x.TransportationId,
                        principalTable: "Transportations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TransportationStatusRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransportationId = table.Column<int>(type: "int", nullable: false),
                    ChangeDatetime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationStatusRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationStatusRecords_Transportations_TransportationId",
                        column: x => x.TransportationId,
                        principalTable: "Transportations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarBodyRequirement_TruckRequirementsId",
                table: "CarBodyRequirement",
                column: "TruckRequirementsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_TransportationOrderId",
                table: "Cargos",
                column: "TransportationOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformations_TransportationOrderId",
                table: "ContactInformations",
                column: "TransportationOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_UserId",
                table: "Documents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverRequests_DriverId",
                table: "DriverRequests",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverRequests_TransportationOrderId",
                table: "DriverRequests",
                column: "TransportationOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverRequests_TruckId",
                table: "DriverRequests",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalInformations_ShipperId",
                table: "LegalInformations",
                column: "ShipperId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoutePoints_TransportationId",
                table: "RoutePoints",
                column: "TransportationId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationInfos_TransportationOrderId",
                table: "TransportationInfos",
                column: "TransportationOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransportationOrders_ShipperId",
                table: "TransportationOrders",
                column: "ShipperId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_DriverId",
                table: "Transportations",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_TransportationOrderId",
                table: "Transportations",
                column: "TransportationOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_TruckId",
                table: "Transportations",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationStatusRecords_TransportationId",
                table: "TransportationStatusRecords",
                column: "TransportationId");

            migrationBuilder.CreateIndex(
                name: "IX_TruckRequirements_TransportationOrderId",
                table: "TruckRequirements",
                column: "TransportationOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_DriverId",
                table: "Trucks",
                column: "DriverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CarBodyRequirement");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "ContactInformations");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "DriverRequests");

            migrationBuilder.DropTable(
                name: "LegalInformations");

            migrationBuilder.DropTable(
                name: "RoutePoints");

            migrationBuilder.DropTable(
                name: "TransportationInfos");

            migrationBuilder.DropTable(
                name: "TransportationStatusRecords");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TruckRequirements");

            migrationBuilder.DropTable(
                name: "Transportations");

            migrationBuilder.DropTable(
                name: "TransportationOrders");

            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
