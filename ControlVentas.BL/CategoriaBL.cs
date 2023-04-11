using ControlVentas.DAL;
// Referencias
using ControlVentas.EN;
using System.Collections.Generic;

namespace ControlVentas.BL
{/// <summary>
/// Contiene los metos de la capa DAL y establece conexion con la base de datos para hacer los procesos CRUD
/// </summary>
    public class CategoriaBL
    {
        /// <summary>
        /// Guarda los datos en la base de datos
        /// </summary>
        /// <param name="pCategoria"></param>
        /// <returns></returns>
        public int Guardar(Categoria pCategoria)
        {
            pCategoria.Estado = 1;
            return CategoriaDAL.Guardar(pCategoria);
        }
        /// <summary>
        /// Modifica los datos en la base de datos
        /// </summary>
        /// <param name="pCategoria"></param>
        /// <returns></returns>
        public int Modificar(Categoria pCategoria)
        {
            pCategoria.Estado = 1;
            return CategoriaDAL.Modificar(pCategoria);
        }
        /// <summary>
        /// Elimina los datos en la base de datos
        /// </summary>
        /// <param name="pCategoria"></param>
        /// <returns></returns>
        public int Eliminar(Categoria pCategoria)
        {
            return CategoriaDAL.Eliminar(pCategoria);
        }
        /// <summary>
        /// Realiza una busca por medio del Id los datos en la base de datos
        /// </summary>
        /// <param name="pCategoria"></param>
        /// <returns></returns>
        public Categoria BuscarPorId(int pId)
        {
            return CategoriaDAL.BuscarPorId(pId);
        }
        /// <summary>
        /// Busca los datos en la base de datos
        /// </summary>
        /// <param name="pCategoria"></param>
        /// <returns></returns>
        public List<Categoria> Buscar(Categoria pCategoria)
        {
            pCategoria.Estado = 1;
            return CategoriaDAL.Buscar(pCategoria);
        }
    }
}
