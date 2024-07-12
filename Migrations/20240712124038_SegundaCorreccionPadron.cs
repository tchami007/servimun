using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiMun.Migrations
{
    public partial class SegundaCorreccionPadron : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Importe2",
                table: "PadronBoletas",
                type: "decimal(12,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "Vencimiento2",
                table: "PadronBoletas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Importe2",
                table: "PadronBoletas");

            migrationBuilder.DropColumn(
                name: "Vencimiento2",
                table: "PadronBoletas");
        }
    }
}
