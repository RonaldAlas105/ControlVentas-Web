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
    public class ProductoBL
    {
        /// <summary>
        /// Guarda los datos en la base de datos
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        public int Guardar(Producto pProducto)
        {
            int resultado = ProductoDAL.Guardar(pProducto);
            if (resultado > 0)
            {
                int IdProducto = ProductoDAL.ObtenerMaxId();
                pProducto.Inventario.IdProducto = IdProducto;
                pProducto.Inventario.Cantidad = 0;
                pProducto.Inventario.Estado = 1;
                pProducto.Inventario.FechaHoraIngreso = DateTime.Now;
                pProducto.Inventario.PrecioCompra = 0;
                resultado = InventarioDAL.Guardar(pProducto.Inventario);
            }
            return resultado;
        }
        /// <summary>
        /// Modifica los datos en la base de datos
        /// </summary>
        /// <param name="pProducto"></param>
        /// <returns></returns>
        public int Modificar(Producto pProducto)
        {
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
        /// Realiza una busca por medio del Id los datos
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
        public void CargarPropiedadVirtualCategoria(List<Producto> pLista, Action<List<Categoria>> pAccion = null)
        {
            //Metodos para cargar los datos de la propiedad virtual de Categoria y Marca
            if (pLista.Count > 0)
            {
                //Dictionary de Categoriaes
                //byte = Tipo de dasto de la llave primaria de Categoria
                //Categoria = clase partav guardar los datos de categorias
                Dictionary<byte, Categoria> diccionarioCategorias = new Dictionary<byte, Categoria>();

                //Buscar los categorias y cargarlos a los usuarios en su7 propiedaad virtual
                foreach (var obj in pLista)
                {
                    //Verificar si existe el Categoria en el diccionario
                    if (diccionarioCategorias.ContainsKey(obj.IdCategoria) == true)
                    {
                        //cargar propiedad virtual desde el diccionario de categorias
                        obj.Categoria = diccionarioCategorias[obj.IdCategoria];
                    }
                    else
                    {
                        //si el rol no existe en el diccionario, se busca en la base de datos y se agrega al diccionario
                        diccionarioCategorias.Add(obj.IdCategoria, CategoriaDAL.BuscarPorId(obj.IdCategoria));
                        //cargar propiedad virtual desde el diccionario de roles
                        obj.Categoria = diccionarioCategorias[obj.IdCategoria];
                    }
                }
                //accion auxiliar para sobrecargar otra propiedad  virtual dentro de la clase 
                if (pAccion != null && diccionarioCategorias.Count > 0)
                {
                    pAccion(diccionarioCategorias.Select(x => x.Value).ToList());
                }
            }

        }
        public void CargarPropiedadVirtualMarca(List<Producto> pLista, Action<List<Marca>> pAccion = null)
        {
            //Metodo para cargar los datos de la propiedad virtual de Rol
            if (pLista.Count > 0)
            {
                //Dictionary de Roles
                //byte = Tipo de dasto de la llave primaria de Rol
                //Rol = clase partav guardar los datos de roles
                Dictionary<int, Marca> diccionarioMarcas = new Dictionary<int, Marca>();

                //Buscar los roles y cargarlos a los usuarios en su7 propiedaad virtual
                foreach (var obj in pLista)
                {
                    //Verificar si existe el Rol en el diccionario
                    if (diccionarioMarcas.ContainsKey(obj.IdMarca) == true)
                    {
                        //cargar propiedad virtual desde el diccionario de roles
                        obj.Marca = diccionarioMarcas[obj.IdMarca];
                    }
                    else
                    {
                        //si el rol no existe en el diccionario, se busca en la base de datos y se agrega al diccionario
                        diccionarioMarcas.Add(obj.IdMarca, MarcaDAL.BuscarPorId(obj.IdMarca));
                        //cargar propiedad virtual desde el diccionario de roles
                        obj.Marca = diccionarioMarcas[obj.IdMarca];
                    }
                }
                //accion auxiliar para sobrecargar otra propiedad  virtual dentro de la clase 
                if (pAccion != null && diccionarioMarcas.Count > 0)
                {
                    pAccion(diccionarioMarcas.Select(x => x.Value).ToList());
                }
            }

        }
        public void CargarPropiedadVirtualInventario(List<Producto> pLista)
        {
            //Metodo para cargar los datos de la propiedad virtual de Rol
            if (pLista.Count > 0)
            {

                foreach (var obj in pLista)
                {

                    obj.Inventario = InventarioDAL.Buscar(new Inventario { IdProducto = obj.Id, Estado = 1 }).FirstOrDefault();
                    if (obj.Inventario == null)
                    {
                        obj.Inventario = new Inventario();
                    }

                }

            }

        }
    }
}

