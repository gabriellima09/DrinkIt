namespace DrinkIt.WebApp.Models
{
    public class CartaoCredito : EntidadeDominio
    {
        public int ClienteId { get; set; }
        public string Numero { get; set; }
        public string NomeTitular { get; set; }
        public string Bandeira { get; set; }
        public int CodigoSeguranca { get; set; }
    }
}