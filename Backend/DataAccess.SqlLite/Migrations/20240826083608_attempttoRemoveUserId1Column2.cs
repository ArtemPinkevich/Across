using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class attempttoRemoveUserId1Column2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationOrders_AspNetUsers_UserId1",
                table: "TransportationOrders");

            migrationBuilder.DropIndex(
                name: "IX_TransportationOrders_UserId1",
                table: "TransportationOrders");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TransportationOrders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "TransportationOrders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransportationOrders_UserId1",
                table: "TransportationOrders",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationOrders_AspNetUsers_UserId1",
                table: "TransportationOrders",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
