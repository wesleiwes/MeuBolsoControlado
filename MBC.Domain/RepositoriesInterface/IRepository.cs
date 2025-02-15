using MBC.Domain.Entities.Base;

namespace MBC.Domain.RepositoriesInterface;

public interface IRepository<T> where T : IEntidade
{
    void Adicione(T entidade);
    void Atualize(T entidade);
    void Remova(int id);
    T BusquePorId(int id);
}