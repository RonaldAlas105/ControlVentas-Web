using ControlVentas.DAL;
// Referencias
using ControlVentas.EN;
using System.Collections.Generic;

namespace ControlVentas.BL
{
    /// <summary>
    /// Contiene los metos de la capa DAL y establece conexion con la base de datos para hacer los procesos CRUD
    /// </summary>
    public class RolBL
    {
        /// <summary>
        /// Guarda los datos en la base de datos
        /// </summary>
        /// <param name="pRol"></param>
        /// <returns></returns>
        public int Guardar(Rol pRol)
        {
            pRol.Estado = 1;
            return RolDAL.Guardar(pRol);
        }
        /// <summary>
        /// Modifica los datos en la base de datos
        /// </summary>
        /// <param name="pRol"></param>
        /// <returns></returns>
        public int Modificar(Rol pRol)
        {
            return RolDAL.Modificar(pRol);
        }
        /// <summary>
        /// Elimina los datos en la base de datos
        /// </summary>
        /// <param name="pRol"></param>
        /// <returns></returns>
        public int Eliminar(Rol pRol)
        {
            return RolDAL.Eliminar(pRol);
        }
        /// <summary>
        /// Realiza una busca por medio del Id los datos en la base de datos
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public Rol BuscarPorId(int pId)
        {
            return RolDAL.BuscarPorId(pId);
        }
        /// <summary>
        /// Busca los datos en la base de datos
        /// </summary>
        /// <param name="pRol"></param>
        /// <returns></returns>
        public List<Rol> Buscar(Rol pRol)
        {
            pRol.Estado = 1;
            return RolDAL.Buscar(pRol);
        }
    }
}
