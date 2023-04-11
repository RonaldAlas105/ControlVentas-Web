using ControlVentas.DAL;
// Referencias
using ControlVentas.EN;
using System.Collections.Generic;

namespace ControlVentas.BL
{
    /// <summary>
    /// Contiene los metos de la capa DAL y establece conexion con la base de datos para hacer los procesos CRUD
    /// </summary>
    public class ClienteBL
    {
        /// <summary>
        /// Guarda los datos en la base de datos
        /// </summary>
        /// <param name="pCliente"></param>
        /// <returns></returns>
        public int Guardar(Cliente pCliente)
        {
            pCliente.Estado = 1;
            return ClienteDAL.Guardar(pCliente);
        }
        /// <summary>
        ///  Modifica los datos en la base de datos
        /// </summary>
        /// <param name="pCliente"></param>
        /// <returns></returns>
        public int Modificar(Cliente pCliente)
        {
            pCliente.Estado = 1;
            return ClienteDAL.Modificar(pCliente);
        }
        /// <summary>
        /// Elimina los datos en la base de datos
        /// </summary>
        /// <param name="pCliente"></param>
        /// <returns></returns>
        public int Eliminar(Cliente pCliente)
        {
            return ClienteDAL.Eliminar(pCliente);
        }
        /// <summary>
        /// Realiza una busca por medio del Id los datos
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public Cliente BuscarPorId(int pId)
        {
            return ClienteDAL.BuscarPorId(pId);
        }
        /// <summary>
        /// Busca los datos en la base de datos
        /// </summary>
        /// <param name="pCliente"></param>
        /// <returns></returns>
        public List<Cliente> Buscar(Cliente pCliente)
        {
            pCliente.Estado = 1;
            return ClienteDAL.Buscar(pCliente);
        }
    }
}
