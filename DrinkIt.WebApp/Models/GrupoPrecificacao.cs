using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DrinkIt.WebApp.Models
{
    public class GrupoPrecificacao : EntidadeDominio
    {
        public string Descricao { get; set; }
        public decimal MargemLucro { get; set; }
    }
}