using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class changedTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferChangeHistoryRecords_Transportations_TransportationId",
                table: "TransferChangeHistoryRecords");

            migrationBuilder.DropTable(
                name: "TransferAssignedDriverRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransferChangeHistoryRecords",
                table: "TransferChangeHistoryRecords");

            migrationBuilder.RenameTable(
                name: "TransferChangeHistoryRecords",
                newName: "TransportationStatusRecords");

            migrationBuilder.RenameIndex(
                name: "IX_TransferChangeHistoryRecords_TransportationId",
                table: "TransportationStatusRecords",
                newName: "IX_TransportationStatusRecords_TransportationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransportationStatusRecords",
                table: "TransportationStatusRecords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationStatusRecords_Transportations_TransportationId",
                table: "TransportationStatusRecords",
                column: "TransportationId",
                principalTable: "Transportations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationStatusRecords_Transportations_TransportationId",
                table: "TransportationStatusRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransportationStatusRecords",
                table: "TransportationStatusRecords");

            migrationBuilder.RenameTable(
                name: "TransportationStatusRecords",
                newName: "TransferChangeHistoryRecords");

            migrationBuilder.RenameIndex(
                name: "IX_TransportationStatusRecords_TransportationId",
                table: "TransferChangeHistoryRecords",
                newName: "IX_TransferChangeHistoryRecords_TransportationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransferChangeHistoryRecords",
                table: "TransferChangeHistoryRecords",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TransferAssignedDriverRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransportationOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    TruckId = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeDatetime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferAssignedDriverRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferAssignedDriverRecords_TransportationOrders_TransportationOrderId",
                        column: x => x.TransportationOrderId,
                        principalTable: "TransportationOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferAssignedDriverRecords_Trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "Trucks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferAssignedDriverRecords_TransportationOrderId",
                table: "TransferAssignedDriverRecords",
                column: "TransportationOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferAssignedDriverRecords_TruckId",
                table: "TransferAssignedDriverRecords",
                column: "TruckId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferChangeHistoryRecords_Transportations_TransportationId",
                table: "TransferChangeHistoryRecords",
                column: "TransportationId",
                principalTable: "Transportations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
