using System.ComponentModel.DataAnnotations;

namespace DrinkIt.WebApp.Models
{
    public class Endereco : EntidadeDominio
    {
        public int ClienteId { get; set; }

        public string Descricao { get; set; }
        [Required]
        public string Logradouro { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string CEP { get; set; }
        public string Complemento { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Estado { get; set; }
        public bool Cobranca { get; set; }
        public bool Entrega { get; set; }
        public int Remover { get; set; }
    }
}