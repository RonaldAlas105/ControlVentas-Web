using ControlVentas.DAL;
// Referencias
using ControlVentas.EN;
using System.Collections.Generic;

namespace ControlVentas.BL
{
    /// <summary>
    /// Contiene los metos de la capa DAL y establece conexion con la base de datos para hacer los procesos CRUD
    /// </summary>
    public class MarcaBL
    {
        /// <summary>
        /// Guarda los datos en la base de datos
        /// </summary>
        /// <param name="pMarca"></param>
        /// <returns></returns>
        public int Guardar(Marca pMarca)
        {
            pMarca.Estado = 1;
            return MarcaDAL.Guardar(pMarca);
        }
        /// <summary>
        /// Modifica los datos en la base de datos
        /// </summary>
        /// <param name="pMarca"></param>
        /// <returns></returns>
        public int Modificar(Marca pMarca)
        {
            return MarcaDAL.Modificar(pMarca);
        }
        /// <summary>
        /// Elimina los datos en la base de datos
        /// </summary>
        /// <param name="pMarca"></param>
        /// <returns></returns>
        public int Eliminar(Marca pMarca)
        {
            return MarcaDAL.Eliminar(pMarca);
        }
        /// <summary>
        /// Realiza una busca por medio del Id los datos en la base de datos
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public Marca BuscarPorId(int pId)
        {
            return MarcaDAL.BuscarPorId(pId);
        }
        /// <summary>
        /// Busca los datos en la base de datos
        /// </summary>
        /// <param name="pMarca"></param>
        /// <returns></returns>
        public List<Marca> Buscar(Marca pMarca)
        {
            pMarca.Estado = 1;
            return MarcaDAL.Buscar(pMarca);
        }
    }
}
