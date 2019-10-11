using DrinkIt.WebApp.Models;
using System.Collections.Generic;

namespace DrinkIt.WebApp.Dao
{
    public interface IBebida
    {
        void GravarMotivoInativacao(int id, string motivo);
        List<Bebida> ConsultarComFiltro(int idGas = 0, int idTeor = 0, int idValor = 0, string textoBusca = "");
    }
}