using MBC.Domain.Entities;
using MBC.Domain.Entities.Base;
using MBC.Domain.Enums;

namespace MBC.Domain.RepositoriesInterface;
public interface ITransacaoRepository : IBaseRepository<Transacao>
{
    IEnumerable<Transacao> ListePorUsuario(int usuarioId);
    IEnumerable<Transacao> ListePorPeriodo(DateTime inicio, DateTime fim, int usuarioId);
    IEnumerable<Transacao> ListePorCategoria(EnumeradorDeCategoriaDeTransacao categoria, int usuarioId);
    decimal ObtenhaTotalPorPeriodo(DateTime inicio, DateTime fim, int usuarioId);
}