using DrinkIt.WebApp.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace DrinkIt.WebApp.Selenium.FluxoCompleto
{
    public class TelasFluxoCompleto
    {
        private readonly Browser _browser;
        private IWebDriver _driver;

        public TelasFluxoCompleto(Browser browser)
        {
            _browser = browser;

            string caminhoDriver = ConfigurationManager.AppSettings["Selenium"];

            _driver = WebDriveFactory.CreateWebDriver(_browser, caminhoDriver);
        }

        public void Esperar(double segundos)
        {
            _driver.Wait(segundos);
        }

        public void Esperar()
        {
            _driver.Wait1();
        }

        public void PreencherEstoque()
        {
            Esperar(3);
            _driver.FindElement(By.Id("navTabEstoque")).Click();
            Esperar(3);
            _driver.FindElement(By.ClassName("btnMenuItemEstoque")).Click();
            Esperar(3);
            _driver.FindElement(By.ClassName("btnEntradaItemEstoque")).Click();
            Esperar(3);
            _driver.SetText(By.Id("QtdeEntrada"), "200");
            _driver.SetText(By.Id("FornecedorEntrada"), "Fornecedor Teste");
            _driver.SetText(By.Id("VlrCustoEntrada"), "1,50");
            _driver.SetText(By.Id("DataEntrada"), DateTime.Now.ToString("dd/MM/yyyy"));
            Esperar(3);
            _driver.FindElement(By.Id("btnSalvarEntrada")).Click();
            Esperar(3);
            _driver.FindElement(By.Id("navTabEstoque")).Click();
        }

        public void CadastroBebidas()
        {
            Bebida bebida = GetBebida();

            _driver.SetText(By.Name("Nome"), bebida.Nome.ToString());
            _driver.SetText(By.Name("Descricao"), bebida.Descricao.ToString());
            _driver.SetText(By.Name("Marca"), bebida.Marca.ToString());
            _driver.SetText(By.Name("Valor"), bebida.Valor.ToString());
            _driver.SetText(By.Name("Volume"), bebida.Volume.ToString());
            _driver.SetText(By.Name("Peso"), bebida.Peso.ToString());
            _driver.SetText(By.Name("Sabor"), bebida.Sabor.ToString());
            _driver.SetText(By.Name("Lote"), bebida.Lote.ToString());
            _driver.SetText(By.Name("DataFabricacao"), bebida.DataFabricacao.ToString("dd/MM/yyyy"));
            _driver.SetText(By.Name("DataValidade"), bebida.DataValidade.ToString("dd/MM/yyyy"));
            _driver.SetText(By.Name("Fabricante"), bebida.Fabricante.ToString());
            _driver.SetText(By.Name("Embalagem"), bebida.Embalagem.ToString());
            _driver.SetText(By.Name("CodigoBarras"), bebida.CodigoBarras.ToString());
            
            //_driver.SetText(By.Name("Teor"), bebida.Teor.ToString());
            _driver.SetText(By.Name("DicaConservacao"), bebida.DicaConservacao.ToString());
            _driver.FindElement(By.Id("checkboxStatusCreate")).Click();
            _driver.SetText(By.Name("LstIngrediente"), "Sódio");
            Esperar(3);
            _driver.FindElement(By.Id("btnCadastrar")).Click();
        }

        public void CadastroClientes()
        {
            Cliente cliente = GetCliente();

            _driver.SetText(By.Name("Email"), cliente.Email.ToString());
            _driver.SetText(By.Name("Login"), cliente.Login.ToString());
            _driver.SetText(By.Name("Senha"), cliente.Senha.ToString());
            _driver.SetText(By.Name("confirmSenha"), cliente.Senha.ToString());
            _driver.SetText(By.Name("Nome"), cliente.Nome.ToString());
            _driver.SetText(By.Name("Cpf"), cliente.Cpf.ToString());
            _driver.SetText(By.Name("DataNascimento"), cliente.DataNascimento.ToString("dd-MM-yyyy"));
            _driver.SetText(By.Name("Genero"), cliente.Genero.ToString());
            _driver.SetText(By.Name("LstDDD"), cliente.Telefones[0].DDD.ToString());
            _driver.SetText(By.Name("LstTelefone"), cliente.Telefones[0].Numero.ToString());
            _driver.SetText(By.Name("Endereco.Descricao"), cliente.Endereco.Descricao.ToString());
            _driver.SetText(By.Name("Endereco.CEP"), cliente.Endereco.CEP.ToString());
            _driver.SetText(By.Name("Endereco.Logradouro"), cliente.Endereco.Logradouro.ToString());
            _driver.SetText(By.Name("Endereco.Numero"), cliente.Endereco.Numero.ToString());
            _driver.SetText(By.Name("Endereco.Complemento"), cliente.Endereco.Complemento.ToString());
            _driver.SetText(By.Name("Endereco.Bairro"), cliente.Endereco.Bairro.ToString());
            _driver.SetText(By.Name("Endereco.Cidade"), cliente.Endereco.Cidade.ToString());
            //_driver.SetText(By.Name("Endereco.Estado"), cliente.Endereco.Estado.ToString());
            IWebElement dropDownListBox = _driver.FindElement(By.Name("Endereco.Estado"));
            SelectElement clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText("São Paulo");
            _driver.Wait1();
            _driver.SetText(By.Name("Cartao.Numero"), cliente.Cartao.Numero.ToString());
            _driver.SetText(By.Name("Cartao.NomeTitular"), cliente.Cartao.NomeTitular.ToString());           
            _driver.SetText(By.Name("Cartao.CodigoSeguranca"), cliente.Cartao.CodigoSeguranca.ToString());
            _driver.SetText(By.Name("Cartao.MesValidade"), cliente.Cartao.MesValidade.ToString());
            _driver.SetText(By.Name("Cartao.AnoValidade"), cliente.Cartao.AnoValidade.ToString());

            _driver.FindElement(By.Id("btnCadastrar")).Click();
        }

        public void LoginAdmin()
        {
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            _driver.LoadPage(TimeSpan.FromSeconds(10), ConfigurationManager.AppSettings["UrlTelaLogin"]);
            _driver.SetText(By.Name("Email"), "admin");
            _driver.SetText(By.Name("Senha"), "12345");
            _driver.FindElement(By.Id("btnEntrar")).Click();
        }

        public void NavegarParaTelaCadastroBebidas()
        {
            _driver.FindElement(By.Id("navTabBebida")).Click();
            Esperar(3);
            _driver.FindElement(By.Id("btnNovaBebida")).Click();
        }

        public void AprovarPedido()
        {
            _driver.FindElement(By.Id("navTabAprovarSaida")).Click();
            Esperar(3);
            _driver.FindElement(By.ClassName("btnopcoesPedidos")).Click();
            Esperar(3);
            _driver.FindElement(By.ClassName("btnColocarTransito")).Click();
            

        }

        public void NavegarParaTelaCadastroCliente()
        {
            _driver.LoadPage(TimeSpan.FromSeconds(10), ConfigurationManager.AppSettings["UrlTelaCadastroCliente"]);
        }


        public void Logout()
        {
            _driver.FindElement(By.Id("btnOpcoesMenu")).Click();
            Esperar(3);
            _driver.FindElement(By.Id("btnLogout")).Click();
        }

        public void AbrirPerfilCliente()
        {
            _driver.FindElement(By.Id("btnOpcoesMenu")).Click();
            Esperar(3);
            _driver.FindElement(By.Id("btnPerfilCliente")).Click();
        }

        public void LoginCliente()
        {
            _driver.LoadPage(TimeSpan.FromSeconds(5), ConfigurationManager.AppSettings["UrlTelaLogin"]);
            _driver.SetText(By.Name("Email"), "teste4@teste.com");
            _driver.SetText(By.Name("Senha"), "Bielzinh@2");
            _driver.FindElement(By.Id("btnEntrar")).Click();
        }

        public void IrParaLogin()
        {
            _driver.FindElement(By.Id("")).Click();
            Esperar(5);
        }

        public void TelaInicial()
        {
            _driver.FindElement(By.Id("btnHome")).Click();
        }

        public void SelecionaBebida()
        {
            _driver.FindElement(By.ClassName("dashBebida")).Click();
            Esperar(5);
            _driver.FindElement(By.Id("btnAddCarrinho")).Click();
        }

        public void IrParaCheckout()
        {
            _driver.FindElement(By.Id("btnFinalizarPedido")).Click();
        }

        public void Checkout()
        {
            //_driver.FindElement(By.Id("dropEndereco")).Click();
            IWebElement dropDownListBox = _driver.FindElement(By.Name("IdEnderecoEntrega"));
            SelectElement clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText("Minha Casa - Rua abc, 123");
            _driver.Wait1();

            //_driver.FindElement(By.Id("btnNovoEndereco")).Click();
            //_driver.Wait1();
            //_driver.FindElement(By.Id("btnNovoEnderecoFechar")).Click();
            //_driver.Wait1();

            //_driver.FindElement(By.Id("SelectCartao")).Click();
            dropDownListBox = _driver.FindElement(By.Name("IdCartao1"));
            clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText("Mastercard - final 1511");
            _driver.Wait1();
            //_driver.FindElement(By.Id("Pagar2Cartoes")).Click();
            //_driver.Wait1();
            //_driver.FindElement(By.Name("valorCartao2")).Click();
            //_driver.Wait1();
            //_driver.FindElement(By.Id("Pagar2Cartoes")).Click();
            //_driver.Wait1();
            _driver.SetText(By.Name("Cep"), "08740-400");
            _driver.Wait1();
            _driver.FindElement(By.Id("btnCalcularFrete")).Click();

            _driver.SetText(By.Id("inputCupom"), "Desconto");
            _driver.Wait1();
            _driver.FindElement(By.Id("btnValidarCupom")).Click();
            _driver.Wait1();

            _driver.FindElement(By.Id("btnFinalizarPedido")).Click();
            _driver.Wait1();
        }

        public void CheckoutComCupom(string cupom)
        {
            //_driver.FindElement(By.Id("dropEndereco")).Click();
            IWebElement dropDownListBox = _driver.FindElement(By.Name("IdEnderecoEntrega"));
            SelectElement clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText("Minha Casa - Rua abc, 123");
            _driver.Wait1();

            //_driver.FindElement(By.Id("btnNovoEndereco")).Click();
            //_driver.Wait1();
            //_driver.FindElement(By.Id("btnNovoEnderecoFechar")).Click();
            //_driver.Wait1();

            //_driver.FindElement(By.Id("SelectCartao")).Click();
            dropDownListBox = _driver.FindElement(By.Name("IdCartao1"));
            clickThis = new SelectElement(dropDownListBox);
            clickThis.SelectByText("Mastercard - final 1511");
            _driver.Wait1();
            //_driver.FindElement(By.Id("Pagar2Cartoes")).Click();
            //_driver.Wait1();
            //_driver.FindElement(By.Name("valorCartao2")).Click();
            //_driver.Wait1();
            //_driver.FindElement(By.Id("Pagar2Cartoes")).Click();
            //_driver.Wait1();
            _driver.SetText(By.Name("Cep"), "08740-400");
            _driver.Wait1();
            _driver.FindElement(By.Id("btnCalcularFrete")).Click();

            _driver.SetText(By.Id("inputCupom"), cupom);
            _driver.Wait1();
            _driver.FindElement(By.Id("btnValidarCupom")).Click();
            _driver.Wait1();

            _driver.FindElement(By.Id("btnFinalizarPedido")).Click();
        }

        public void InserirDatas()
        {
            _driver.SetText(By.Name("Inicio"), "10/09/2019");
            _driver.SetText(By.Name("Fim"), "31/12/2019");
            _driver.FindElement(By.Id("btnPesquisar")).Click();
        }

        public void SolicitarTroca()
        {
            _driver.FindElement(By.Id("opcoesPedidos")).Click();
            _driver.Wait1();
            
            _driver.FindElement(By.ClassName("btnSolicitarTroca")).Click();
            _driver.Wait1();

            _driver.FindElement(By.ClassName("checkboxTrocaItem")).Click();
            _driver.Wait1();

            _driver.SetText(By.Name("MotivoSolicitacao"), "O produto estava vencido!");
            _driver.Wait1();
            _driver.FindElement(By.Id("btnSolicitarTroca")).Click();
            _driver.Wait1();
        }

        public void TratarSolicitacaoTroca()
        {
            _driver.FindElement(By.Id("navTabTroca")).Click();
            _driver.Wait1();

            _driver.FindElement(By.Id("opcoesTrocaPedidos")).Click();
            _driver.Wait1();
            _driver.FindElement(By.Id("btnReprovar")).Click();
            _driver.Wait1();
            _driver.SetText(By.Name("MotivoReprova"), "O produto NÃO estava vencido !!!");
            _driver.Wait1();
            _driver.FindElement(By.Id("cancelaReprova")).Click();
            _driver.Wait1();

            _driver.FindElement(By.Id("opcoesTrocaPedidos")).Click();
            _driver.Wait1();
            _driver.FindElement(By.Id("btnAprovar")).Click();
            _driver.Wait1();
            //_driver.FindElement(By.Id("selectCupomTroca")).Click();
            //_driver.Wait1();
            _driver.FindElement(By.Id("btnAprovarTroca")).Click();
            _driver.Wait1();
        }

        public string AbrirCupons()
        {
            string nomecupom;
            _driver.FindElement(By.Id("tabCuponsCliente")).Click();
            Esperar(5);
            nomecupom = _driver.FindElement(By.ClassName("tdNomeCupom")).GetAttribute("data-desc");
            return nomecupom;
        }

        public void Fechar()
        {
            _driver.Quit();
            _driver = null;
        }

        public Cliente GetCliente()
        {
            return new Cliente
            {
                Id = 1,
                Cpf = "323.144.349-00",
                DataNascimento = DateTime.Parse("09-06-2000"),
                Email = "teste4@teste.com",
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
                    Bandeira = Bandeira.Mastercard,
                    CodigoSeguranca = 123,
                    NomeTitular = "Gabriel Lima Gomes",
                    Numero = "1234-5678-9000-1511",
                    MesValidade = 02,
                    AnoValidade = 2019
                    
                },
                Genero = "Masculino",
                Login = "teste",
                Senha = "Bielzinh@2",
                Nome = "Usuário Teste",
                Telefones = new List<Telefone>
                {
                    new Telefone
                    {
                        DDD = 11,
                        Numero = "40028922",
                        IdTipo = 1
                    }
                }
                
            };
        }

        public Bebida GetBebida()
        {
            return new Bebida
            {
                Id = 1,
                Nome = "Bebida Teste",
                TipoBebida = new TipoBebida
                {
                    Id = 1
                },
                Descricao = "Descricao Teste",
                Marca = "Marca Teste",
                Valor = 27.99M,
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