using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkIt.WebApp.Models
{
	public class SolicitacaoTroca
	{
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int Status { get; set; }
        public int IdCliente { get; set; }
        public string DescCliente { get; set; }
        public int IdPedido { get; set; }
        public DateTime Data { get; set; }
    }
}