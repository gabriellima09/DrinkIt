using DrinkIt.WebApp.Dao;
using System;
using System.IO;
using System.Text;
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
            tela.Esperar(3);
            tela.NavegarParaTelaCadastroBebidas();
            tela.CadastroBebidas();
            tela.Esperar(3);
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
            tela.Esperar(3);
            tela.Logout();

            tela.Esperar(3);

            DbContext.ExecuteQuery(EnriquecimentoPedidosGrafico());

            tela.LoginAdmin();
            tela.Esperar(6);
            tela.InserirDatas();
            tela.Esperar(10);

            tela.Fechar();
        }
        

        public string EnriquecimentoPedidosGrafico()
        {
            string arquivo = @"C:\Users\guga-\OneDrive\Documentos\ProjetoLES2019-2\DrinkIt\DrinkIt.WebApp\Database\ENRIQUECIMENTO_BD_GUSTAVO_25-11-2019.sql";
            StringBuilder Sql = new StringBuilder();
            if (File.Exists(arquivo))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(arquivo))
                    {
                        String linha;
                        // Lê linha por linha
                        while ((linha = sr.ReadLine()) != null)
                        {
                            Sql.Append(linha);
                            Sql.Append("\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }
            else
            {
                throw new Exception("O arquivo não foi encontrado!");
            }
            return Sql.ToString();
        }
    }
}