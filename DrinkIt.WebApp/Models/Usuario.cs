namespace DrinkIt.WebApp.Models
{
    public class Usuario : EntidadeDominio
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}