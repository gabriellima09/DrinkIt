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
            tela.Esperar(7);
            tela.InserirDatas();
            tela.Esperar(40);    
            tela.Fechar();
        }
    }
}