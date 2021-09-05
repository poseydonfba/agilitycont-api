using System;

namespace AgilityCont.Api.Models
{
    public class AlterarTransacaoModel
    {
        public Guid Id { get; set; }
        public int IdTipoLancamento { get; set; }
        public Guid IdTipoTransacao { get; set; }
        public string Descricao { get; set; }
        public DateTime DataTransacao { get; set; }
        public double Valor { get; set; }
        public double Desconto { get; set; }
        public Guid IdFormaPagamento { get; set; }
    }
}