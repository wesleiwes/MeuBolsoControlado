using MBC.Domain.Entities;
using MBC.Domain.Entities.Base;
using MBC.Domain.Enums;
using MBC.Domain.RepositoriesInterface;

namespace MBC.Infrastructure.Data.Repositories;
public class TransacaoRepository : ITransacaoRepository
{
    public void Adicione(IEntidade entidade)
    {
        throw new NotImplementedException();
    }

    public void Atualize(IEntidade entidade)
    {
        throw new NotImplementedException();
    }

    public IEntidade BusquePorId(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Transacao> ListePorCategoria(CategoriaTransacao categoria, int usuarioId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Transacao> ListePorPeriodo(DateTime inicio, DateTime fim, int usuarioId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Transacao> ListePorUsuario(int usuarioId)
    {
        throw new NotImplementedException();
    }

    public decimal ObtenhaTotalPorPeriodo(DateTime inicio, DateTime fim, int usuarioId)
    {
        throw new NotImplementedException();
    }

    public void Remova(int id)
    {
        throw new NotImplementedException();
    }
}