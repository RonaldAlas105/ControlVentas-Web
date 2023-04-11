// Referencias
using ControlVentas.EN;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControlVentas.DAL
{
    public class RolDAL
    {
        #region Metodos Crear, Modificar y Eliminar
        /// <summary>
        /// Inserta un nuevo registro con el nombre y el estado especificados en la tabla 'Roles'.
        /// </summary>
        /// <param name="pRol">Un objeto Rol que contiene el nombre y el estado que se va a insertar.</param>
        /// <returns>Un valor entero que indica el número de filas afectadas.</returns>

        public static int Guardar(Rol pRol)
        {
            string consulta = @"INSERT INTO Roles(Nombre, Estado) 
                                VALUES (@Nombre, @Estado)";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Nombre", pRol.Nombre);
            comando.Parameters.AddWithValue("@Estado", pRol.Estado);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        ///Actualiza un registro existente en la tabla 'Roles' con el Id especificado, configurando su nombre con el nuevo nombre especificado en el objeto Rol.
        /// </summary>
        /// <param name="pRol">Un objeto Rol que contiene el nuevo nombre e Id del registro que se actualizará.</param>
        /// <returns>Un valor entero que indica el número de filas afectadas.</returns>
        public static int Modificar(Rol pRol)
        {
            string consulta = @"UPDATE Roles
                                SET Nombre = @Nombre
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Nombre", pRol.Nombre);

            comando.Parameters.AddWithValue("@Id", pRol.Id);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        ///Actualiza un registro existente en la tabla 'Roles' con el Id especificado, configurando su estado en 0, eliminándolo efectivamente.
        /// </summary>
        /// <param name="pRol">Un objeto Rol que contiene el Id del registro que se eliminará</param>
        /// <returns>Un valor entero que indica el número de filas afectadas.</returns>
        public static int Eliminar(Rol pRol)
        {
            // Eliminacion logica
            string consulta = @"UPDATE Roles SET Estado = 0 WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pRol.Id);

            return ComunDB.EjecutarComando(comando);
        }
        #endregion
        /// <summary>
        /// Busca un rol en la tabla "Roles" por su ID.
        /// </summary>
        /// <param name="pId">ID del rol a buscar.</param>
        /// <returns>Un objeto "Rol" que corresponde al registro encontrado en la base de datos.</returns>
        #region Metodos de busqueda
        public static Rol BuscarPorId(int pId)
        {
            string consulta = @"SELECT Id, Nombre, Estado 
                                FROM Roles
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Rol objRol = new Rol();
            while (reader.Read())
            {
                objRol.Id = reader.GetByte(0);
                objRol.Nombre = reader.GetString(1);
                objRol.Estado = reader.GetByte(2);
            }
            return objRol;
        }
        /// <summary>
        /// Busca roles en la tabla "Roles" con filtros opcionales por nombre y estado.
        /// </summary>
        /// <param name="pRol">Objeto "Rol" con los filtros de búsqueda.</param>
        /// <returns>Una lista de objetos "Rol" que corresponden a los registros encontrados en la base de datos.</returns>
        // Metodo de busqueda avanzada
        public static List<Rol> Buscar(Rol pRol)
        {
            byte Contador = 0;

            string consulta = @"SELECT TOP 50 Id, Nombre, Estado
                                FROM Roles";
            string whereSQL = "";

            SqlCommand comando = ComunDB.ObtenerComando();

            // Defininir los filtros WHERE de la consulta
            if (!string.IsNullOrWhiteSpace(pRol.Nombre))
            {
                if (Contador > 0)
                {
                    whereSQL += " AND ";
                }
                Contador++;
                whereSQL += " Nombre LIKE @Nombre ";
                comando.Parameters.AddWithValue("@Nombre", "%" + pRol.Nombre + "%");
            }

            if (pRol.Estado > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " Estado = @Estado ";
                comando.Parameters.AddWithValue("@Estado", pRol.Estado);
            }

            // Comprobar que existen filtros agregados al WHERE
            if (whereSQL.Trim().Length > 0)
            {
                whereSQL = " WHERE " + whereSQL;
            }
            // Concatenar consulta SQL completa
            comando.CommandText = consulta + whereSQL;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            List<Rol> lista = new List<Rol>();
            while (reader.Read())
            {
                Rol objRol = new Rol();
                objRol.Id = reader.GetByte(0);
                objRol.Nombre = reader.GetString(1);
                objRol.Estado = reader.GetByte(2);
                lista.Add(objRol);
            }
            return lista;
        }
        #endregion
    }
}
