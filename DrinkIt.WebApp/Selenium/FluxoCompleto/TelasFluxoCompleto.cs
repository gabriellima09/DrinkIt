using DrinkIt.WebApp.Models;
using OpenQA.Selenium;
using System;
using System.Configuration;

namespace DrinkIt.WebApp.Selenium.FluxoCompleto
{
    public class TelasFluxoCompleto
    {
        private readonly Browser _browser;
        private readonly IWebDriver _driver;

        public TelasFluxoCompleto(Browser browser)
        {
            _browser = browser;

            string caminhoDriver = ConfigurationManager.AppSettings["Selenium"];

            _driver = WebDriveFactory.CreateWebDriver(_browser, caminhoDriver);
        }

        public void CadastroBebidas()
        {
            TelaCadastroBebida tela = new TelaCadastroBebida(Browser.Chrome);

            tela.CarregarPagina();
            tela.PreencherDadosBebida(GetBebida());
            tela.ProcessarCadastro();
        }

        public void CadastroClientes()
        {
            TelaCadastroCliente tela = new TelaCadastroCliente(Browser.Chrome);

            tela.CarregarPagina();
            tela.PreencherDadosCliente(GetCliente());
            tela.ProcessarCadastro();
        }

        public void LoginAdmin()
        {
            throw new NotImplementedException();
        }

        public void NavegarParaTelaCadastroBebidas()
        {
            throw new NotImplementedException();
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void LoginCliente()
        {
            throw new NotImplementedException();
        }

        public void TelaInicial()
        {
            throw new NotImplementedException();
        }

        public void SelecionaBebida()
        {
            throw new NotImplementedException();
        }

        public void MostrarCarrinho()
        {
            throw new NotImplementedException();
        }

        public void Checkout()
        {
            throw new NotImplementedException();
        }

        public void MostrarPedido()
        {
            throw new NotImplementedException();
        }

        public void SolicitarTroca()
        {
            throw new NotImplementedException();
        }

        public void TratarSolicitacaoTroca()
        {
            throw new NotImplementedException();
        }

        public Cliente GetCliente()
        {
            return new Cliente
            {
                Id = 1,
                Cpf = "123.456.789-00",
                DataNascimento = DateTime.Now,
                Email = "teste@teste.com",
                Endereco = new Endereco
                {
                    Logradouro = "Rua abc",
                    Bairro = "Centro",
                    CEP = "01234-567",
                    Cidade = "Mogi das Cruzes",
                    Complemento = string.Empty,
                    Estado = "SP",
                    Cobranca = true,
                    Descricao = "Minha Casa",
                    Entrega = true,
                    Numero = "123",
                },
                Cartao = new CartaoCredito
                {
                    Bandeira = "Mastercard",
                    CodigoSeguranca = 123,
                    NomeTitular = "Gabriel Lima Gomes",
                    Numero = "123456789000"
                },
                Genero = "Masculino",
                Telefone = "(11) 91234-5678",
                Login = "teste",
                Senha = "teste",
                Nome = "Usuário Teste"
            };
        }

        public Bebida GetBebida()
        {
            return new Bebida
            {
                Id = 1,
                Nome = "Bebida Teste",
                Descricao = "Descricao Teste",
                Marca = "Marca Teste",
                Valor = 9.99M,
                Volume = "2 Litros",
                Peso = "500 gramas",
                Sabor = "Sabor teste",
                Lote = "Lote Teste",
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now,
                Fabricante = "Fabricante Teste",
                Embalagem = "Lata",
                CodigoBarras = "0123456789",
                Alcoolico = true,
                Teor = 10,
                Gaseificada = false,
                ContemGluten = false,
                DicaConservacao = "Dica Teste",
                Status = true
            };
        }
    }
}