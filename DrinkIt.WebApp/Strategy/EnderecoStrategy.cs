using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Strategy
{
    public class EnderecoStrategy : IStrategy
    {
        public bool Processar(EntidadeDominio entidade)
        {
            Endereco endereco = (Endereco)entidade;

            return endereco != null
                && ValidadorAuxiliar.ValidarPropriedadeVazia(endereco.Bairro)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(endereco.CEP)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(endereco.Cidade)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(endereco.Descricao)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(endereco.Estado)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(endereco.Logradouro)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(endereco.Numero);
        }
    }
}