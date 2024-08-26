using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class deleteCarWashRepository : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CarWashes_CarWashId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CarWashes_CarWashId1",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CarWashes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CarWashId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CarWashId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CarWashId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CarWashId1",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarWashId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarWashId1",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarWashes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BoxesQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    EndWorkTime = table.Column<string>(type: "TEXT", nullable: true),
                    Latitude = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Longitude = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    ReservedMinutesBetweenRecords = table.Column<int>(type: "INTEGER", nullable: false),
                    StartWorkTime = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarWashes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CarWashId",
                table: "AspNetUsers",
                column: "CarWashId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CarWashId1",
                table: "AspNetUsers",
                column: "CarWashId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CarWashes_CarWashId",
                table: "AspNetUsers",
                column: "CarWashId",
                principalTable: "CarWashes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CarWashes_CarWashId1",
                table: "AspNetUsers",
                column: "CarWashId1",
                principalTable: "CarWashes",
                principalColumn: "Id");
        }
    }
}
