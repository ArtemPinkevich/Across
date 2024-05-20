using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class orderAssingedDriversTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferChangeHistoryRecords_AspNetUsers_AssignedDriverId",
                table: "TransferChangeHistoryRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferChangeHistoryRecords_AspNetUsers_AssignedLawyerId",
                table: "TransferChangeHistoryRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferChangeHistoryRecords_AspNetUsers_AssignedManagerId",
                table: "TransferChangeHistoryRecords");

            migrationBuilder.DropIndex(
                name: "IX_TransferChangeHistoryRecords_AssignedDriverId",
                table: "TransferChangeHistoryRecords");

            migrationBuilder.DropIndex(
                name: "IX_TransferChangeHistoryRecords_AssignedLawyerId",
                table: "TransferChangeHistoryRecords");

            migrationBuilder.DropIndex(
                name: "IX_TransferChangeHistoryRecords_AssignedManagerId",
                table: "TransferChangeHistoryRecords");

            migrationBuilder.DropColumn(
                name: "AssignedDriverId",
                table: "TransferChangeHistoryRecords");

            migrationBuilder.DropColumn(
                name: "AssignedLawyerId",
                table: "TransferChangeHistoryRecords");

            migrationBuilder.DropColumn(
                name: "AssignedManagerId",
                table: "TransferChangeHistoryRecords");

            migrationBuilder.CreateTable(
                name: "TransferAssignedDriverRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TransportationOrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    ChangeDatetime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferAssignedDriverRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferAssignedDriverRecords_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferAssignedDriverRecords_TransportationOrders_TransportationOrderId",
                        column: x => x.TransportationOrderId,
                        principalTable: "TransportationOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferAssignedDriverRecords_TransportationOrderId",
                table: "TransferAssignedDriverRecords",
                column: "TransportationOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferAssignedDriverRecords_UserId",
                table: "TransferAssignedDriverRecords",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferAssignedDriverRecords");

            migrationBuilder.AddColumn<string>(
                name: "AssignedDriverId",
                table: "TransferChangeHistoryRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignedLawyerId",
                table: "TransferChangeHistoryRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignedManagerId",
                table: "TransferChangeHistoryRecords",
                type: "TEXT",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_TransferChangeHistoryRecords_AspNetUsers_AssignedDriverId",
                table: "TransferChangeHistoryRecords",
                column: "AssignedDriverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferChangeHistoryRecords_AspNetUsers_AssignedLawyerId",
                table: "TransferChangeHistoryRecords",
                column: "AssignedLawyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferChangeHistoryRecords_AspNetUsers_AssignedManagerId",
                table: "TransferChangeHistoryRecords",
                column: "AssignedManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
