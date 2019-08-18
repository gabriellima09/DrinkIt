using DrinkIt.WebApp.Models;
using System;
using Xunit;

namespace DrinkIt.WebApp.Selenium
{
    public class TesteCadastroBebida
    {
        [Fact]
        public void TestarCadastroBebida()
        {
            Bebida bebida = new Bebida
            {
                Id = 1,
                Nome = "Bebida Teste",
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
                Teor = "10%",
                Gaseificada = false,
                ContemGluten = false,
                DicaConservacao = "Dica Teste",
                Status = "ATIVO"
            };

            TelaCadastroBebida tela = new TelaCadastroBebida(Browser.Chrome);

            tela.CarregarPagina();
            tela.PreencherDadosBebida(bebida);
            tela.ProcessarCadastro();
            tela.Esperar(5);
            tela.Fechar();
        }

    }
}