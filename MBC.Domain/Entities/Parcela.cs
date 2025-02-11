namespace MBC.Domain.Entities;
public class Parcela
{
    public int Id { get; set; }
    public int Numero { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public bool Pago { get; set; }
    public int DespesaId { get; set; }
    public Despesa Despesa { get; set; }
}

