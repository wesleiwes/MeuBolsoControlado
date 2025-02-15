using MBC.Domain.Entities;
using MBC.Domain.Entities.Base;

namespace MBC.Domain.RepositoriesInterface;
public interface IUsuarioRepository : IRepository<IEntidade>
{
    Usuario BusquePorEmail(string email);
    bool VerifiqueSeEmailExiste(string email);
}