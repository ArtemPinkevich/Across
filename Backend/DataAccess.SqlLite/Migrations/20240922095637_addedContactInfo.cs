using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class addedContactInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransportationOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoadingTime = table.Column<string>(type: "TEXT", nullable: true),
                    LoadingContactPerson = table.Column<string>(type: "TEXT", nullable: true),
                    LoadingContactPhone = table.Column<string>(type: "TEXT", nullable: true),
                    UnloadingContactPerson = table.Column<string>(type: "TEXT", nullable: true),
                    UnloadingContactPhone = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInformations_TransportationOrders_TransportationOrderId",
                        column: x => x.TransportationOrderId,
                        principalTable: "TransportationOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformations_TransportationOrderId",
                table: "ContactInformations",
                column: "TransportationOrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInformations");
        }
    }
}
