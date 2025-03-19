﻿// <auto-generated />
using System;
using Examen.Microservices.Compras.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Examen.Microservices.Compras.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20250315165000_Migracioncuatro")]
    partial class Migracioncuatro
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Examen.Microservices.Compras.Models.CompraCab", b =>
                {
                    b.Property<int>("Id_CompraCab")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_CompraCab"));

                    b.Property<DateTime>("FecRegistro")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Igv")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id_CompraCab");

                    b.ToTable("CompraCabs");
                });

            modelBuilder.Entity("Examen.Microservices.Compras.Models.CompraDet", b =>
                {
                    b.Property<int>("Id_CompraDet")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_CompraDet"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("Id_CompraCab")
                        .HasColumnType("int");

                    b.Property<int>("Id_Producto")
                        .HasColumnType("int");

                    b.Property<decimal>("Igv")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Sub_Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id_CompraDet");

                    b.ToTable("CompraDets");
                });

            modelBuilder.Entity("Examen.Microservices.Compras.Models.Producto", b =>
                {
                    b.Property<int>("Id_Producto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Producto"));

                    b.Property<double>("Costo")
                        .HasColumnType("float");

                    b.Property<DateTime>("Fec_registro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre_producto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NroLote")
                        .HasColumnType("int");

                    b.Property<double>("PrecioVenta")
                        .HasColumnType("float");

                    b.HasKey("Id_Producto");

                    b.ToTable("Productos");
                });
#pragma warning restore 612, 618
        }
    }
}
