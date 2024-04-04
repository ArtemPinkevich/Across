using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class AddedOrdersRepository : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_AspNetUsers_UserId",
                table: "Cargos");

            migrationBuilder.DropIndex(
                name: "IX_Cargos_UserId",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cargos");

            migrationBuilder.RenameColumn(
                name: "regNumber",
                table: "Trucks",
                newName: "RegNumber");

            migrationBuilder.RenameColumn(
                name: "createdId",
                table: "Trucks",
                newName: "CreatedId");

            migrationBuilder.AddColumn<int>(
                name: "CarryingCapacity",
                table: "Trucks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransportationOrderId",
                table: "Cargos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CarBodyRequirement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TentTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    Container = table.Column<bool>(type: "INTEGER", nullable: false),
                    Van = table.Column<bool>(type: "INTEGER", nullable: false),
                    AllMetal = table.Column<bool>(type: "INTEGER", nullable: false),
                    Isothermal = table.Column<bool>(type: "INTEGER", nullable: false),
                    Refrigerator = table.Column<bool>(type: "INTEGER", nullable: false),
                    RefrigeratorMult = table.Column<bool>(type: "INTEGER", nullable: false),
                    BulkheadRefr = table.Column<bool>(type: "INTEGER", nullable: false),
                    MeatRailsRef = table.Column<bool>(type: "INTEGER", nullable: false),
                    Flatbed = table.Column<bool>(type: "INTEGER", nullable: false),
                    Opentop = table.Column<bool>(type: "INTEGER", nullable: false),
                    Opentrailer = table.Column<bool>(type: "INTEGER", nullable: false),
                    DumpTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    Barge = table.Column<bool>(type: "INTEGER", nullable: false),
                    Dolly = table.Column<bool>(type: "INTEGER", nullable: false),
                    DollyPlat = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adjustable = table.Column<bool>(type: "INTEGER", nullable: false),
                    Tral = table.Column<bool>(type: "INTEGER", nullable: false),
                    BeamTruckNgb = table.Column<bool>(type: "INTEGER", nullable: false),
                    Bus = table.Column<bool>(type: "INTEGER", nullable: false),
                    Autocart = table.Column<bool>(type: "INTEGER", nullable: false),
                    Autotower = table.Column<bool>(type: "INTEGER", nullable: false),
                    AutoCarrier = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConcreteTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    BitumenTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    FuelTank = table.Column<bool>(type: "INTEGER", nullable: false),
                    OffRoader = table.Column<bool>(type: "INTEGER", nullable: false),
                    Gas = table.Column<bool>(type: "INTEGER", nullable: false),
                    GrainTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    HorseTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    ContainerTrail = table.Column<bool>(type: "INTEGER", nullable: false),
                    FurageTuck = table.Column<bool>(type: "INTEGER", nullable: false),
                    Crane = table.Column<bool>(type: "INTEGER", nullable: false),
                    TimberTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    ScrapTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    Manipulator = table.Column<bool>(type: "INTEGER", nullable: false),
                    Microbus = table.Column<bool>(type: "INTEGER", nullable: false),
                    FlourTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    PanelsTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    Pickup = table.Column<bool>(type: "INTEGER", nullable: false),
                    Ripetruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    Pyramid = table.Column<bool>(type: "INTEGER", nullable: false),
                    RollTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    Tractor = table.Column<bool>(type: "INTEGER", nullable: false),
                    Cattle = table.Column<bool>(type: "INTEGER", nullable: false),
                    Innloader = table.Column<bool>(type: "INTEGER", nullable: false),
                    PipeTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    CementTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    TankerTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    ChipTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    Wrecker = table.Column<bool>(type: "INTEGER", nullable: false),
                    DualPurpose = table.Column<bool>(type: "INTEGER", nullable: false),
                    Klyushkovoz = table.Column<bool>(type: "INTEGER", nullable: false),
                    GarbageTruck = table.Column<bool>(type: "INTEGER", nullable: false),
                    Jumbo = table.Column<bool>(type: "INTEGER", nullable: false),
                    TankContainer20 = table.Column<bool>(type: "INTEGER", nullable: false),
                    TankContainer40 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Mega = table.Column<bool>(type: "INTEGER", nullable: false),
                    Doppelstock = table.Column<bool>(type: "INTEGER", nullable: false),
                    SlidingSemiTrailer2040 = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarBodyRequirement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportationOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    TransportationStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    LoadDateFrom = table.Column<string>(type: "TEXT", nullable: true),
                    LoadDateTo = table.Column<string>(type: "TEXT", nullable: true),
                    LoadingLocalityName = table.Column<string>(type: "TEXT", nullable: true),
                    LoadingAddress = table.Column<string>(type: "TEXT", nullable: true),
                    UnloadingLocalityName = table.Column<string>(type: "TEXT", nullable: true),
                    UnloadingAddress = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TruckRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransportationOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoadingType = table.Column<int>(type: "INTEGER", nullable: false),
                    UnloadingType = table.Column<int>(type: "INTEGER", nullable: false),
                    CarBodyRequirementId = table.Column<int>(type: "INTEGER", nullable: false),
                    HasLTtl = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasLiftGate = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasStanchionTrailer = table.Column<bool>(type: "INTEGER", nullable: false),
                    CarryingCapacity = table.Column<int>(type: "INTEGER", nullable: false),
                    Adr1 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr2 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr3 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr4 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr5 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr6 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr7 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr8 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr9 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Tir = table.Column<bool>(type: "INTEGER", nullable: false),
                    Ekmt = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TruckRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TruckRequirements_CarBodyRequirement_CarBodyRequirementId",
                        column: x => x.CarBodyRequirementId,
                        principalTable: "CarBodyRequirement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TruckRequirements_TransportationOrders_TransportationOrderId",
                        column: x => x.TransportationOrderId,
                        principalTable: "TransportationOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_TransportationOrderId",
                table: "Cargos",
                column: "TransportationOrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransportationOrders_UserId",
                table: "TransportationOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TruckRequirements_CarBodyRequirementId",
                table: "TruckRequirements",
                column: "CarBodyRequirementId");

            migrationBuilder.CreateIndex(
                name: "IX_TruckRequirements_TransportationOrderId",
                table: "TruckRequirements",
                column: "TransportationOrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_TransportationOrders_TransportationOrderId",
                table: "Cargos",
                column: "TransportationOrderId",
                principalTable: "TransportationOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargos_TransportationOrders_TransportationOrderId",
                table: "Cargos");

            migrationBuilder.DropTable(
                name: "TruckRequirements");

            migrationBuilder.DropTable(
                name: "CarBodyRequirement");

            migrationBuilder.DropTable(
                name: "TransportationOrders");

            migrationBuilder.DropIndex(
                name: "IX_Cargos_TransportationOrderId",
                table: "Cargos");

            migrationBuilder.DropColumn(
                name: "CarryingCapacity",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "TransportationOrderId",
                table: "Cargos");

            migrationBuilder.RenameColumn(
                name: "RegNumber",
                table: "Trucks",
                newName: "regNumber");

            migrationBuilder.RenameColumn(
                name: "CreatedId",
                table: "Trucks",
                newName: "createdId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Cargos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cargos_UserId",
                table: "Cargos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargos_AspNetUsers_UserId",
                table: "Cargos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
