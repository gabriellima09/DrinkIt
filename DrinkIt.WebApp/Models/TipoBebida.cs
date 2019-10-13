namespace DrinkIt.WebApp.Models
{
    public class TipoBebida : EntidadeDominio
    {
        public string Descricao { get; set; }
        public int IdGrupoPrecificacao { get; set; }
        public string DescGrupoPrecificacao { get; set; }
    }
}