using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Microservices.Venta.Migrations
{
    /// <inheritdoc />
    public partial class addVentas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id_Producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_producto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NroLote = table.Column<int>(type: "int", nullable: false),
                    Fec_registro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id_Producto);
                });

            migrationBuilder.CreateTable(
                name: "VentaCabs",
                columns: table => new
                {
                    Id_VentaCab = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FecRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Igv = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaCabs", x => x.Id_VentaCab);
                });

            migrationBuilder.CreateTable(
                name: "VentaDets",
                columns: table => new
                {
                    Id_VentaDet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_VentaCab = table.Column<int>(type: "int", nullable: false),
                    Id_Producto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sub_Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Igv = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentaDets", x => x.Id_VentaDet);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientoCabs");

            migrationBuilder.DropTable(
                name: "MovimientoDets");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "VentaCabs");

            migrationBuilder.DropTable(
                name: "VentaDets");
        }
    }
}
