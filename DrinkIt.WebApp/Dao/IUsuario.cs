using DrinkIt.WebApp.Models;

namespace DrinkIt.WebApp.Dao
{
    public interface IUsuario
    {
        bool Login(string email, string senha);
        Usuario RecuperarUsuario(string email);
    }
}