using DrinkIt.WebApp.Dao;

namespace DrinkIt.WebApp.Facade
{
    public class ProcedimentoTrocaStatus
    {
        private int IdStatus = 0;

        public void EmProcessamento(int idPedido)
        {
            IdStatus = 1;
            new ProcedimentoTrocaStatusDao().TrocarStatus(idPedido, IdStatus);
        }

        public void Aprovada(int idPedido)
        {
            IdStatus = 2;
            new ProcedimentoTrocaStatusDao().TrocarStatus(idPedido, IdStatus);
        }

        public void Reprovada(int idPedido)
        {
            IdStatus = 3;
            new ProcedimentoTrocaStatusDao().TrocarStatus(idPedido, IdStatus);
        }

        public void EmTransito(int idPedido)
        {
            IdStatus = 4;
            new ProcedimentoTrocaStatusDao().TrocarStatus(idPedido, IdStatus);
        }

        public void EmTransporte(int idPedido)
        {
            IdStatus = 5;
            new ProcedimentoTrocaStatusDao().TrocarStatus(idPedido, IdStatus);
        }

        public void Entregue(int idPedido)
        {
            IdStatus = 6;
            new ProcedimentoTrocaStatusDao().TrocarStatus(idPedido, IdStatus);
        }

        public void EmTroca(int idPedido)
        {
            IdStatus = 7;
            new ProcedimentoTrocaStatusDao().TrocarStatus(idPedido, IdStatus);
        }

        public void TrocaAutorizada(int idPedido)
        {
            IdStatus = 8;
            new ProcedimentoTrocaStatusDao().TrocarStatus(idPedido, IdStatus);
        }

        public void TrocaNaoAutorizada(int idPedido)
        {
            IdStatus = 9;
            new ProcedimentoTrocaStatusDao().TrocarStatus(idPedido, IdStatus);
        }

        public void Trocado(int idPedido)
        {
            IdStatus = 10;
            new ProcedimentoTrocaStatusDao().TrocarStatus(idPedido, IdStatus);
        }

        public void Cancelado(int idPedido)
        {
            IdStatus = 11;
            new ProcedimentoTrocaStatusDao().TrocarStatus(idPedido, IdStatus);
        }

        public void Finalizado(int idPedido)
        {
            IdStatus = 12;
            new ProcedimentoTrocaStatusDao().TrocarStatus(idPedido, IdStatus);
        }
    }
}