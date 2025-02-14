using MBC.Domain.Entities;
using MBC.Domain.Entities.Base;
using MBC.Domain.Enums;
using MBC.Domain.RepositoriesInterface;

namespace MBC.Infrastructure.Data.Repositories;
public class TransacaoRepository : ITransacaoRepository
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

    public IEnumerable<Transacao> ListarPorCategoria(CategoriaTransacao categoria, int usuarioId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Transacao> ListarPorPeriodo(DateTime inicio, DateTime fim, int usuarioId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Transacao> ListarPorUsuario(int usuarioId)
    {
        throw new NotImplementedException();
    }

    public decimal ObterTotalPorPeriodo(DateTime inicio, DateTime fim, int usuarioId)
    {
        throw new NotImplementedException();
    }

    public void Remover(IEntidade entidade)
    {
        throw new NotImplementedException();
    }
}
