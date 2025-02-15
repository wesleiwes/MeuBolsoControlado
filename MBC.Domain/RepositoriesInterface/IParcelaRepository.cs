using MBC.Domain.Entities;
using MBC.Domain.Entities.Base;

namespace MBC.Domain.RepositoriesInterface;
public interface IParcelaRepository : IRepository<Parcela>
{
    IEnumerable<Parcela> ListePorTransacao(int transacaoId);
    Parcela BusqueProximaParcelaNaoPaga(int transacaoId);
    decimal CalculeValorRestante(int transacaoId);

}