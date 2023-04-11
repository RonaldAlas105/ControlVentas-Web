using ControlVentas.DAL;
//Referencias
using ControlVentas.EN;
using System.Collections.Generic;

namespace ControlVentas.BL
{
    /// <summary>
    /// Contiene los metos de la capa DAL y establece conexion con la base de datos para hacer los procesos CRUD
    /// </summary>
    public class ProveedorBL
    {
        /// <summary>
        /// Guarda los datos en la base de datos
        /// </summary>
        /// <param name="pProveedor"></param>
        /// <returns></returns>
        public int Guardar(Proveedor pProveedor)
        {
            pProveedor.Estado = 1;
            return ProveedorDAL.Guardar(pProveedor);
        }
        /// <summary>
        /// Modifica los datos en la base de datos
        /// </summary>
        /// <param name="pProveedor"></param>
        /// <returns></returns>
        public int Modificar(Proveedor pProveedor)
        {
            pProveedor.Estado = 1;
            return ProveedorDAL.Modificar(pProveedor);
        }
        /// <summary>
        /// Elimina los datos en la base de datos
        /// </summary>
        /// <param name="pProveedor"></param>
        /// <returns></returns>
        public int Eliminar(Proveedor pProveedor)
        {
            return ProveedorDAL.Eliminar(pProveedor);
        }
        /// <summary>
        /// Realiza una busca por medio del Id los datos en la base de datos
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public Proveedor BuscarPorId(int pId)
        {
            return ProveedorDAL.BuscarPorId(pId);
        }
        /// <summary>
        /// Busca los datos en la base de datos
        /// </summary>
        /// <param name="pProveedor"></param>
        /// <returns></returns>
        public List<Proveedor> Buscar(Proveedor pProveedor)
        {
            pProveedor.Estado = 1;
            return ProveedorDAL.Buscar(pProveedor);
        }
    }
}
