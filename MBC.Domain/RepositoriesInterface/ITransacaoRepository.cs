using MBC.Domain.Entities;
using MBC.Domain.Entities.Base;
using MBC.Domain.Enums;

namespace MBC.Domain.RepositoriesInterface;
public interface ITransacaoRepository : IRepository<IEntidade>
{
    IEnumerable<Transacao> ListarPorUsuario(int usuarioId);
    IEnumerable<Transacao> ListarPorPeriodo(DateTime inicio, DateTime fim, int usuarioId);
    IEnumerable<Transacao> ListarPorCategoria(CategoriaTransacao categoria, int usuarioId);
    decimal ObterTotalPorPeriodo(DateTime inicio, DateTime fim, int usuarioId);

}
