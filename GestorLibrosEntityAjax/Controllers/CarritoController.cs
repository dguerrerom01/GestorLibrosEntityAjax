using GestorLibrosEntityAjax.Comun;
using GestorLibrosEntityAjax.Filtro;
using GestorLibrosEntityAjax.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestorLibrosEntityAjax.Controllers
{
    public class CarritoController : Controller
    {
        Entities contexto = new Entities();

        // GET: Carrito
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult carrito()
        {
            return View();
        }

        [FiltroUser]
        public ActionResult doFactura()
        {
            DbContextTransaction transaccionFactura = contexto.Database.BeginTransaction();

            tfactura factura = new tfactura();

            factura.CodUsuario = (Session["usuario"] as tusuario).CodUsuario;
            factura.Borrado = "0";
            factura.FechaFactura = DateTime.Now.ToShortDateString();
            contexto.tfactura.Add(factura);


            if (contexto.SaveChanges() == 1)
            {
                transaccionFactura.Commit();
                transaccionFactura.Dispose();
            }
            else
            {
                transaccionFactura.Rollback();
                transaccionFactura.Dispose();
            }


            foreach (LineaVenta linea in (Session["carro"] as Carrito).ItemList)
            {
                DbContextTransaction transaccion = contexto.Database.BeginTransaction();
                tlineafactura lineaTemp = new tlineafactura();
                //EntityFramework automaticamente asigna los CodFacturas. asique no tenemos que asignarlos. 
                lineaTemp.CodFactura = factura.CodFactura;
                lineaTemp.CodLibro = linea.Libro.CodLibro;
                lineaTemp.Cantidad = linea.Cantidad.ToString();
                lineaTemp.Total = linea.subTotal().ToString();
                contexto.tlineafactura.Add(lineaTemp);
                if (contexto.SaveChanges() == 1)
                {
                    transaccion.Commit();
                    transaccion.Dispose();
                }
                else
                {
                    transaccion.Rollback();
                    transaccion.Dispose();
                    return Content(Mensaje.mostrarmensaje("Error en la transaccion...", "carrito"));
                }
            }

             (Session["carro"] as Carrito).limpiar();
            return RedirectToRoute(new { controller = "Inicio", action = "Inicio", id = UrlParameter.Optional });
        }
    }
}