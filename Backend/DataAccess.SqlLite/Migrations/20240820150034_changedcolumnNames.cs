using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class changedcolumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverAndOrderWishes_AspNetUsers_PossibleDriversId",
                table: "DriverAndOrderWishes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverAndOrderWishes",
                table: "DriverAndOrderWishes");

            migrationBuilder.DropIndex(
                name: "IX_DriverAndOrderWishes_PossibleDriversId",
                table: "DriverAndOrderWishes");

            migrationBuilder.RenameColumn(
                name: "PossibleDriversId",
                table: "DriverAndOrderWishes",
                newName: "DriversId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverAndOrderWishes",
                table: "DriverAndOrderWishes",
                columns: new[] { "DriversId", "OrdersOfferedByDriverId" });

            migrationBuilder.CreateIndex(
                name: "IX_DriverAndOrderWishes_OrdersOfferedByDriverId",
                table: "DriverAndOrderWishes",
                column: "OrdersOfferedByDriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverAndOrderWishes_AspNetUsers_DriversId",
                table: "DriverAndOrderWishes",
                column: "DriversId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverAndOrderWishes_AspNetUsers_DriversId",
                table: "DriverAndOrderWishes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverAndOrderWishes",
                table: "DriverAndOrderWishes");

            migrationBuilder.DropIndex(
                name: "IX_DriverAndOrderWishes_OrdersOfferedByDriverId",
                table: "DriverAndOrderWishes");

            migrationBuilder.RenameColumn(
                name: "DriversId",
                table: "DriverAndOrderWishes",
                newName: "PossibleDriversId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverAndOrderWishes",
                table: "DriverAndOrderWishes",
                columns: new[] { "OrdersOfferedByDriverId", "PossibleDriversId" });

            migrationBuilder.CreateIndex(
                name: "IX_DriverAndOrderWishes_PossibleDriversId",
                table: "DriverAndOrderWishes",
                column: "PossibleDriversId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverAndOrderWishes_AspNetUsers_PossibleDriversId",
                table: "DriverAndOrderWishes",
                column: "PossibleDriversId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
