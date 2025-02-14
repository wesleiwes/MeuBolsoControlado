using MBC.Domain.Entities;
using MBC.Domain.Entities.Base;

namespace MBC.Domain.RepositoriesInterface;
public interface IParcelaRepository : IRepository<IEntidade>
{
    IEnumerable<Parcela> ListarPorTransacao(int transacaoId);
    Parcela BuscarProximaParcelaNaoPaga(int transacaoId);
    decimal CalcularValorRestante(int transacaoId);

}
