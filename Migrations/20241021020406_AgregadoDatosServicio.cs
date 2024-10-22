using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiMun.Migrations
{
    public partial class AgregadoDatosServicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumeroCliente",
                table: "ServicioClientes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumeroTelefono",
                table: "ServicioClientes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroCliente",
                table: "ServicioClientes");

            migrationBuilder.DropColumn(
                name: "NumeroTelefono",
                table: "ServicioClientes");
        }
    }
}
