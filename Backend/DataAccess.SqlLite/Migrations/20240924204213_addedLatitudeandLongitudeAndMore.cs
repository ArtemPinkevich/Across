using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class addedLatitudeandLongitudeAndMore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoadDateFrom",
                table: "TransportationOrders");

            migrationBuilder.DropColumn(
                name: "LoadDateTo",
                table: "TransportationOrders");

            migrationBuilder.DropColumn(
                name: "LoadingAddress",
                table: "TransportationOrders");

            migrationBuilder.DropColumn(
                name: "LoadingLocalityName",
                table: "TransportationOrders");

            migrationBuilder.DropColumn(
                name: "UnloadingAddress",
                table: "TransportationOrders");

            migrationBuilder.DropColumn(
                name: "UnloadingLocalityName",
                table: "TransportationOrders");

            migrationBuilder.RenameColumn(
                name: "Longtitude",
                table: "RoutePoints",
                newName: "Longitude");

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Trucks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Trucks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateTime",
                table: "RoutePoints",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransportationInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransportationOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoadDateFrom = table.Column<string>(type: "TEXT", nullable: true),
                    LoadDateTo = table.Column<string>(type: "TEXT", nullable: true),
                    LoadingLocalityName = table.Column<string>(type: "TEXT", nullable: true),
                    LoadingAddress = table.Column<string>(type: "TEXT", nullable: true),
                    LoadingLatitude = table.Column<string>(type: "TEXT", nullable: true),
                    LoadingLongitude = table.Column<string>(type: "TEXT", nullable: true),
                    UnloadingLocalityName = table.Column<string>(type: "TEXT", nullable: true),
                    UnloadingAddress = table.Column<string>(type: "TEXT", nullable: true),
                    UnLoadingLatitude = table.Column<string>(type: "TEXT", nullable: true),
                    UnLoadingLongitude = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationInfos_TransportationOrders_TransportationOrderId",
                        column: x => x.TransportationOrderId,
                        principalTable: "TransportationOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportationInfos_TransportationOrderId",
                table: "TransportationInfos",
                column: "TransportationOrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportationInfos");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Trucks");

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "RoutePoints");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "RoutePoints",
                newName: "Longtitude");

            migrationBuilder.AddColumn<string>(
                name: "LoadDateFrom",
                table: "TransportationOrders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoadDateTo",
                table: "TransportationOrders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoadingAddress",
                table: "TransportationOrders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoadingLocalityName",
                table: "TransportationOrders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnloadingAddress",
                table: "TransportationOrders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnloadingLocalityName",
                table: "TransportationOrders",
                type: "TEXT",
                nullable: true);
        }
    }
}
