using DrinkIt.WebApp.Models;
using System;
using Xunit;

namespace DrinkIt.WebApp.Selenium
{
    public class TesteCadastroCliente
    {
        [Fact]
        public void TestarCadastroCliente()
        {
            Cliente cliente = new Cliente
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
                    Bandeira = "Visa",
                    CodigoSeguranca = 123,
                    NomeTitular = "",
                    Numero = "123456789000"
                },
                Genero = "Masculino",
                Telefone = "(11) 91234-5678",
                Login = "teste",
                Senha = "teste",
                Nome = "Usuário Teste"
            };

            TelaCadastroCliente tela = new TelaCadastroCliente(Browser.Chrome);

            tela.CarregarPagina();
            tela.PreencherDadosCliente(cliente);
            tela.ProcessarCadastro();
            tela.Esperar(5);
            tela.Fechar();
        }

    }
}