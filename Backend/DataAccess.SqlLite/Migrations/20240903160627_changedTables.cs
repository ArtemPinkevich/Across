using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class changedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferChangeHistoryRecords_TransportationOrders_TransportationOrderId",
                table: "TransferChangeHistoryRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportationOrders_Trucks_CurrentAssignedTruckId",
                table: "TransportationOrders");

            migrationBuilder.DropIndex(
                name: "IX_TransportationOrders_CurrentAssignedTruckId",
                table: "TransportationOrders");

            migrationBuilder.DropColumn(
                name: "CurrentAssignedTruckId",
                table: "TransportationOrders");

            migrationBuilder.RenameColumn(
                name: "CurrentTransportationOrderStatus",
                table: "TransportationOrders",
                newName: "TransportationOrderStatus");

            migrationBuilder.RenameColumn(
                name: "TransportationOrderStatus",
                table: "TransferChangeHistoryRecords",
                newName: "TransportationStatus");

            migrationBuilder.RenameColumn(
                name: "TransportationOrderId",
                table: "TransferChangeHistoryRecords",
                newName: "TransportationId");

            migrationBuilder.RenameIndex(
                name: "IX_TransferChangeHistoryRecords_TransportationOrderId",
                table: "TransferChangeHistoryRecords",
                newName: "IX_TransferChangeHistoryRecords_TransportationId");

            migrationBuilder.AddColumn<int>(
                name: "TruckId",
                table: "Transportations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_TruckId",
                table: "Transportations",
                column: "TruckId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferChangeHistoryRecords_Transportations_TransportationId",
                table: "TransferChangeHistoryRecords",
                column: "TransportationId",
                principalTable: "Transportations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transportations_Trucks_TruckId",
                table: "Transportations",
                column: "TruckId",
                principalTable: "Trucks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferChangeHistoryRecords_Transportations_TransportationId",
                table: "TransferChangeHistoryRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_Transportations_Trucks_TruckId",
                table: "Transportations");

            migrationBuilder.DropIndex(
                name: "IX_Transportations_TruckId",
                table: "Transportations");

            migrationBuilder.DropColumn(
                name: "TruckId",
                table: "Transportations");

            migrationBuilder.RenameColumn(
                name: "TransportationOrderStatus",
                table: "TransportationOrders",
                newName: "CurrentTransportationOrderStatus");

            migrationBuilder.RenameColumn(
                name: "TransportationStatus",
                table: "TransferChangeHistoryRecords",
                newName: "TransportationOrderStatus");

            migrationBuilder.RenameColumn(
                name: "TransportationId",
                table: "TransferChangeHistoryRecords",
                newName: "TransportationOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_TransferChangeHistoryRecords_TransportationId",
                table: "TransferChangeHistoryRecords",
                newName: "IX_TransferChangeHistoryRecords_TransportationOrderId");

            migrationBuilder.AddColumn<int>(
                name: "CurrentAssignedTruckId",
                table: "TransportationOrders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransportationOrders_CurrentAssignedTruckId",
                table: "TransportationOrders",
                column: "CurrentAssignedTruckId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferChangeHistoryRecords_TransportationOrders_TransportationOrderId",
                table: "TransferChangeHistoryRecords",
                column: "TransportationOrderId",
                principalTable: "TransportationOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationOrders_Trucks_CurrentAssignedTruckId",
                table: "TransportationOrders",
                column: "CurrentAssignedTruckId",
                principalTable: "Trucks",
                principalColumn: "Id");
        }
    }
}
