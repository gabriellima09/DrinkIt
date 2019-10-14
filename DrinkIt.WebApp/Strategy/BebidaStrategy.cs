using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Strategy
{
    public class BebidaStrategy : IStrategy
    {
        public string Processar(EntidadeDominio entidade)
        {
            Bebida bebida = (Bebida)entidade;

            return Validar(bebida) ? string.Empty : "Bebida Inválida!";
        }

        public bool Validar(Bebida bebida)
        {
            if (
                string.IsNullOrEmpty(bebida.Nome) ||
                string.IsNullOrEmpty(bebida.Descricao) ||
                bebida.TipoBebida.Id == 0 ||
                //bebida.TipoBebida.Id == 0 ||
                string.IsNullOrEmpty(bebida.Marca) ||
                string.IsNullOrEmpty(bebida.Volume) ||
                bebida.Valor == 0 ||
                string.IsNullOrEmpty(bebida.Peso) ||
                string.IsNullOrEmpty(bebida.Sabor) ||
                string.IsNullOrEmpty(bebida.Lote) ||
                bebida.DataFabricacao == null ||
                bebida.DataValidade == null ||
                string.IsNullOrEmpty(bebida.Fabricante) ||
                string.IsNullOrEmpty(bebida.Embalagem) ||
                string.IsNullOrEmpty(bebida.CodigoBarras) ||
                bebida.Ingredientes.Count == 0 ||
                string.IsNullOrEmpty(bebida.DicaConservacao) ||
                string.IsNullOrEmpty(bebida.CaminhoImagem) ||
                string.IsNullOrEmpty(bebida.Nome)
            ) return false;

            //Caso seja alcoólica, deve-se fornecer o teor.
            if (bebida.Alcoolico && bebida.Teor == 0)
                return false;

            //OK, bebida validada.
            return true;// ValidarBebida(bebida); > verificar se os dados estão todos corretos
        }

    }
}