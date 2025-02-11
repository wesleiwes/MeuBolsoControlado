namespace MBC.Domain.Entities;
public class Despesa : Transacao
{
    public bool Pago { get; set; }
    public List<Parcela> Parcelas { get; set; }
}

