using Xunit;

namespace DrinkIt.WebApp.Selenium.FluxoCompleto
{
    public class TesteFluxoCompleto
    {
        [Fact]
        public void FluxoCompleto()
        {
            TelasFluxoCompleto tela = new TelasFluxoCompleto(Browser.Chrome);

            tela.LoginAdmin();
            tela.Esperar(5);
            tela.NavegarParaTelaCadastroBebidas();
            tela.CadastroBebidas();
            tela.Esperar(5);
            tela.Logout();
            tela.NavegarParaTelaCadastroCliente();
            tela.CadastroClientes();
            tela.Esperar(5);
            tela.LoginCliente();
            tela.Esperar(5);
            tela.TelaInicial();
            tela.Esperar(5);
            tela.SelecionaBebida();
            tela.Esperar(5);
            tela.IrParaCheckout();
            //tela.Checkout();
            //tela.SolicitarTroca();
            //tela.Logout();
            //tela.TratarSolicitacaoTroca();
        }
    }
}