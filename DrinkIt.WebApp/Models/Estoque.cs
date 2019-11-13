using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkIt.WebApp.Models
{
    public class Estoque
    {
        public int IdBebida { get; set; }
        public string DescBebida { get; set; }
        public int Qtde { get; set; }
        public string Fornecedor { get; set; }
        public DateTime DtEntrada { get; set; }
        public decimal VlrCusto { get; set; }
    }
}