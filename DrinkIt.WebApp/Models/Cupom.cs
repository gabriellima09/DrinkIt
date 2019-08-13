using System;

namespace DrinkIt.WebApp.Models
{
    public class Cupom : EntidadeDominio
    {
        public string Descricao { get; set; }
        public string Status { get; set; }
        public DateTime DataEmissao { get; set; }        
    }
}