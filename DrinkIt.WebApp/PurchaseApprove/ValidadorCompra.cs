using DrinkIt.WebApp.Models;
using System;

namespace DrinkIt.WebApp.PurchaseApprove
{
    public class ValidadorCompra
    {
        private static Bandeira Bandeira { get; set; }

        public ValidadorCompra(Bandeira bandeira)
        {
            Bandeira = bandeira;
        }

        public static bool ValidarCompra()
        {
            switch (Bandeira)
            {
                case Bandeira.Mastercard:
                    return new Random().Next() > 0;
                case Bandeira.Visa:
                    return new Random().Next() > 0;
                default:
                    throw new NotImplementedException();
            }
        }

    }
}