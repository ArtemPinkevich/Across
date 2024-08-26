using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class currentOrderStatusAndTruckAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasLiftgate",
                table: "Trucks",
                newName: "HasLiftGate");

            migrationBuilder.AddColumn<int>(
                name: "BodyVolume",
                table: "TruckRequirements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InnerBodyHeight",
                table: "TruckRequirements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InnerBodyLength",
                table: "TruckRequirements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InnerBodyWidth",
                table: "TruckRequirements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentAssignedTruckId",
                table: "TransportationOrders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentTransportationStatus",
                table: "TransportationOrders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransportationOrders_CurrentAssignedTruckId",
                table: "TransportationOrders",
                column: "CurrentAssignedTruckId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationOrders_Trucks_CurrentAssignedTruckId",
                table: "TransportationOrders",
                column: "CurrentAssignedTruckId",
                principalTable: "Trucks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationOrders_Trucks_CurrentAssignedTruckId",
                table: "TransportationOrders");

            migrationBuilder.DropIndex(
                name: "IX_TransportationOrders_CurrentAssignedTruckId",
                table: "TransportationOrders");

            migrationBuilder.DropColumn(
                name: "BodyVolume",
                table: "TruckRequirements");

            migrationBuilder.DropColumn(
                name: "InnerBodyHeight",
                table: "TruckRequirements");

            migrationBuilder.DropColumn(
                name: "InnerBodyLength",
                table: "TruckRequirements");

            migrationBuilder.DropColumn(
                name: "InnerBodyWidth",
                table: "TruckRequirements");

            migrationBuilder.DropColumn(
                name: "CurrentAssignedTruckId",
                table: "TransportationOrders");

            migrationBuilder.DropColumn(
                name: "CurrentTransportationStatus",
                table: "TransportationOrders");

            migrationBuilder.RenameColumn(
                name: "HasLiftGate",
                table: "Trucks",
                newName: "HasLiftgate");
        }
    }
}
