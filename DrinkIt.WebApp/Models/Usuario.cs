using System.ComponentModel.DataAnnotations;

namespace DrinkIt.WebApp.Models
{
    public class Usuario : EntidadeDominio
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Senha { get; set; }
        public Carrinho Carrinho { get; set; } = new Carrinho();
    }
}