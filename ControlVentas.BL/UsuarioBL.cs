using ControlVentas.DAL;
//Referencias
using ControlVentas.EN;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlVentas.BL
{
    /// <summary>
    ///  Contiene los metos de la capa DAL y establece conexion con la base de datos para hacer los procesos CRUD y logeo
    /// </summary>
    public class UsuarioBL
    {
        /// <summary>
        /// Guarda los datos en la base de datos
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public int Guardar(Usuario pUsuario)
        {
            pUsuario.Estado = 1;
            return UsuarioDAL.Guardar(pUsuario);
        }
        /// <summary>
        /// Modifica los datos en la base de datos
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public int Modificar(Usuario pUsuario)
        {
            pUsuario.Estado = 1;
            return UsuarioDAL.Modificar(pUsuario);
        }
        /// <summary>
        /// Elimina los datos en la base de datos
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public int Eliminar(Usuario pUsuario)
        {
            return UsuarioDAL.Eliminar(pUsuario);
        }
        /// <summary>
        /// Realiza una busca por medio del Id los datos en la base de datos
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public Usuario BuscarPorId(int pId)
        {
            return UsuarioDAL.BuscarPorId(pId);

        }
        /// <summary>
        /// Busca los datos en la base de datos
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<Usuario> Buscar(Usuario pUsuario)
        {
            pUsuario.Estado = 1;
            return UsuarioDAL.Buscar(pUsuario);
        }
        /// <summary>
        /// Contiene los registros para acceder con las credenciales en el logeo
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Usuario IniciarSesion(Usuario pUsuario)
        {
            return UsuarioDAL.IniciarSesion(pUsuario);
        }

        public void CargarPropiedadVirtualRol(List<Usuario> pLista, Action<List<Rol>> pAccion = null)
        {
            //Metodos para cargar los datos de la propiedad virtual de Rol y Marca
            if (pLista.Count > 0)
            {
                //Dictionary de Roles
                //byte = Tipo de dasto de la llave primaria de Rol
                //Rol = clase partav guardar los datos de categorias
                Dictionary<byte, Rol> diccionarioRols = new Dictionary<byte, Rol>();

                //Buscar los categorias y cargarlos a los usuarios en su7 propiedaad virtual
                foreach (var obj in pLista)
                {
                    //Verificar si existe el Rol en el diccionario
                    if (diccionarioRols.ContainsKey(obj.IdRol) == true)
                    {
                        //cargar propiedad virtual desde el diccionario de categorias
                        obj.Rol = diccionarioRols[obj.IdRol];
                    }
                    else
                    {
                        //si el rol no existe en el diccionario, se busca en la base de datos y se agrega al diccionario
                        diccionarioRols.Add(obj.IdRol, RolDAL.BuscarPorId(obj.IdRol));
                        //cargar propiedad virtual desde el diccionario de roles
                        obj.Rol = diccionarioRols[obj.IdRol];
                    }
                }
                //accion auxiliar para sobrecargar otra propiedad  virtual dentro de la clase 
                if (pAccion != null && diccionarioRols.Count > 0)
                {
                    pAccion(diccionarioRols.Select(x => x.Value).ToList());
                }
            }

        }

    }
}