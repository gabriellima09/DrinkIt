using System;
using System.Collections.Generic;

namespace DrinkIt.WebApp.Models
{
    public class Carrinho
    {
        public DateTime DataUltimaInsercaoCarrinho { get; set; }
        public List<Bebida> Bebidas { get; set; } = new List<Bebida>();
    }
}