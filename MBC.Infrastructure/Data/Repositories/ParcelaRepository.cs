using MBC.Domain.Entities;
using MBC.Domain.Entities.Base;
using MBC.Domain.RepositoriesInterface;
using MBC.Infrastructure.Mappers;

namespace MBC.Infrastructure.Data.Repositories;
public class ParcelaRepository : IParcelaRepository
{
    private readonly ParcelaMapper _parcelaMapper = new();

    public void Adicione(Parcela parcela) => 
        _parcelaMapper.Adicionar(parcela);    

    public void Atualize(Parcela parcela) => 
        _parcelaMapper.Atualizar(parcela);

    public Parcela BusquePorId(int id) =>
        _parcelaMapper.BuscarPorId(id);

    public Parcela BusqueProximaParcelaNaoPaga(int transacaoId) => 
        _parcelaMapper.BuscarProximaParcelaNaoPaga(transacaoId);

    public decimal CalculeValorRestante(int transacaoId) =>
        _parcelaMapper.CalcularValorRestante(transacaoId);

    public IEnumerable<Parcela> ListePorTransacao(int transacaoId) =>
        _parcelaMapper.ListarPorTransacao(transacaoId);

    public void Remova(int id) =>
        _parcelaMapper.Remover(id);
}