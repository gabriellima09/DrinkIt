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
    }
}