using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkIt.WebApp.Dao
{
    interface ITelefone
    {
        List<Telefone> ConsultarPorCliente(int IdCliente);
    }
}
