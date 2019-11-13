using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkIt.WebApp.Models
{
    public class HistoricoBebida
    {
        public int IdBebida { get; set; }
        public string DescBebida { get; set; }
        public DateTime DtAcao { get; set; }
        public string Categoria { get; set; }
        public string Acao { get; set; }
        public string Motivo { get; set; }
    }
}