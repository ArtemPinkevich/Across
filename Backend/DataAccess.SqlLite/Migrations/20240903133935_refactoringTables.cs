using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class refactoringTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationOrders_AspNetUsers_UserId",
                table: "TransportationOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_AspNetUsers_UserId",
                table: "Trucks");

            migrationBuilder.DropTable(
                name: "DriverAndOrderWishes");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Trucks",
                newName: "DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_Trucks_UserId",
                table: "Trucks",
                newName: "IX_Trucks_DriverId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TransportationOrders",
                newName: "ShipperId");

            migrationBuilder.RenameColumn(
                name: "CurrentTransportationStatus",
                table: "TransportationOrders",
                newName: "Price");

            migrationBuilder.RenameIndex(
                name: "IX_TransportationOrders_UserId",
                table: "TransportationOrders",
                newName: "IX_TransportationOrders_ShipperId");

            migrationBuilder.RenameColumn(
                name: "TransportationStatus",
                table: "TransferChangeHistoryRecords",
                newName: "TransportationOrderStatus");

            migrationBuilder.AddColumn<int>(
                name: "CurrentTransportationOrderStatus",
                table: "TransportationOrders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReservePhoneNumber",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DriverRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DriverId = table.Column<string>(type: "TEXT", nullable: false),
                    TruckId = table.Column<int>(type: "INTEGER", nullable: false),
                    TransportationOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDateTime = table.Column<string>(type: "TEXT", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Transportations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransportationOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    DriverId = table.Column<string>(type: "TEXT", nullable: false),
                    TransportationStatus = table.Column<int>(type: "INTEGER", nullable: false)
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
                });

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
                name: "IX_Transportations_DriverId",
                table: "Transportations",
                column: "DriverId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_TransportationOrderId",
                table: "Transportations",
                column: "TransportationOrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationOrders_AspNetUsers_ShipperId",
                table: "TransportationOrders",
                column: "ShipperId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_AspNetUsers_DriverId",
                table: "Trucks",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationOrders_AspNetUsers_ShipperId",
                table: "TransportationOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Trucks_AspNetUsers_DriverId",
                table: "Trucks");

            migrationBuilder.DropTable(
                name: "DriverRequests");

            migrationBuilder.DropTable(
                name: "Transportations");

            migrationBuilder.DropColumn(
                name: "CurrentTransportationOrderStatus",
                table: "TransportationOrders");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ReservePhoneNumber",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "Trucks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Trucks_DriverId",
                table: "Trucks",
                newName: "IX_Trucks_UserId");

            migrationBuilder.RenameColumn(
                name: "ShipperId",
                table: "TransportationOrders",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "TransportationOrders",
                newName: "CurrentTransportationStatus");

            migrationBuilder.RenameIndex(
                name: "IX_TransportationOrders_ShipperId",
                table: "TransportationOrders",
                newName: "IX_TransportationOrders_UserId");

            migrationBuilder.RenameColumn(
                name: "TransportationOrderStatus",
                table: "TransferChangeHistoryRecords",
                newName: "TransportationStatus");

            migrationBuilder.CreateTable(
                name: "DriverAndOrderWishes",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    TruckId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverAndOrderWishes", x => new { x.OrderId, x.TruckId });
                    table.ForeignKey(
                        name: "FK_DriverAndOrderWishes_TransportationOrders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "TransportationOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverAndOrderWishes_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverAndOrderWishes_TruckId",
                table: "DriverAndOrderWishes",
                column: "TruckId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationOrders_AspNetUsers_UserId",
                table: "TransportationOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Trucks_AspNetUsers_UserId",
                table: "Trucks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
