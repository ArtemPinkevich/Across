using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SqlLite.Migrations
{
    public partial class addedLegalInfoAndCargoPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Cargos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LegalInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShipperId = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Bin = table.Column<string>(type: "TEXT", nullable: true),
                    VatSeria = table.Column<string>(type: "TEXT", nullable: true),
                    BankName = table.Column<string>(type: "TEXT", nullable: true),
                    BankBin = table.Column<string>(type: "TEXT", nullable: true),
                    BankSwiftCode = table.Column<string>(type: "TEXT", nullable: true),
                    AccountNumber = table.Column<string>(type: "TEXT", nullable: true),
                    LegalAddress = table.Column<string>(type: "TEXT", nullable: true),
                    CompanyCeo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalInformations_AspNetUsers_ShipperId",
                        column: x => x.ShipperId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LegalInformations_ShipperId",
                table: "LegalInformations",
                column: "ShipperId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LegalInformations");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cargos");
        }
    }
}
