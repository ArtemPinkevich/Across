using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class RenameHasLTtl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasLiftGate",
                table: "TruckRequirements",
                newName: "HasLiftgate");

            migrationBuilder.RenameColumn(
                name: "HasLTtl",
                table: "TruckRequirements",
                newName: "HasLtl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasLiftgate",
                table: "TruckRequirements",
                newName: "HasLiftGate");

            migrationBuilder.RenameColumn(
                name: "HasLtl",
                table: "TruckRequirements",
                newName: "HasLTtl");
        }
    }
}
