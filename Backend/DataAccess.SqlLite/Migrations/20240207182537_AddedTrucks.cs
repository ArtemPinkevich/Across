using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class AddedTrucks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    createdId = table.Column<string>(type: "TEXT", nullable: true),
                    regNumber = table.Column<string>(type: "TEXT", nullable: true),
                    TrailerType = table.Column<int>(type: "INTEGER", nullable: false),
                    CarBodyType = table.Column<int>(type: "INTEGER", nullable: false),
                    LoadingType = table.Column<int>(type: "INTEGER", nullable: false),
                    HasLTtl = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasLiftGate = table.Column<bool>(type: "INTEGER", nullable: false),
                    HasStanchionTrailer = table.Column<bool>(type: "INTEGER", nullable: false),
                    BodyVolume = table.Column<int>(type: "INTEGER", nullable: false),
                    InnerBodyLength = table.Column<int>(type: "INTEGER", nullable: false),
                    InnerBodyWidth = table.Column<int>(type: "INTEGER", nullable: false),
                    InnerBodyHeight = table.Column<int>(type: "INTEGER", nullable: false),
                    Adr1 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr2 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr3 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr4 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr5 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr6 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr7 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr8 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Adr9 = table.Column<bool>(type: "INTEGER", nullable: false),
                    Tir = table.Column<bool>(type: "INTEGER", nullable: false),
                    Ekmt = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trucks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_UserId",
                table: "Trucks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trucks");
        }
    }
}
