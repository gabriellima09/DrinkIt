using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Strategy
{
    public class CpfStrategy : IStrategy
    {
        public string Processar(EntidadeDominio entidade)
        {
            Cliente cliente = (Cliente)entidade;

            return ValidarCpf(cliente?.Cpf) ? string.Empty : "Cpf Inválido!";
        }

        private bool ValidarCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return false;
            }

            if (cpf.Equals("00000000000") ||
                cpf.Equals("11111111111") ||
                cpf.Equals("22222222222") || cpf.Equals("33333333333") ||
                cpf.Equals("44444444444") || cpf.Equals("55555555555") ||
                cpf.Equals("66666666666") || cpf.Equals("77777777777") ||
                cpf.Equals("88888888888") || cpf.Equals("99999999999") ||
                (cpf.Length != 11))
            {
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return false;
            }
            else
            {
                var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                cpf = cpf.Trim().Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                    return false;

                for (int j = 0; j < 10; j++)
                    if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                        return false;

                var tempCpf = cpf.Substring(0, 9);
                var soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

                var resto = soma % 11;
                resto = resto < 2 ? 0 : 11 - resto;

                var digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

                resto = soma % 11;
                resto = resto < 2 ? 0 : 11 - resto;

                digito = digito + resto.ToString();

                return cpf.EndsWith(digito);
            }
        }

    }
}