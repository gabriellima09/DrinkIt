using DrinkIt.WebApp.Dao;
using DrinkIt.WebApp.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult GetGraficoVendas(string inicio, string fim)
        {
            Dashboard dash = new DashboardDao().GetDashboardVendas(inicio, fim);

            return PartialView("PvScriptGraficoVendas", dash);
        }

        public ActionResult PvScriptGraficoVendas(Dashboard dash)
        {
            if (dash == null)
            {
                dash = new Dashboard
                {
                    DataAlcoolico = new List<string>
                    {
                        "01/2019",
                        "02/2019",
                        "03/2019",
                        "04/2019",
                        "05/2019",
                        "06/2019",
                        "07/2019",
                        "08/2019",
                        "09/2019",
                        "10/2019",
                        "11/2019",
                        "12/2019"
                    },
                    ValoresAlcoolicos = new List<int>(),
                    ValoresNaoAlcoolicos = new List<int>()
                };
            }

            if (dash.DataAlcoolico == null)
            {
                dash.DataAlcoolico = new List<string>
                    {
                        "01/2019",
                        "02/2019",
                        "03/2019",
                        "04/2019",
                        "05/2019",
                        "06/2019",
                        "07/2019",
                        "08/2019",
                        "09/2019",
                        "10/2019",
                        "11/2019",
                        "12/2019"
                    };
                dash.DataNaoAlcoolico = dash.DataAlcoolico;
            }
            if (dash.ValoresAlcoolicos == null)
            {
                dash.ValoresAlcoolicos = new List<int>();
            }
            if (dash.ValoresNaoAlcoolicos == null)
            {
                dash.ValoresNaoAlcoolicos = new List<int>();
            }

            return PartialView(dash);
        }

    }
}
