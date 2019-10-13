using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DrinkIt.WebApp.Models
{
    public class Cliente : Usuario
    {
        [Required]
        public string Genero { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        public string Cpf { get; set; }
        [Required]
        public List<Telefone> Telefones { get; set; }
        [Required]
        public Endereco Endereco { get; set; }
        public List<Endereco> Enderecos { get; set; }
        [Required]
        public CartaoCredito Cartao { get; set; }
        public List<CartaoCredito> Cartoes { get; set; }
        public bool Status { get; set; }
    }
}