using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class changedcolumnNames2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverAndOrderWishes_AspNetUsers_DriversId",
                table: "DriverAndOrderWishes");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverAndOrderWishes_TransportationOrders_OrdersOfferedByDriverId",
                table: "DriverAndOrderWishes");

            migrationBuilder.RenameColumn(
                name: "OrdersOfferedByDriverId",
                table: "DriverAndOrderWishes",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "DriversId",
                table: "DriverAndOrderWishes",
                newName: "DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverAndOrderWishes_OrdersOfferedByDriverId",
                table: "DriverAndOrderWishes",
                newName: "IX_DriverAndOrderWishes_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverAndOrderWishes_AspNetUsers_DriverId",
                table: "DriverAndOrderWishes",
                column: "DriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverAndOrderWishes_TransportationOrders_OrderId",
                table: "DriverAndOrderWishes",
                column: "OrderId",
                principalTable: "TransportationOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverAndOrderWishes_AspNetUsers_DriverId",
                table: "DriverAndOrderWishes");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverAndOrderWishes_TransportationOrders_OrderId",
                table: "DriverAndOrderWishes");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "DriverAndOrderWishes",
                newName: "OrdersOfferedByDriverId");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "DriverAndOrderWishes",
                newName: "DriversId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverAndOrderWishes_OrderId",
                table: "DriverAndOrderWishes",
                newName: "IX_DriverAndOrderWishes_OrdersOfferedByDriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverAndOrderWishes_AspNetUsers_DriversId",
                table: "DriverAndOrderWishes",
                column: "DriversId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverAndOrderWishes_TransportationOrders_OrdersOfferedByDriverId",
                table: "DriverAndOrderWishes",
                column: "OrdersOfferedByDriverId",
                principalTable: "TransportationOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
