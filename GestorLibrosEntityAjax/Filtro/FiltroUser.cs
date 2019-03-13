using GestorLibrosEntityAjax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GestorLibrosEntityAjax.Filtro
{ 
public class FiltroUser : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        tusuario usuario = (tusuario)filterContext.HttpContext.Session["usuario"];

        if (usuario == null)
        {
            Redirigir(filterContext);
        }


    }

    private void Redirigir(ActionExecutingContext filterContext)
    {
        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
            {
                { "controller","Inicio" },
                {"action","Inicio" }
            });
    }
}
}