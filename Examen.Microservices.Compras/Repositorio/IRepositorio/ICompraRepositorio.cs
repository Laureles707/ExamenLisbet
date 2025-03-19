using Examen.Microservices.Compras.Models;

namespace Examen.Microservices.Compras.Repositorio.IRepositorio
{
    public interface ICompraRepositorio
    {
        Task<List<CompraCab>> GetComprasAsync();
        Task CrearCompraAsync(CompraCab compraCab, List<CompraDet> compraDet);
    }
}
