﻿using MBC.Domain.Entities.Base;
using MBC.Domain.Enums;

namespace MBC.Domain.Entities;
public class Transacao : IEntidade
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public EnumeradorDeCategoriaDeTransacao Categoria { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}