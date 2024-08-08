using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class addedDriverWishesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationOrders_AspNetUsers_UserId",
                table: "TransportationOrders");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TransportationOrders",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DriverAndOrderWishes",
                columns: table => new
                {
                    OrdersOfferedByDriverId = table.Column<int>(type: "INTEGER", nullable: false),
                    PossibleDriversId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverAndOrderWishes", x => new { x.OrdersOfferedByDriverId, x.PossibleDriversId });
                    table.ForeignKey(
                        name: "FK_DriverAndOrderWishes_AspNetUsers_PossibleDriversId",
                        column: x => x.PossibleDriversId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverAndOrderWishes_TransportationOrders_OrdersOfferedByDriverId",
                        column: x => x.OrdersOfferedByDriverId,
                        principalTable: "TransportationOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DriverAndOrderWishes_PossibleDriversId",
                table: "DriverAndOrderWishes",
                column: "PossibleDriversId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationOrders_AspNetUsers_UserId",
                table: "TransportationOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationOrders_AspNetUsers_UserId",
                table: "TransportationOrders");

            migrationBuilder.DropTable(
                name: "DriverAndOrderWishes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TransportationOrders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationOrders_AspNetUsers_UserId",
                table: "TransportationOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
