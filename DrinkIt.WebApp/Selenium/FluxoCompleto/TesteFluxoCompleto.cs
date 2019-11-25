﻿using Xunit;

namespace DrinkIt.WebApp.Selenium.FluxoCompleto
{
    public class TesteFluxoCompleto
    {
        [Fact]
        public void FluxoCompleto()
        {
            TelasFluxoCompleto tela = new TelasFluxoCompleto(Browser.Chrome);

            tela.LoginAdmin();
            tela.Esperar(3);
            tela.NavegarParaTelaCadastroBebidas();
            tela.CadastroBebidas();
            tela.PreencherEstoque();
            tela.Esperar(3);
            tela.Logout();
            tela.NavegarParaTelaCadastroCliente();
            tela.CadastroClientes();
            tela.Esperar();
            tela.LoginCliente();
            tela.Esperar(3);
            tela.TelaInicial();
            tela.Esperar(3);
            tela.SelecionaBebida();
            tela.Esperar(3);
            tela.IrParaCheckout();
            tela.Checkout();
            tela.Esperar(3);

            tela.Logout();
            tela.LoginAdmin();
            tela.Esperar(3);
            tela.AprovarPedido();
            tela.Esperar(3);
            tela.Logout();
            tela.LoginCliente();
            tela.Esperar(3);
            tela.AbrirPerfilCliente();
            tela.Esperar(3);

            tela.SolicitarTroca();
            tela.Logout();
            tela.LoginAdmin();
            tela.Esperar(3);
            tela.TratarSolicitacaoTroca();
            tela.Esperar(3);

            tela.Logout();
            tela.LoginCliente();
            tela.Esperar(3);
            tela.AbrirPerfilCliente();
            tela.Esperar(3);
            string cupom = tela.AbrirCupons();
            tela.Esperar(3);
            tela.TelaInicial();
            
            tela.Esperar(3);
            tela.SelecionaBebida();
            tela.Esperar(3);
            tela.IrParaCheckout();
            tela.CheckoutComCupom(cupom);
            tela.Esperar(6);

            tela.Fechar();
        }
    }
}