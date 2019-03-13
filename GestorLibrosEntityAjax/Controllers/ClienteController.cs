using GestorLibrosEntityAjax.Filtro;
using GestorLibrosEntityAjax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestorLibrosEntityAjax.Controllers
{
    public class ClienteController : Controller
    {
        Entities contexto = new Entities();

        [FiltroUser]
        public ActionResult verFacturas()

        {
            tusuario usuario = (Session["usuario"] as tusuario);


            return View(usuario.tfactura.ToList<tfactura>());
        }
        [FiltroUser]
        public ActionResult detalleFactura(int CodFactura)
        {

            List<LineaVenta> listaVentas = new List<LineaVenta>();

            foreach (tlineafactura lineaFactura in contexto.tlineafactura.Where(e => e.CodFactura == CodFactura).ToList())
            {
                listaVentas.Add(new LineaVenta(lineaFactura.tlibro, Int32.Parse(lineaFactura.Cantidad)));
            }

            return View(listaVentas);
        }

    }
}