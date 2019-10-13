using DrinkIt.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DrinkIt.WebApp.Dao
{
    interface ITipoBebida
    {
        IEnumerable<SelectListItem> GetGruposPrecificacao();
    }
}
