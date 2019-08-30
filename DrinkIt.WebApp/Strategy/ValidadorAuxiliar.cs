using System;
using System.Text.RegularExpressions;

namespace DrinkIt.WebApp.Strategy
{
    public static class ValidadorAuxiliar
    {
        public static bool ValidarData(this DateTime data)
        {
            return data > DateTime.MinValue && data <= DateTime.Now;
        }

        public static bool ValidarEmail(this string email)
        {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            return !string.IsNullOrWhiteSpace(email) && rg.IsMatch(email);
        }

        public static bool ValidarPropriedadeVazia(this string propriedade)
        {
            return !string.IsNullOrWhiteSpace(propriedade);
        }
    }
}