using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen.Microservices.Compras.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompraCabs",
                columns: table => new
                {
                    Id_CompraCab = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FecRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Igv = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraCabs", x => x.Id_CompraCab);
                });

            migrationBuilder.CreateTable(
                name: "CompraDets",
                columns: table => new
                {
                    Id_CompraDet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_CompraCab = table.Column<int>(type: "int", nullable: false),
                    Id_Producto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Sub_Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Igv = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraDets", x => x.Id_CompraDet);
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
                    PrecioVenta = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id_Producto);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompraCabs");

            migrationBuilder.DropTable(
                name: "CompraDets");

            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
