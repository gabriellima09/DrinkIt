using System;
using System.Collections.Generic;

namespace DrinkIt.WebApp.Models
{
    public class Pedido : EntidadeDominio
    {
        public List<Bebida> Bebidas { get; set; }
        public CartaoCredito Cartao1 { get; set; }
        public CartaoCredito Cartao2 { get; set; }
        public Cliente Cliente { get; set; }
        public Endereco EnderecoEntrega { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
        public string Status { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Frete { get; set; }

        public int IdEnderecoEntrega { get; set; }
        public int IdCartao1 { get; set; }
        public int IdCartao2 { get; set; }
        public int IdCupom { get; set; }
        public int IdCliente { get; set; }

        public bool Pagar2Cartoes { get; set; }

        public Cupom Cupom { get; set; }

        public Endereco NovoEndereco { get; set; }
        public CartaoCredito NovoCartao { get; set; }
    }
}