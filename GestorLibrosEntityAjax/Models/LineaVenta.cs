using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestorLibrosEntityAjax.Models
{
    public class LineaVenta
    {
        private static readonly int DEFAULT_QUANTITY = 1;

        private tlibro _libro;
        private int _cantidad;

        public tlibro Libro { get => _libro; }
        public int Cantidad { get => _cantidad; }

        public LineaVenta(tlibro libro, int cantidad)
        {
            this._libro = libro;
            this._cantidad = cantidad;
        }

        public LineaVenta(tlibro libro)
        {
            this._libro = libro;
            this._cantidad = DEFAULT_QUANTITY;
        }

        public void IncrementQuantity()
        {
            this._cantidad++;
        }

        public float subTotal()
        {
            return float.Parse(_libro.Precio) * this.Cantidad;
        }

        public void ReduceQuantity()
        {
            this._cantidad--;
        }

        public bool IsEmpty()
        {
            return this._cantidad <= 0;
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return this.Libro.Equals(((LineaVenta)obj).Libro);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return this.Libro.GetHashCode();
        }
    }
}