using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Strategy
{
    public class ClienteStrategy : IStrategy
    {
        public string Processar(EntidadeDominio entidade)
        {
            Cliente cliente = (Cliente)entidade;

            return Validar(cliente) ? string.Empty : "Cliente inválido!";
        }

        private bool Validar(Cliente cliente)
        {
            return ValidadorAuxiliar.ValidarData(cliente.DataNascimento)
                && ValidadorAuxiliar.ValidarEmail(cliente.Email)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(cliente.Login)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(cliente.Senha)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(cliente.Nome)
                && ValidadorAuxiliar.ValidarPropriedadeVazia(cliente.Genero);
        }
    }
}