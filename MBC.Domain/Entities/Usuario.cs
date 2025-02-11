﻿namespace MBC.Domain.Entities;
public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public DateTime DataCadastro { get; set; }
    public List<Transacao> Transacoes { get; set; }
}

