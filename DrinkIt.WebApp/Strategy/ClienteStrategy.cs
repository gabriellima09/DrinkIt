using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Strategy
{
    public class ClienteStrategy : IStrategy
    {
        public bool Processar(EntidadeDominio entidade)
        {
            Cliente cliente = (Cliente)entidade;

            return ValidadorAuxiliar.ValidarData(cliente.DataNascimento)
                && ValidadorAuxiliar.ValidarEmail(cliente.Email)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(cliente.Login)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(cliente.Senha)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(cliente.Nome)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(cliente.Genero)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(cliente.Telefone);
        }
    }
}