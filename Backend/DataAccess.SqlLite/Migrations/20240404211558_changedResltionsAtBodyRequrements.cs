using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class changedResltionsAtBodyRequrements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TruckRequirements_CarBodyRequirement_CarBodyRequirementId",
                table: "TruckRequirements");

            migrationBuilder.DropIndex(
                name: "IX_TruckRequirements_CarBodyRequirementId",
                table: "TruckRequirements");

            migrationBuilder.DropColumn(
                name: "CarBodyRequirementId",
                table: "TruckRequirements");

            migrationBuilder.AddColumn<int>(
                name: "TruckRequirementsId",
                table: "CarBodyRequirement",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CarBodyRequirement_TruckRequirementsId",
                table: "CarBodyRequirement",
                column: "TruckRequirementsId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CarBodyRequirement_TruckRequirements_TruckRequirementsId",
                table: "CarBodyRequirement",
                column: "TruckRequirementsId",
                principalTable: "TruckRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarBodyRequirement_TruckRequirements_TruckRequirementsId",
                table: "CarBodyRequirement");

            migrationBuilder.DropIndex(
                name: "IX_CarBodyRequirement_TruckRequirementsId",
                table: "CarBodyRequirement");

            migrationBuilder.DropColumn(
                name: "TruckRequirementsId",
                table: "CarBodyRequirement");

            migrationBuilder.AddColumn<int>(
                name: "CarBodyRequirementId",
                table: "TruckRequirements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TruckRequirements_CarBodyRequirementId",
                table: "TruckRequirements",
                column: "CarBodyRequirementId");

            migrationBuilder.AddForeignKey(
                name: "FK_TruckRequirements_CarBodyRequirement_CarBodyRequirementId",
                table: "TruckRequirements",
                column: "CarBodyRequirementId",
                principalTable: "CarBodyRequirement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
