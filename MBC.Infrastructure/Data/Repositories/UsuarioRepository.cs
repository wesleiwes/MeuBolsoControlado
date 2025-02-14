using MBC.Domain.Entities;
using MBC.Domain.Entities.Base;
using MBC.Domain.RepositoriesInterface;

namespace MBC.Infrastructure.Data.Repositories;
public class UsuarioRepository : IUsuarioRepository
{
    public void Adicionar(IEntidade entidade)
    {
        throw new NotImplementedException();
    }

    public void Atualizar(IEntidade entidade)
    {
        throw new NotImplementedException();
    }

    public Usuario BuscarPorEmail(string email)
    {
        throw new NotImplementedException();
    }

    public IEntidade BuscarPorId(int id)
    {
        throw new NotImplementedException();
    }

    public void Remover(IEntidade entidade)
    {
        throw new NotImplementedException();
    }

    public bool VerificarSeEmailExiste(string email)
    {
        throw new NotImplementedException();
    }
}
