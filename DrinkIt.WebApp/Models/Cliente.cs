using System;

namespace DrinkIt.WebApp.Models
{
    public class Cliente : Usuario
    {
        public string Genero { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }
    }
}