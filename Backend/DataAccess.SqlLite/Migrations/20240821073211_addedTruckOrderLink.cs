using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class addedTruckOrderLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferAssignedDriverRecords_AspNetUsers_UserId",
                table: "TransferAssignedDriverRecords");

            migrationBuilder.DropIndex(
                name: "IX_TransferAssignedDriverRecords_UserId",
                table: "TransferAssignedDriverRecords");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TransferAssignedDriverRecords");

            migrationBuilder.AddColumn<int>(
                name: "TruckId",
                table: "TransferAssignedDriverRecords",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TransferAssignedDriverRecords_TruckId",
                table: "TransferAssignedDriverRecords",
                column: "TruckId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferAssignedDriverRecords_Trucks_TruckId",
                table: "TransferAssignedDriverRecords",
                column: "TruckId",
                principalTable: "Trucks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferAssignedDriverRecords_Trucks_TruckId",
                table: "TransferAssignedDriverRecords");

            migrationBuilder.DropIndex(
                name: "IX_TransferAssignedDriverRecords_TruckId",
                table: "TransferAssignedDriverRecords");

            migrationBuilder.DropColumn(
                name: "TruckId",
                table: "TransferAssignedDriverRecords");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TransferAssignedDriverRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransferAssignedDriverRecords_UserId",
                table: "TransferAssignedDriverRecords",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferAssignedDriverRecords_AspNetUsers_UserId",
                table: "TransferAssignedDriverRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
