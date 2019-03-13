using GestorLibrosEntityAjax.Comun;
using GestorLibrosEntityAjax.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestorLibrosEntityAjax.Controllers
{
    public class InicioController : Controller
    {
        Entities contexto = new Entities();
        public ActionResult Inicio()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(tusuario usuario)
        {
            tusuario usuTemp = contexto.tusuario.Where(e => e.Nick == usuario.Nick).ToList().Count == 0 ? null : contexto.tusuario.Where(e => e.Nick == usuario.Nick).ToList().First();

            if (usuTemp != null)
            {
                if (ComprobarUsuario(usuario, usuTemp))
                {
                    //Lo ponemos en null para que no se guarde la clave en la sesion por seguridad.
                    usuTemp.Pass = null;

                    Session.Add("usuario", usuTemp);
                    return View("Inicio");
                }
                else
                {

                    return Content(Mensaje.mostrarmensaje("Contraseña Incorrecta...", "Inicio"));

                }
            }
            else
            {

                return Content(Mensaje.mostrarmensaje("Usuario no encontrado..., registrese", "Inicio"));
            }


        }
        [HttpPost]
        public ActionResult Registro(tusuario usuario)
        {
            if (contexto.tusuario.Where(e => e.Nick == usuario.Nick).ToList().Count == 0)
            {
                usuario.Rol = "user";

                DbContextTransaction transaccion = contexto.Database.BeginTransaction();

                contexto.tusuario.Add(usuario);

                if (contexto.SaveChanges() == 1)
                {
                    transaccion.Commit();
                    Session["usuario"] = usuario;
                    return Content(Mensaje.mostrarmensaje("Registrado Correctamente", "Inicio"));

                }
                else
                {
                    transaccion.Dispose();

                    return Content(Mensaje.mostrarmensaje("Fallo a la hora de guardar", "Inicio"));
                }

            }


            return Content(Mensaje.mostrarmensaje("El usuario ya existe", "Inicio"));
        }


        private bool addUsuario(tusuario usuario)
        {

            return true;
        }

        private bool ComprobarUsuario(tusuario usuario, tusuario temporal)
        {

            if (usuario.Rol == null && temporal != null)
            {
                if (usuario.Pass.Equals(temporal.Pass))
                {
                    return true;
                }
            }
            else if (usuario.Rol != null && temporal == null)
            {
                return true;
            }
            return false;
        }


    }
}