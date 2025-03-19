﻿using System.ComponentModel.DataAnnotations;

namespace Examen.Microservices.Compras.Models.Dtos
{
    public class VentaDto
    {

        public int Id_VentaCab { get; set; }
        public DateTime FecRegistro { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
    }
}
