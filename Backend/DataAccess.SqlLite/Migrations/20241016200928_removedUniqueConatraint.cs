using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class removedUniqueConatraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transportations_DriverId",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Transportations_TransportationOrderId",
                table: "Transportations");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_DriverId",
                table: "Transportations",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_TransportationOrderId",
                table: "Transportations",
                column: "TransportationOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transportations_DriverId",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Transportations_TransportationOrderId",
                table: "Transportations");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_DriverId",
                table: "Transportations",
                column: "DriverId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_TransportationOrderId",
                table: "Transportations",
                column: "TransportationOrderId",
                unique: true);
        }
    }
}
