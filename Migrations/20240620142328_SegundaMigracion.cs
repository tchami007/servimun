using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiMun.Migrations
{
    public partial class SegundaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sintetico",
                table: "TributosMunicipales",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sintetico",
                table: "TributosMunicipales");
        }
    }
}
