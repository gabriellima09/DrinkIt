using Xunit;

namespace DrinkIt.WebApp.Selenium.FluxoGraficos
{
    public class TesteFluxoGrafico
    {
        [Fact]
        public void FluxoGrafico()
        {
            TelasFluxoGrafico tela = new TelasFluxoGrafico(Browser.Chrome);

            tela.LoginAdmin();
            tela.Esperar(3);
            tela.InserirDatas();
            tela.Esperar(30);  
            tela.Fechar();
        }
    }
}