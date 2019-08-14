using System;
using System.Collections.Generic;

namespace DrinkIt.WebApp.Models
{
    public class Bebida : EntidadeDominio
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoBebida TipoBebida { get; set; }
        public string Marca { get; set; }
        public decimal Valor { get; set; }
        public string Volume { get; set; }
        public string Peso { get; set; }
        public string Sabor { get; set; }
        public string Lote { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public string Fabricante { get; set; }
        public string Embalagem { get; set; }
        public string CodigoBarras { get; set; }
        public bool Alcoolico { get; set; }
        public string Teor { get; set; }
        public bool Gaseificada { get; set; }
        public bool ContemGluten { get; set; }
        public List<Ingrediente> Ingredientes { get; set; }
        public string DicaConservacao { get; set; }
        public string Status { get; set; }
        public string CaminhoImagem { get; set; }
    }
}