using System;

namespace DrinkIt.WebApp.Models
{
    public class Status : EntidadeDominio
    {
        public string Descricao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}