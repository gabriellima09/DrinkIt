using System.Collections.Generic;

namespace DrinkIt.WebApp.Models
{
    public class Dashboard
    {
        public string Inicio { get; set; }
        public string Fim { get; set; }

        public string Top1 { get; set; }
        public string Top2 { get; set; }
        public string Top3 { get; set; }

        public int Qtd1 { get; set; }
        public int Qtd2 { get; set; }
        public int Qtd3 { get; set; }

        public List<int> Qtd { get; set; }
        public List<string> Top { get; set; }

        public List<string> DataAlcoolico { get; set; }
        public List<int> ValoresAlcoolicos { get; set; }

        public List<string> DataNaoAlcoolico { get; set; }
        public List<int> ValoresNaoAlcoolicos { get; set; }
    }
}