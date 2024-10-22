using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiMun.Migrations
{
    public partial class MigracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreServicio = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sintetico = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.IdServicio);
                });

            migrationBuilder.CreateTable(
                name: "ServicioClientes",
                columns: table => new
                {
                    IdContribuyente = table.Column<int>(type: "int", nullable: false),
                    IdServicio = table.Column<int>(type: "int", nullable: false),
                    NumeroServicio = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicioClientes", x => new { x.IdContribuyente, x.IdServicio });
                    table.UniqueConstraint("AK_ServicioClientes_NumeroServicio", x => x.NumeroServicio);
                    table.ForeignKey(
                        name: "FK_ServicioClientes_Contribuyentes_IdContribuyente",
                        column: x => x.IdContribuyente,
                        principalTable: "Contribuyentes",
                        principalColumn: "IdContribuyente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicioClientes_Servicios_IdServicio",
                        column: x => x.IdServicio,
                        principalTable: "Servicios",
                        principalColumn: "IdServicio",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateTable(
                name: "ServicioBoletas",
                columns: table => new
                {
                    IdBoletaServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroServicio = table.Column<int>(type: "int", nullable: false),
                    Periodo = table.Column<int>(type: "int", nullable: false),
                    Importe = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Vencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pagado = table.Column<bool>(type: "bit", nullable: false),
                    Vencimiento2 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Importe2 = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicioBoletas", x => x.IdBoletaServicio);
                    table.ForeignKey(
                        name: "FK_ServicioBoletas_ServicioClientes_NumeroServicio",
                        column: x => x.NumeroServicio,
                        principalTable: "ServicioClientes",
                        principalColumn: "NumeroServicio",
                        onDelete: ReferentialAction.Cascade);
                });



            migrationBuilder.CreateIndex(
                name: "IX_ServicioBoletas_NumeroServicio",
                table: "ServicioBoletas",
                column: "NumeroServicio");

            migrationBuilder.CreateIndex(
                name: "IX_ServicioClientes_IdServicio",
                table: "ServicioClientes",
                column: "IdServicio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServicioBoletas");

            migrationBuilder.DropTable(
                name: "ServicioClientes");

            migrationBuilder.DropTable(
                name: "Servicios");
        }
    }
}
