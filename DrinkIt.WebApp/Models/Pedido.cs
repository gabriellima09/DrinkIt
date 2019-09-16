using System;
using System.Collections.Generic;

namespace DrinkIt.WebApp.Models
{
    public class Pedido : EntidadeDominio
    {
        public List<Bebida> Bebidas { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Data { get; set; }
        public Endereco Endereco { get; set; }
        public CartaoCredito Cartao1 { get; set; }
        public CartaoCredito Cartao2 { get; set; }
        public Cupom CupomDesconto { get; set; }
        public Cupom CupomTroca { get; set; }
        public string Status { get; set; }
    }
}