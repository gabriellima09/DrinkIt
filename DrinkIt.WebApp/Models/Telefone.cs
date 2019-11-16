using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkIt.WebApp.Models
{
    public class Telefone : EntidadeDominio
    {
        public int IdCliente { get; set; }
        public int IdTipo { get; set; }
        public string DescricaoTipo { get; set; }
        public int DDD { get; set; }
        public string Numero { get; set; }
    }
}