using Microsoft.EntityFrameworkCore.Migrations;

namespace StarWars.Api.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "api_cache",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(nullable: false),
                    ContentType = table.Column<string>(nullable: false),
                    Payload = table.Column<string>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    ExpiresAtUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_cache", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_api_cache_Key",
                table: "api_cache",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "api_cache");
        }
    }
}




