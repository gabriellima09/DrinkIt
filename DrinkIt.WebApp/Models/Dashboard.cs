using System.Collections.Generic;

namespace DrinkIt.WebApp.Models
{
    public class Dashboard
    {
        public string Inicio { get; set; }
        public string Fim { get; set; }

        public List<string> DataAlcoolico { get; set; }
        public List<int> ValoresAlcoolicos { get; set; }

        public List<string> DataNaoAlcoolico { get; set; }
        public List<int> ValoresNaoAlcoolicos { get; set; }
    }
}