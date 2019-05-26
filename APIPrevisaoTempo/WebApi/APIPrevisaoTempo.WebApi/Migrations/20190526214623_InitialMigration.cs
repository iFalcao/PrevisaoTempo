using Microsoft.EntityFrameworkCore.Migrations;

namespace APIPrevisaoTempo.WebApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CustomCode = table.Column<string>(maxLength: 30, nullable: false),
                    Latitude = table.Column<double>(type: "decimal(9,6)", nullable: true),
                    Longitude = table.Column<double>(type: "decimal(9,6)", nullable: true),
                    Country = table.Column<string>(maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CustomCode",
                table: "Cities",
                column: "CustomCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
