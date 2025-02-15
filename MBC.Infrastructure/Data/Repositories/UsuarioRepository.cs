using MBC.Domain.Entities;
using MBC.Domain.Entities.Base;
using MBC.Domain.RepositoriesInterface;

namespace MBC.Infrastructure.Data.Repositories;
public class UsuarioRepository : IUsuarioRepository
{
    public void Adicione(IEntidade entidade)
    {
        throw new NotImplementedException();
    }

    public void Atualize(IEntidade entidade)
    {
        throw new NotImplementedException();
    }

    public Usuario BusquePorEmail(string email)
    {
        throw new NotImplementedException();
    }

    public IEntidade BusquePorId(int id)
    {
        throw new NotImplementedException();
    }

    public void Remova(int id)
    {
        throw new NotImplementedException();
    }

    public bool VerifiqueSeEmailExiste(string email)
    {
        throw new NotImplementedException();
    }
}