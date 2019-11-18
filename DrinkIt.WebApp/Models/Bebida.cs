using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrinkIt.WebApp.Models
{
    public class Bebida : EntidadeDominio
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public TipoBebida TipoBebida { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public string Volume { get; set; }
        [Required]
        public string Peso { get; set; }
        [Required]
        public string Sabor { get; set; }
        [Required]
        public string Lote { get; set; }
        [Required]
        public DateTime DataFabricacao { get; set; }
        [Required]
        public DateTime DataValidade { get; set; }
        [Required]
        public string Fabricante { get; set; }
        [Required]
        public string Embalagem { get; set; }
        [Required]
        public string CodigoBarras { get; set; }
        [Required]
        public bool Alcoolico { get; set; }
        
        public decimal Teor { get; set; }

        [Required]
        public bool Gaseificada { get; set; }
        [Required]
        public bool ContemGluten { get; set; }
        [Required]
        public List<Ingrediente> Ingredientes { get; set; }
        [Required]
        public string DicaConservacao { get; set; }
        [Required]
        public bool Status { get; set; }

        public string CaminhoImagem { get; set; }

        public decimal ValorVenda { get; set; }
        public decimal MargemLucro { get; set; }

        public int Quantidade { get; set; }
        public int IdCategoria { get; set; }
    }
}