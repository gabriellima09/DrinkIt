using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Dao
{
    public interface IBebida
    {
        void GravarMotivoInativacao(int id, string motivo);
    }
}