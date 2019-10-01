using System;
using System.Collections.Generic;

namespace DrinkIt.WebApp.Models
{
    public class Pedido : EntidadeDominio
    {
        public List<Bebida> Bebidas { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; }
        public decimal ValorTotal { get; set; }

        public int IdEnderecoEntrega { get; set; }
        public int IdCartao1 { get; set; }
        public int IdCartao2 { get; set; }
        public int IdCupomDesconto { get; set; }
        public int IdCupomTroca { get; set; }

        public Cupom CupomDesconto { get; set; }
        public Cupom CupomTroca { get; set; }

        public Endereco NovoEndereco { get; set; }
        public CartaoCredito NovoCartao { get; set; }
    }
}