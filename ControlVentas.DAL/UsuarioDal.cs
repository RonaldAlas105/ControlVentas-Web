using ControlVentas.EN;
using System.Collections.Generic;
//Referencias
using System.Data.SqlClient;

namespace ControlVentas.DAL
{
    public class UsuarioDAL
    {
        /// <summary>
        /// Guarda un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="pUsuario">El objeto Usuario a guardar.</param>
        /// <returns>El número de filas afectadas por la operación de guardado.</returns>
        public static int Guardar(Usuario pUsuario)
        {
            string consulta = @"INSERT INTO Usuarios( IdRol, Nombre, Apellido, NombreUsuario, ClaveUsuario, Estado) 
                                VALUES( @IdRol, @Nombre, @Apellido, @NombreUsuario, @ClaveUsuario, @Estado)";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@IdRol", pUsuario.IdRol);
            comando.Parameters.AddWithValue("@Nombre", pUsuario.Nombre);
            comando.Parameters.AddWithValue("@Apellido", pUsuario.Apellido);
            comando.Parameters.AddWithValue("@NombreUsuario", pUsuario.NombreUsuario);
            comando.Parameters.AddWithValue("@ClaveUsuario", pUsuario.ClaveUsuario);
            comando.Parameters.AddWithValue("@Estado", pUsuario.Estado);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        /// Modifica un usuario existente en la base de datos.
        /// </summary>
        /// <param name="pUsuario">El objeto Usuario con los nuevos datos a guardar.</param>
        /// <returns>El número de filas afectadas por la operación de modificación.</returns>
        public static int Modificar(Usuario pUsuario)
        {
            string consulta = @"UPDATE Usuarios 
                                SET IdRol = @IdRol, Nombre = @Nombre, Apellido = @Apellido,
                                            NombreUsuario = @NombreUsuario
                                WHERE Id = @Id";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;

            comando.Parameters.AddWithValue("@IdRol", pUsuario.IdRol);
            comando.Parameters.AddWithValue("@Nombre", pUsuario.Nombre);
            comando.Parameters.AddWithValue("@Apellido", pUsuario.Apellido);
            comando.Parameters.AddWithValue("@NombreUsuario", pUsuario.NombreUsuario);
            comando.Parameters.AddWithValue("@Id", pUsuario.Id);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        /// Elimina un usuario existente en la base de datos.
        /// </summary>
        /// <param name="pUsuario">El objeto Usuario a eliminar.</param>
        /// <returns>El número de filas afectadas por la operación de eliminación.</returns>
        public static int Eliminar(Usuario pUsuario)
        {
            // Eliminacion logica
            string consulta = @"UPDATE Usuarios
                                SET Estado = 0
                                WHERE Id = @Id";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pUsuario.Id);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        /// Busca un usuario en la base de datos por su ID.
        /// </summary>
        /// <param name="pId">ID del usuario a buscar.</param>
        /// <returns>El objeto Usuario con los datos del usuario encontrado, o un objeto vacío si no se encuentra.</returns>
        //Busqueda de registro por Id
        public static Usuario BuscarPorId(int pId)
        {
            string consulta = @"SELECT Id, IdRol, Nombre, Apellido, NombreUsuario, Estado
                                FROM Usuarios 
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Usuario objUsuario = new Usuario();
            while (reader.Read())
            {
                objUsuario.Id = reader.GetInt32(0);
                objUsuario.IdRol = reader.GetByte(1);
                objUsuario.Nombre = reader.GetString(2);
                objUsuario.Apellido = reader.GetString(3);
                objUsuario.NombreUsuario = reader.GetString(4);
                objUsuario.Estado = reader.GetByte(5);
            }

            return objUsuario;
        }
        /// <summary>
        /// Busca usuarios en la base de datos utilizando filtros avanzados.
        /// </summary>
        /// <param name="pUsuario">Objeto Usuario con los filtros de búsqueda aplicados.</param>
        /// <returns>Una lista de objetos Usuario que cumplen con los filtros de búsqueda.</returns>
        //Busqueda avanzada con filtros
        public static List<Usuario> Buscar(Usuario pUsuario)
        {
            byte contador = 0;

            //Definir consulta SQL base
            string consulta = @"SELECT TOP 50 Id, IdRol, Nombre, Apellido, NombreUsuario,Estado
                                FROM Usuarios";
            string whereSQL = "";

            SqlCommand comando = ComunDB.ObtenerComando();

            // Definir filtros WHERE de la consulta
            if (pUsuario.IdRol > 0)
            {
                if (contador > 0)
                {
                    whereSQL += " AND ";
                }
                contador++;
                whereSQL += " IdRol = @IdRol ";
                comando.Parameters.AddWithValue("@IdRol", pUsuario.IdRol);
            }
            if (!string.IsNullOrWhiteSpace(pUsuario.Nombre))
            {
                if (contador > 0)
                {
                    whereSQL += " AND ";
                }
                contador++;
                whereSQL += " Nombre Like @Nombre ";
                comando.Parameters.AddWithValue("@Nombre", "%" + pUsuario.Nombre + "%");
            }
            if (!string.IsNullOrWhiteSpace(pUsuario.Apellido))
            {
                if (contador > 0)
                {
                    whereSQL += " AND ";
                }
                contador++;
                whereSQL += " Apellido Like @Apellido ";
                comando.Parameters.AddWithValue("@Apellido", "%" + pUsuario.Apellido + "%");
            }

            if (pUsuario.Estado > 0)
            {
                if (contador > 0)
                {
                    whereSQL += " AND ";
                }
                contador++;
                whereSQL += " Estado = @Estado ";
                comando.Parameters.AddWithValue("@Estado", pUsuario.Estado);
            }

            // Comprobar que existen filtros
            if (whereSQL.Trim().Length > 0)
            {
                whereSQL = " WHERE " + whereSQL;
            }

            // Definir consulta SQL completa
            comando.CommandText = consulta + whereSQL;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            List<Usuario> lista = new List<Usuario>();
            while (reader.Read())
            {
                Usuario objUsuario = new Usuario();
                objUsuario.Id = reader.GetInt32(0);
                objUsuario.IdRol = reader.GetByte(1);
                objUsuario.Nombre = reader.GetString(2);
                objUsuario.Apellido = reader.GetString(3);
                objUsuario.NombreUsuario = reader.GetString(4);
                objUsuario.Estado = reader.GetByte(5);
                //objUsuario.Rol = RolDAL.BuscarPorId(objUsuario.IdRol);//forma deficiente para obetener encapsulamiento de nuestra clase EN
                lista.Add(objUsuario);
            }

            comando.Connection.Dispose();
            return lista;
        }
        /// <summary>
        /// Método que se encarga de iniciar sesión de un usuario en la aplicación.
        /// </summary>
        /// <param name="pUsuario">Objeto de tipo Usuario que contiene los datos de inicio de sesión.</param>
        /// <returns>Retorna un objeto de tipo Usuario con los datos de sesión correspondientes.</returns>
        public static Usuario IniciarSesion(Usuario pUsuario)
        {
            string consulta = @"SELECT Id, IdRol, Nombre, Apellido, NombreUsuario, Estado
                                FROM Usuarios 
                                WHERE NombreUsuario = @NombreUsuario AND ClaveUsuario = @ClaveUsuario   AND Estado = 1";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@NombreUsuario", pUsuario.NombreUsuario);
            comando.Parameters.AddWithValue("@ClaveUsuario", pUsuario.ClaveUsuario);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Usuario objUsuario = new Usuario();
            while (reader.Read())
            {

                objUsuario.Id = reader.GetInt32(0);
                objUsuario.IdRol = reader.GetByte(1);
                objUsuario.Nombre = reader.GetString(2);
                objUsuario.Apellido = reader.GetString(3);
                objUsuario.NombreUsuario = reader.GetString(4);
                objUsuario.Estado = reader.GetByte(5);
            }

            return objUsuario;
        }
    }
}
