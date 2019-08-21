namespace DrinkIt.WebApp.Models
{
    public class Endereco
    {
        public string Descricao { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public bool Cobranca { get; set; }
        public bool Entrega { get; set; }
    }
}