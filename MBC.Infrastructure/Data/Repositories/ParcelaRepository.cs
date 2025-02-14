using MBC.Domain.Entities;
using MBC.Domain.Entities.Base;
using MBC.Domain.RepositoriesInterface;

namespace MBC.Infrastructure.Data.Repositories;
public class ParcelaRepository : IParcelaRepository
{
    public void Adicionar(IEntidade entidade)
    {
        throw new NotImplementedException();
    }

    public void Atualizar(IEntidade entidade)
    {
        throw new NotImplementedException();
    }

    public IEntidade BuscarPorId(int id)
    {
        throw new NotImplementedException();
    }

    public Parcela BuscarProximaParcelaNaoPaga(int transacaoId)
    {
        throw new NotImplementedException();
    }

    public decimal CalcularValorRestante(int transacaoId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Parcela> ListarPorTransacao(int transacaoId)
    {
        throw new NotImplementedException();
    }

    public void Remover(IEntidade entidade)
    {
        throw new NotImplementedException();
    }
}
