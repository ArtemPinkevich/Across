using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class TransportationStatusDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransportationStatus",
                table: "TransportationStatusRecords");

            migrationBuilder.DropColumn(
                name: "TransportationStatus",
                table: "Transportations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransportationStatus",
                table: "TransportationStatusRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransportationStatus",
                table: "Transportations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
