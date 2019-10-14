using System.Collections.Generic;

namespace DrinkIt.WebApp.Models
{
    public class Resultado
    {
        public bool Sucesso { get; set; }
        public List<string> MensagensErro { get; set; }
    }
}