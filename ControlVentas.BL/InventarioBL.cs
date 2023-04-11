using ControlVentas.DAL;
//Referencias
using ControlVentas.EN;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlVentas.BL
{
    /// <summary>
    ///  Contiene los metos de la capa DAL y establece conexion con la base de datos para hacer los procesos CRUD
    /// </summary>
    public class InventarioBL
    {
        /// <summary>
        /// Guarda los datos en la base de datos
        /// </summary>
        /// <param name="pInventario"></param>
        /// <returns></returns>
        public int Guardar(Inventario pInventario)
        {
            pInventario.Estado = 1;
            return InventarioDAL.Guardar(pInventario);
        }
        /// <summary>
        /// Modifica los datos en la base de datos
        /// </summary>
        /// <param name="pInventario"></param>
        /// <returns></returns>
        public int Modificar(Inventario pInventario)
        {
            pInventario.Estado = 1;
            return InventarioDAL.Modificar(pInventario);
        }
        /// <summary>
        /// Elimina los datos en la base de datos
        /// </summary>
        /// <param name="pInventario"></param>
        /// <returns></returns>
        public int Eliminar(Inventario pInventario)
        {
            return InventarioDAL.Eliminar(pInventario);
        }
        /// <summary>
        /// Realiza una busca por medio del Id los datos en la base de datos
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public Inventario BuscarPorId(int pId)
        {
            return InventarioDAL.BuscarPorId(pId);
        }
        /// <summary>
        /// Busca los datos en la base de datos
        /// </summary>
        /// <param name="pInventario"></param>
        /// <returns></returns>
        public List<Inventario> Buscar(Inventario pInventario)
        {
            pInventario.Estado = 1;
            return InventarioDAL.Buscar(pInventario);
        }
        /// <summary>
        /// Carga los registros  de la tabla Producto
        /// </summary>
        /// <param name="pLista"></param>
        /// <param name="pAccion"></param>
        public void CargarPropiedadVirtualProducto(List<Inventario> pLista, Action<List<Producto>> pAccion = null)
        {
            if (pLista.Count > 0)
            {
                Dictionary<int, Producto> diccionarioProductos = new Dictionary<int, Producto>();

                foreach (var obj in pLista)
                {
                    if (diccionarioProductos.ContainsKey(obj.IdProducto)==true)
                    {
                        obj.Producto = diccionarioProductos[obj.IdProducto];
                    }
                    else
                    {
                        diccionarioProductos.Add(obj.IdProducto, ProductoDAL.BuscarPorId(obj.IdProducto));
                        obj.Producto = diccionarioProductos[obj.IdProducto];
                    }
                }
                if (pAccion != null && diccionarioProductos.Count > 0)
                {
                    pAccion(diccionarioProductos.Select(x => x.Value).ToList());
                }
            }
        }
    }
}
