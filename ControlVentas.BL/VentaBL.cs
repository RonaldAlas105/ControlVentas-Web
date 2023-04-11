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
    public class VentaBL
    {
        /// <summary>
        /// Guarda los datos en la base de datos
        /// </summary>
        /// <param name="pVenta"></param>
        /// <returns></returns>
        public int Guardar(Venta pVenta)
        {
            pVenta.Estado = 1;
            pVenta.FechaRegistro = DateTime.Now;
            pVenta.Estatus = (byte)Venta.EnumEstatus.CREADO;
            int resultado = VentaDAL.Guardar(pVenta);
            long IdVenta = VentaDAL.ObtenerMaxId();
            if (resultado > 0)
            {
                foreach (var item in pVenta.Detalles)
                {
                    item.IdVenta = IdVenta;
                    item.Estado = 1;
                    resultado = DetalleVentaDAL.Guardar(item);
                    if (resultado > 0)
                    {
                        resultado = InventarioDAL.ActualizarStock(new Inventario { IdProducto = item.IdProducto, Cantidad = item.Cantidad }, InventarioDAL.TipoMovimiento.DISMINUIR);
                    }
                }
            }

            return resultado;
        }/// <summary>
        /// Modifica los registros de la tabla Venta
        /// </summary>
        /// <param name="pVenta"></param>
        /// <returns></returns>
        public int Modificar(Venta pVenta)
        {
            pVenta.Estado = 1;
            return VentaDAL.Modificar(pVenta);
        }
        /// <summary>
        /// Realiza una eliminación logica para la tabla Ventas, 1 es activo, 0 es inactivo
        /// </summary>
        /// <param name="pVenta"></param>
        /// <returns></returns>
        public int Eliminar(Venta pVenta)
        {
            int resultado = 0;
            List<DetalleVenta> detalles = DetalleVentaDAL.Buscar(new DetalleVenta { IdVenta = pVenta.Id, Estado = 1 });
            foreach (var item in detalles)
            {
                resultado = DetalleVentaDAL.Eliminar(item);
            }
            resultado = VentaDAL.Eliminar(pVenta);
            return resultado;
        }

        /// <summary>
        /// Carga las propiedades virtuales de la tabla Clientes
        /// </summary>
        /// <param name="pLista"></param>
        /// <param name="pAccion"></param>
        public void CargarPropiedadVirtualCliente(List<Venta> pLista, Action<List<Cliente>> pAccion = null)
        {
            //Metodo para cargar los datos de la propiedad virtual de Rol
            if (pLista.Count > 0)
            {
                //Dictionary de Roles
                //byte = Tipo de dasto de la llave primaria de Rol
                //Rol = clase partav guardar los datos de roles
                Dictionary<int, Cliente> diccionarioClientes = new Dictionary<int, Cliente>();

                //Buscar los roles y cargarlos a los usuarios en su7 propiedaad virtual
                foreach (var obj in pLista)
                {
                    //Verificar si existe el Rol en el diccionario
                    if (diccionarioClientes.ContainsKey(obj.IdCliente) == true)
                    {
                        //cargar propiedad virtual desde el diccionario de roles
                        obj.Cliente = diccionarioClientes[obj.IdCliente];
                    }
                    else
                    {
                        //si el rol no existe en el diccionario, se busca en la base de datos y se agrega al diccionario
                        diccionarioClientes.Add(obj.IdCliente, ClienteDAL.BuscarPorId(obj.IdCliente));
                        //cargar propiedad virtual desde el diccionario de roles
                        obj.Cliente = diccionarioClientes[obj.IdCliente];
                    }
                }
                //accion auxiliar para sobrecargar otra propiedad  virtual dentro de la clase 
                if (pAccion != null && diccionarioClientes.Count > 0)
                {
                    pAccion(diccionarioClientes.Select(x => x.Value).ToList());
                }
            }

        }


        public Venta BuscarPorId(int pId)
        {
            Venta objVenta = VentaDAL.BuscarPorId(pId);
            objVenta.Detalles = DetalleVentaDAL.Buscar(new DetalleVenta { IdVenta = objVenta.Id, Estado = 1 });
            new DetalleVentaBL().CargarPropiedadVirtualProducto(objVenta.Detalles);
            return objVenta;
        }
        public List<Venta> Buscar(Venta pVenta)
        {
            pVenta.Estado = 1;
            return VentaDAL.Buscar(pVenta);
        }
    }
}
