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
    public class CompraBL
    {
        /// <summary>
        /// Guarda los datos en la base de datos
        /// </summary>
        /// <param name="pCompra"></param>
        /// <returns></returns>
        public int Guardar(Compra pCompra)
        {
            pCompra.Estado = 1;
            pCompra.FechaRegistro = DateTime.Now;
            pCompra.Estatus = (byte)Compra.EnumEstatus.CREADO;
            int resultado = CompraDAL.Guardar(pCompra);
            long IdCompra = CompraDAL.ObtenerMaxId();
            if (resultado > 0)
            {
                foreach (var item in pCompra.Detalles)
                {
                    item.IdCompra = IdCompra;
                    item.Estado = 1;
                    resultado = DetallesCompraDAL.Guardar(item);
                    if (resultado > 0)
                    {
                        resultado = InventarioDAL.ActualizarStock(new Inventario { IdProducto = item.IdProducto, Cantidad = item.Cantidad }, InventarioDAL.TipoMovimiento.AUMENTAR);
                    }
                }
            }

            return resultado;
        }
        /// <summary>
        /// Modifica los datos en la base de datos
        /// </summary>
        /// <param name="pCompra"></param>
        /// <returns></returns>
        public int Modificar(Compra pCompra)
        {
            pCompra.Estado = 1;
            return CompraDAL.Modificar(pCompra);
        }
        /// <summary>
        /// Elimina los datos en la base de datos
        /// </summary>
        /// <param name="pCompra"></param>
        /// <returns></returns>
        public int Eliminar(Compra pCompra)
        {
            int resultado = 0;
            List<DetallesCompra> detalles = DetallesCompraDAL.Buscar(new DetallesCompra { IdCompra = pCompra.Id, Estado = 1 });
            foreach (var item in detalles)
            {
                resultado = DetallesCompraDAL.Eliminar(item);
            }
            resultado = CompraDAL.Eliminar(pCompra);
            return resultado;
        }

        public void CargarPropiedadVirtualProveedor(List<Compra> pLista, Action<List<Proveedor>> pAccion = null)
        {
            //Metodo para cargar los datos de la propiedad virtual de Rol
            if (pLista.Count > 0)
            {
                //Dictionary de Roles
                //byte = Tipo de dasto de la llave primaria de Rol
                //Rol = clase partav guardar los datos de roles
                Dictionary<int, Proveedor> diccionarioProveedores = new Dictionary<int, Proveedor>();

                //Buscar los roles y cargarlos a los usuarios en su7 propiedaad virtual
                foreach (var obj in pLista)
                {
                    //Verificar si existe el Rol en el diccionario
                    if (diccionarioProveedores.ContainsKey(obj.IdProveedor) == true)
                    {
                        //cargar propiedad virtual desde el diccionario de roles
                        obj.Proveedor = diccionarioProveedores[obj.IdProveedor];
                    }
                    else
                    {
                        //si el rol no existe en el diccionario, se busca en la base de datos y se agrega al diccionario
                        diccionarioProveedores.Add(obj.IdProveedor, ProveedorDAL.BuscarPorId(obj.IdProveedor));
                        //cargar propiedad virtual desde el diccionario de roles
                        obj.Proveedor = diccionarioProveedores[obj.IdProveedor];
                    }
                }
                //accion auxiliar para sobrecargar otra propiedad  virtual dentro de la clase 
                if (pAccion != null && diccionarioProveedores.Count > 0)
                {
                    pAccion(diccionarioProveedores.Select(x => x.Value).ToList());
                }
            }

        }

        /// <summary>
        /// Realiza una busca por medio del Id los datos en la base de datos
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public Compra BuscarPorId(int pId)
        {
            Compra objCompra = CompraDAL.BuscarPorId(pId);
            objCompra.Detalles = DetallesCompraDAL.Buscar(new DetallesCompra { IdCompra = objCompra.Id, Estado = 1 });
            new DetalleCompraBL().CargarPropiedadVirtualProducto(objCompra.Detalles);
            return objCompra;
        }
        /// <summary>
        /// Busca los datos en la base de datos
        /// </summary>
        /// <param name="pCompra"></param>
        /// <returns></returns>
        public List<Compra> Buscar(Compra pCompra)
        {
            pCompra.Estado = 1;
            return CompraDAL.Buscar(pCompra);
        }
    }
}
