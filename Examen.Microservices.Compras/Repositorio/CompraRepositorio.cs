using Examen.Microservices.Compras.Data;
using Examen.Microservices.Compras.Models;
using Examen.Microservices.Compras.Models.Dtos;
using Examen.Microservices.Compras.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;

namespace Examen.Microservices.Compras.Repositorio
{
    public class CompraRepositorio : ICompraRepositorio
    {
        private readonly ApplicationDBContext _bd;

        public CompraRepositorio(ApplicationDBContext bd)
        {
            _bd = bd;
        }

        public async Task CrearCompraAsync(CompraCab compraCab, List<CompraDet> compraDet)
        {
            await _bd.CompraCabs.AddAsync(compraCab);
            await _bd.SaveChangesAsync();

            foreach (var detalle in compraDet)
            {
                detalle.Id_CompraCab = compraCab.Id_CompraCab;
                await _bd.CompraDets.AddAsync(detalle);
            }

            await _bd.SaveChangesAsync();
        }



        public async Task<List<CompraCab>> GetComprasAsync()
        {
           return await _bd.CompraCabs.OrderByDescending( c => c.Id_CompraCab).ToListAsync();
            // await _bd.CompraCabs.Include(c => c.Detalles).ToListAsync();
        }

    }
}
