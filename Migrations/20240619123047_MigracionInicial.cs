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
                name: "Contribuyentes",
                columns: table => new
                {
                    IdContribuyente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroDocumentoContribuyente = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    ApellidoNombreContribuyente = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DomicilioCalleContribuyente = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DomicilioNumeroContribuyente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefonoContribuyente = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SexoContribuyente = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    FechaNacimientoContribuyente = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contribuyentes", x => x.IdContribuyente);
                });

            migrationBuilder.CreateTable(
                name: "TributosMunicipales",
                columns: table => new
                {
                    IdTributo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTributo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TributosMunicipales", x => x.IdTributo);
                });

            migrationBuilder.CreateTable(
                name: "PadronContribuyentes",
                columns: table => new
                {
                    IdContribuyente = table.Column<int>(type: "int", nullable: false),
                    IdTributoMunicipal = table.Column<int>(type: "int", nullable: false),
                    NumeroPadron = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PadronContribuyentes", x => new { x.IdContribuyente, x.IdTributoMunicipal });
                    table.UniqueConstraint("AK_PadronContribuyentes_NumeroPadron", x => x.NumeroPadron);
                    table.ForeignKey(
                        name: "FK_PadronContribuyentes_Contribuyentes_IdContribuyente",
                        column: x => x.IdContribuyente,
                        principalTable: "Contribuyentes",
                        principalColumn: "IdContribuyente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PadronContribuyentes_TributosMunicipales_IdTributoMunicipal",
                        column: x => x.IdTributoMunicipal,
                        principalTable: "TributosMunicipales",
                        principalColumn: "IdTributo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PadronBoletas",
                columns: table => new
                {
                    IdBoleta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroPadron = table.Column<int>(type: "int", nullable: false),
                    Periodo = table.Column<int>(type: "int", nullable: false),
                    Importe = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    Vencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pagado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PadronBoletas", x => x.IdBoleta);
                    table.ForeignKey(
                        name: "FK_PadronBoletas_PadronContribuyentes_NumeroPadron",
                        column: x => x.NumeroPadron,
                        principalTable: "PadronContribuyentes",
                        principalColumn: "NumeroPadron",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contribuyentes_NumeroDocumentoContribuyente",
                table: "Contribuyentes",
                column: "NumeroDocumentoContribuyente",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PadronBoletas_NumeroPadron",
                table: "PadronBoletas",
                column: "NumeroPadron");

            migrationBuilder.CreateIndex(
                name: "IX_PadronContribuyentes_IdTributoMunicipal",
                table: "PadronContribuyentes",
                column: "IdTributoMunicipal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PadronBoletas");

            migrationBuilder.DropTable(
                name: "PadronContribuyentes");

            migrationBuilder.DropTable(
                name: "Contribuyentes");

            migrationBuilder.DropTable(
                name: "TributosMunicipales");
        }
    }
}
