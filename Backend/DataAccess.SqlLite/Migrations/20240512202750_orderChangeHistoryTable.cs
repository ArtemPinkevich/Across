using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class orderChangeHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransportationStatus",
                table: "TransportationOrders");

            migrationBuilder.CreateTable(
                name: "TransferChangeHistoryRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransportationOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssignedDriverId = table.Column<string>(type: "TEXT", nullable: true),
                    AssignedManagerId = table.Column<string>(type: "TEXT", nullable: true),
                    AssignedLawyerId = table.Column<string>(type: "TEXT", nullable: true),
                    ChangeDatetime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TransportationStatus = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferChangeHistoryRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferChangeHistoryRecords_AspNetUsers_AssignedDriverId",
                        column: x => x.AssignedDriverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferChangeHistoryRecords_AspNetUsers_AssignedLawyerId",
                        column: x => x.AssignedLawyerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferChangeHistoryRecords_AspNetUsers_AssignedManagerId",
                        column: x => x.AssignedManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferChangeHistoryRecords_TransportationOrders_TransportationOrderId",
                        column: x => x.TransportationOrderId,
                        principalTable: "TransportationOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferChangeHistoryRecords_AssignedDriverId",
                table: "TransferChangeHistoryRecords",
                column: "AssignedDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferChangeHistoryRecords_AssignedLawyerId",
                table: "TransferChangeHistoryRecords",
                column: "AssignedLawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferChangeHistoryRecords_AssignedManagerId",
                table: "TransferChangeHistoryRecords",
                column: "AssignedManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferChangeHistoryRecords_TransportationOrderId",
                table: "TransferChangeHistoryRecords",
                column: "TransportationOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferChangeHistoryRecords");

            migrationBuilder.AddColumn<int>(
                name: "TransportationStatus",
                table: "TransportationOrders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
