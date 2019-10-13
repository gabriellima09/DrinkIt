using System;

namespace DrinkIt.WebApp.Models
{
    public class Cupom : EntidadeDominio
    {
        public string Descricao { get; set; }
        public bool Status { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataExpiracao { get; set; }
        public int IdTipo { get; set; }
        public decimal Valor { get; set; }
    }
}