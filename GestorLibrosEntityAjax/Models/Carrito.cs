using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestorLibrosEntityAjax.Models
{
    public class Carrito
    {
        public List<LineaVenta> ItemList { get; } = new List<LineaVenta>();

        public bool AddItem(tlibro libro)
        {
            if (libro != null)
            {
                LineaVenta lineaVenta = new LineaVenta(libro);
                Predicate<LineaVenta> filter = (LineaVenta ln) => { return lineaVenta.Equals(ln); };
                if (!ItemList.Exists(filter))
                {
                    ItemList.Add(lineaVenta);
                    return true;
                }
                else
                {
                    ItemList.Find(filter).IncrementQuantity();
                    return true;

                }
            }
            return false;
        }

        public bool RemoveItem(tlibro libro)
        {
            if (libro != null)
            {
                LineaVenta lineaVenta = new LineaVenta(libro);
                Predicate<LineaVenta> filter = (LineaVenta ln) => { return lineaVenta.Equals(ln); };
                if (!ItemList.Exists(filter))
                {
                    return false;
                }
                else
                {
                    LineaVenta lineaObtenida = ItemList.Find(filter);
                    lineaObtenida.ReduceQuantity();
                    if (lineaObtenida.IsEmpty())
                        ItemList.Remove(lineaObtenida);
                    return true;

                }
            }
            return false;
        }

        public void limpiar()
        {
            Predicate<LineaVenta> filter = (LineaVenta ln) => { return true; };
            this.ItemList.RemoveAll(filter);
        }
    }
}