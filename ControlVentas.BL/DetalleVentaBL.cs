using ControlVentas.DAL;
// Referencias
using ControlVentas.EN;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlVentas.BL
{
    /// <summary>
    /// Contiene los metos de la capa DAL y establece conexion con la base de datos para hacer los procesos CRUD
    /// </summary>
    public class DetalleVentaBL
    {
        /// <summary>
        /// Guarda los datos en la base de datos
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        public int Guardar(Producto pProducto)
        {
            pProducto.Estado = 1;
            return ProductoDAL.Guardar(pProducto);
        }
        /// <summary>
        /// Modifica los datos en la base de datos
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        public int Modificar(Producto pProducto)
        {
            pProducto.Estado = 1;
            return ProductoDAL.Modificar(pProducto);
        }
        /// <summary>
        /// Elimina los datos en la base de datos
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        public int Eliminar(Producto pProducto)
        {
            return ProductoDAL.Eliminar(pProducto);
        }
        /// <summary>
        /// Realiza una busca por medio del Id los datos en la base de datos
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public Producto BuscarPorId(int pId)
        {
            return ProductoDAL.BuscarPorId(pId);
        }
        /// <summary>
        /// Busca los datos en la base de datos
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        public List<Producto> Buscar(Producto pProducto)
        {
            pProducto.Estado = 1;
            return ProductoDAL.Buscar(pProducto);
        }

        public void CargarPropiedadVirtualProducto(List<DetalleVenta> pLista, Action<List<Producto>> pAccion = null)
        {
            // Metodo para cargar los datos de la propiedad virtual de Producto
            if (pLista.Count > 0)
            {
                // Dictionary de Productoes
                // byte = Tipo de dato de la llave primaria de Producto
                // Producto = Clase para guardar los datos de Productoes
                Dictionary<int, Producto> diccionarioProductos = new Dictionary<int, Producto>();

                // Buscar los Productoes y cargarlos a los usuarios en su propiedad virtual
                foreach (var obj in pLista)
                {
                    // Verificar si existe el Producto en el diccionario
                    if (diccionarioProductos.ContainsKey(obj.IdProducto) == true)
                    {
                        // Cargar propiedad virtual desde el diccionario de Productoes
                        obj.Producto = diccionarioProductos[obj.IdProducto];
                    }
                    else
                    {
                        // Si el Producto no existe en el diccionario, se busca en la base de datos y se agrega al diccionario
                        diccionarioProductos.Add(obj.IdProducto, ProductoDAL.BuscarPorId(obj.IdProducto));
                        // Cargar propiedad virtual desde el diccionario de Productoes
                        obj.Producto = diccionarioProductos[obj.IdProducto];
                    }
                }

                // Accion auxiliar para sobrecargar otro propiedad virtual dentro de la clase Producto
                if (pAccion != null && diccionarioProductos.Count > 0)
                {
                    pAccion(diccionarioProductos.Select(x => x.Value).ToList());
                }
            }
        }

    }
}
