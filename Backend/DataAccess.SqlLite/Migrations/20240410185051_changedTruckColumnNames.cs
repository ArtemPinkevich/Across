using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class changedTruckColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasLiftGate",
                table: "Trucks",
                newName: "HasLiftgate");

            migrationBuilder.RenameColumn(
                name: "HasLTtl",
                table: "Trucks",
                newName: "HasLtl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasLiftgate",
                table: "Trucks",
                newName: "HasLiftGate");

            migrationBuilder.RenameColumn(
                name: "HasLtl",
                table: "Trucks",
                newName: "HasLTtl");
        }
    }
}
