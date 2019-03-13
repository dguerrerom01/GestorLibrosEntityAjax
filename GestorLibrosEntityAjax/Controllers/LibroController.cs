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
    public class LibroController : Controller
    {
        Entities contexto = new Entities();

        public ActionResult Consultar()
        {
            return View(contexto.tlibro.Where(e => e.Borrado == "0").ToList());
        }


        public ActionResult verLibro(string CodLibro)
        {
            return View(contexto.tlibro.Find(int.Parse(CodLibro)));
        }

        // Creamos el metodo addLibro que manda a la vista una lista de objetos genericos, en este caso necesitamos la lista de temas-
        [FiltroAdmin]
        public ActionResult addLibro()
        {   //Obtenemos la lisa de temas
            return View(contexto.ttema.ToList());
        }

        //Aqui repetimos el mismo metodo, pero le entra un Libro.
        //Esto es una anotacion, dice que es un metodo post. 
        [FiltroAdmin]
        [HttpPost]
        public ActionResult addLibro(tlibro libro)
        {
            //Se crea una transaccion

            DbContextTransaction transaccion = contexto.Database.BeginTransaction();
            //Hacemos la operaciones necesarias para guardar el nuevo libro.


            libro.Precio = libro.Precio.Replace(".", ",");
            //   libro.CodLibro = UtilFichero.GenerarCodigo(libro.GetType());
            libro.Borrado = "0";
            libro.Formatouno = libro.Formatouno == null ? "N/A" : libro.Formatouno;
            libro.Formatodos = libro.Formatodos == null ? "N/A" : libro.Formatodos;
            libro.Formatotres = libro.Formatotres == null ? "N/A" : libro.Formatotres;

            contexto.tlibro.Add(libro);



            if (contexto.SaveChanges() == 1)
            {
                transaccion.Commit();
                return Content(Mensaje.mostrarmensaje("Libro insertado correctamente", "addLibro"));
            }
            else
            {
                transaccion.Dispose();
                return Content(Mensaje.mostrarmensaje("Error en la transccion...", "addLibro"));

            }
        }

        /*
         *Seguidamente vamos a crear el metodo modificarLibro, aqui.. Hay un problema necesitamos mandarle a la vista 2 modelos, como? Pues un array. 
         * 
         */
        [FiltroAdmin]
        public ActionResult modificarLibro(string codLibro)
        {
            //Creamos el array de objet de 2 posiciones, en el guardaremos el libro y la lista de Temas.
            object[] modelos = new object[2];
            //Obtenermos el libro en a editar en concreto.
            modelos[0] = contexto.tlibro.Find(int.Parse(codLibro));
            //Obtenemos los tipos de libros
            modelos[1] = contexto.ttema.ToList<ttema>();
            //Devolvemos el array a la lista.
            return View(modelos);
        }

        [FiltroAdmin]
        [HttpPost]
        public ActionResult modificarLibro(tlibro libro)
        {

            DbContextTransaction transaccion = contexto.Database.BeginTransaction();
            //   contexto.tlibro.Find(libro.CodLibro).IDTema = libro.IDTema;
            contexto.tlibro.Find(libro.CodLibro).Precio = libro.Precio.Replace(".", ",");
            contexto.tlibro.Find(libro.CodLibro).Borrado = "0";
            contexto.tlibro.Find(libro.CodLibro).Formatouno = libro.Formatouno == null ? "N/A" : libro.Formatouno;
            contexto.tlibro.Find(libro.CodLibro).Formatodos = libro.Formatodos == null ? "N/A" : libro.Formatodos;
            contexto.tlibro.Find(libro.CodLibro).Formatotres = libro.Formatotres == null ? "N/A" : libro.Formatotres;



            if (contexto.SaveChanges() == 1)
            {
                transaccion.Commit();
                return RedirectToAction("Consultar");
            }
            else
            {
                transaccion.Dispose();
                return Content(Mensaje.mostrarmensaje("Error", "Consultar"));

            }
        }

        //  return RedirectToAction("Inicio");


        [FiltroAdmin]
        public ActionResult borrarLibro(string CodLibro)
        {

            DbContextTransaction transaccion = contexto.Database.BeginTransaction();

            contexto.tlibro.Find(CodLibro).Borrado = "1";

            if (contexto.SaveChanges() == 1)
            {
                transaccion.Commit();
                return RedirectToAction("Consultar");
            }
            else
            {
                transaccion.Dispose();
                return Content(Mensaje.mostrarmensaje("erroe", "modificarLibro"));

            }


        }

        public ActionResult addCarrito(string CodLibro, string accion)
        {


            Carrito carrito = Session["carro"] as Carrito;
            carrito.AddItem(contexto.tlibro.Find(int.Parse(CodLibro)));
            /*
             * Estas dos lineas se pueden unificar en una : 
             *(Session["carro"] as Carrito).AddItem((control.Buscar(new TLibro().GetType(), CodLibro)) as TLibro); 
             */

            return RedirectToAction("Consultar");
        }


        public ActionResult borrarItemCarrito(string CodLibro)
        {
            (Session["carro"] as Carrito).RemoveItem(contexto.tlibro.Find(int.Parse(CodLibro)));

            return RedirectToAction("Consultar");

        }

    }

}