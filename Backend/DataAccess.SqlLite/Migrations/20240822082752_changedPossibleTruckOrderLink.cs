using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class changedPossibleTruckOrderLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverAndOrderWishes_AspNetUsers_DriverId",
                table: "DriverAndOrderWishes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverAndOrderWishes",
                table: "DriverAndOrderWishes");

            migrationBuilder.DropIndex(
                name: "IX_DriverAndOrderWishes_OrderId",
                table: "DriverAndOrderWishes");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "DriverAndOrderWishes");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "TransportationOrders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TruckId",
                table: "DriverAndOrderWishes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverAndOrderWishes",
                table: "DriverAndOrderWishes",
                columns: new[] { "OrderId", "TruckId" });

            migrationBuilder.CreateIndex(
                name: "IX_TransportationOrders_UserId1",
                table: "TransportationOrders",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_DriverAndOrderWishes_TruckId",
                table: "DriverAndOrderWishes",
                column: "TruckId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverAndOrderWishes_Trucks_TruckId",
                table: "DriverAndOrderWishes",
                column: "TruckId",
                principalTable: "Trucks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationOrders_AspNetUsers_UserId1",
                table: "TransportationOrders",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverAndOrderWishes_Trucks_TruckId",
                table: "DriverAndOrderWishes");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationOrders_AspNetUsers_UserId1",
                table: "TransportationOrders");

            migrationBuilder.DropIndex(
                name: "IX_TransportationOrders_UserId1",
                table: "TransportationOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverAndOrderWishes",
                table: "DriverAndOrderWishes");

            migrationBuilder.DropIndex(
                name: "IX_DriverAndOrderWishes_TruckId",
                table: "DriverAndOrderWishes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TransportationOrders");

            migrationBuilder.DropColumn(
                name: "TruckId",
                table: "DriverAndOrderWishes");

            migrationBuilder.AddColumn<string>(
                name: "DriverId",
                table: "DriverAndOrderWishes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverAndOrderWishes",
                table: "DriverAndOrderWishes",
                columns: new[] { "DriverId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_DriverAndOrderWishes_OrderId",
                table: "DriverAndOrderWishes",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverAndOrderWishes_AspNetUsers_DriverId",
                table: "DriverAndOrderWishes",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
