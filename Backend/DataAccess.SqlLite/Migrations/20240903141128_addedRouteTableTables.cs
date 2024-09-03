using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class addedRouteTableTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Transportations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoutePoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransportationId = table.Column<int>(type: "INTEGER", nullable: false),
                    LocationName = table.Column<string>(type: "TEXT", nullable: true),
                    Latitude = table.Column<string>(type: "TEXT", nullable: true),
                    Longtitude = table.Column<string>(type: "TEXT", nullable: true)
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutePoints_TransportationId",
                table: "RoutePoints",
                column: "TransportationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutePoints");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Transportations");
        }
    }
}
