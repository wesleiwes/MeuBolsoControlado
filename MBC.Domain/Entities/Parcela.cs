using MBC.Domain.Entities.Base;

namespace MBC.Domain.Entities;
public class Parcela : IEntidade
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public bool Pago { get; set; }
    public int TransacaoId { get; set; }
    public Transacao Transacao { get; set; }
}

