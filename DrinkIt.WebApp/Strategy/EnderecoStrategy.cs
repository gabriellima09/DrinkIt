using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Strategy
{
    public class EnderecoStrategy : IStrategy
    {
        public string Processar(EntidadeDominio entidade)
        {
            Endereco endereco = (Endereco)entidade;

            return Validar(endereco) ? string.Empty : "Endereço inválido!";
        }

        private bool Validar(Endereco endereco)
        {
            return endereco != null
                && ValidadorAuxiliar.ValidarPropriedadeVazia(endereco.Bairro)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(endereco.CEP)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(endereco.Cidade)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(endereco.Descricao)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(endereco.Estado)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(endereco.Logradouro)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(endereco.Numero)
                && (endereco.Cobranca || endereco.Entrega);
        }
    }
}