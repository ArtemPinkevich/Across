using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class changedUnloadingColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnLoadingLongitude",
                table: "TransportationInfos",
                newName: "UnloadingLongitude");

            migrationBuilder.RenameColumn(
                name: "UnLoadingLatitude",
                table: "TransportationInfos",
                newName: "UnloadingLatitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnloadingLongitude",
                table: "TransportationInfos",
                newName: "UnLoadingLongitude");

            migrationBuilder.RenameColumn(
                name: "UnloadingLatitude",
                table: "TransportationInfos",
                newName: "UnLoadingLatitude");
        }
    }
}
