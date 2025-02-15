using MBC.Domain.Entities;
using MBC.Domain.Entities.Base;

namespace MBC.Domain.RepositoriesInterface;
public interface IUsuarioRepository : IBaseRepository<Usuario>
{
    Usuario? BusquePorEmail(string email);
    bool VerifiqueSeEmailExiste(string email);
}