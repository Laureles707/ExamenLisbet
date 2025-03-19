using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Microservices.Compras.Migrations
{
    /// <inheritdoc />
    public partial class addMovimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PrecioVenta",
                table: "Productos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Costo",
                table: "Productos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<decimal>(
                name: "Costo",
                table: "CompraDets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "MovimientoCabs",
                columns: table => new
                {
                    Id_MovimientoCab = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fec_registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_TipoMovimiento = table.Column<int>(type: "int", nullable: false),
                    Id_DocumentoOrigen = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientoCabs", x => x.Id_MovimientoCab);
                });

            migrationBuilder.CreateTable(
                name: "MovimientoDets",
                columns: table => new
                {
                    Id_MovimientoDet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_movimientocab = table.Column<int>(type: "int", nullable: false),
                    Id_Producto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientoDets", x => x.Id_MovimientoDet);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientoCabs");

            migrationBuilder.DropTable(
                name: "MovimientoDets");

            migrationBuilder.DropColumn(
                name: "Costo",
                table: "CompraDets");

            migrationBuilder.AlterColumn<double>(
                name: "PrecioVenta",
                table: "Productos",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Costo",
                table: "Productos",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
