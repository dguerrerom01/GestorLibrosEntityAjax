using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestorLibrosEntityAjax.Comun
{
    class Errores : Exception
    {
        Exception _error;
        public static string ERRORCLAVE = "Clave";
        public static string ERROR_FICHERO = "fichero";
        public Errores(Exception e)
        {
            _error = e;
        }
        public static string ControlErrores(Exception e)
        {
            if (e is System.IO.IOException)
            {
                return "ERROR: al tratar el fichero";
            }
            else if (e is System.IO.FileNotFoundException)
            {
                return "ERROR: Archivo no encontrado";
            }
            else if (e is System.Data.SqlClient.SqlException)
            {
                return "ERROR: Base de datos";
            }
            else if (e is TypeLoadException)
            {
                return "ERROR: Clase no encontrada";
            }
            else if (e is TypeAccessException)
            {
                return "ERROR: No se pudo hacer una instancia reflexiva";
            }
            else if (e is TypeInitializationException)
            {
                return "ERROR: No se pudo instanciar";
            }
            else if (e is ArgumentException)
            {
                return "ERROR: Metodo inválido";
            }
            else if (e.Equals(ERRORCLAVE))
            {
                return "ERROR: No existe la clave principal";
            }
            else if (e.Equals(ERROR_FICHERO))
            {
                return "ERROR: No existe la clave principal";
            }
            return "ERROR desconocido";

        }

        public override string Message { get; }
    }

}
