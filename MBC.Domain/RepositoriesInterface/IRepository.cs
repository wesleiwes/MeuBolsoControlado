using MBC.Domain.Entities.Base;

namespace MBC.Domain.RepositoriesInterface;
public interface IRepository<T> where T : IEntidade
{
    void Adicionar(T entidade);
    void Atualizar(T entidade);
    void Remover(T entidade);
    T BuscarPorId(int id);
}
