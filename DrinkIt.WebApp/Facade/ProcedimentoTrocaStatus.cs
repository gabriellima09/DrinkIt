using DrinkIt.WebApp.Dao;

namespace DrinkIt.WebApp.Facade
{
    public class ProcedimentoTrocaStatus
    {
        private int IdStatus = 0;

        public void Entregue(int idPedido)
        {
            IdStatus = 1;
            new ProcedimentoTrocaStatusDao().TrocarStatus(idPedido, IdStatus);
        }

    }
}