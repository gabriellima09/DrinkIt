using System.ComponentModel.DataAnnotations;

namespace DrinkIt.WebApp.Models
{
    public class CartaoCredito : EntidadeDominio
    {
        public int ClienteId { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        public string NomeTitular { get; set; }
        [Required]
        public string Bandeira { get; set; }
        [Required]
        public int CodigoSeguranca { get; set; }
        public bool Preferencial { get; set; }
    }
}