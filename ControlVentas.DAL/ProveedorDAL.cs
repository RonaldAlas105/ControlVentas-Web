// Referencias
using ControlVentas.EN;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControlVentas.DAL
{
    public class ProveedorDAL
    {

        #region Metodos Crear, Modificar y Eliminar
        /// <summary>
        /// Guarda un objeto Proveedor en la base de datos.
        /// </summary>
        /// <param name="pProveedor">Objeto Proveedor a guardar.</param>
        /// <returns>El número de filas afectadas por la operación de inserción.</returns>
        public static int Guardar(Proveedor pProveedor)
        {
            string consulta = @"INSERT INTO Proveedores(Nombre, Direccion, Telefono, Estado) 
                                VALUES (@Nombre, @Direccion, @Telefono, @Estado)";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Nombre", pProveedor.Nombre);
            comando.Parameters.AddWithValue("@Direccion", pProveedor.Direccion);
            comando.Parameters.AddWithValue("@Telefono", pProveedor.Telefono);
            comando.Parameters.AddWithValue("@Estado", pProveedor.Estado);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        /// Modifica un objeto Proveedor en la base de datos.
        /// </summary>
        /// <param name="pProveedor">Objeto Proveedor con los datos a modificar.</param>
        /// <returns>El número de filas afectadas por la operación de modificación.</returns>
        public static int Modificar(Proveedor pProveedor)
        {
            string consulta = @"UPDATE Proveedores
                                SET Nombre = @Nombre, Direccion= @Direccion,Telefono = @Telefono
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Nombre", pProveedor.Nombre);
            comando.Parameters.AddWithValue("@Direccion", pProveedor.Direccion);
            comando.Parameters.AddWithValue("@Telefono", pProveedor.Telefono);
            comando.Parameters.AddWithValue("@Id", pProveedor.Id);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        /// Elimina un objeto Proveedor de la base de datos.
        /// </summary>
        /// <param name="pProveedor">Objeto Proveedor a eliminar.</param>
        /// <returns>El número de filas afectadas por la operación de eliminación.</returns>
        public static int Eliminar(Proveedor pProveedor)
        {
            // Eliminacion logica
            string consulta = @"UPDATE Proveedores SET Estado = 0 WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pProveedor.Id);

            return ComunDB.EjecutarComando(comando);
        }
        #endregion

        /// <summary>
        /// Busca un proveedor por su Id.
        /// </summary>
        /// <param name="pId">Id del proveedor a buscar.</param>
        /// <returns>Objeto Proveedor si se encuentra, de lo contrario null.</returns>
        #region Metodos de busqueda
        public static Proveedor BuscarPorId(int pId)
        {
            string consulta = @"SELECT Id, Nombre, Direccion,Telefono ,Estado 
                                FROM Proveedores
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Proveedor objProveedor = new Proveedor();
            while (reader.Read())
            {
                objProveedor.Id = reader.GetInt32(0);
                objProveedor.Nombre = reader.GetString(1);
                objProveedor.Direccion = reader.GetString(2);
                objProveedor.Telefono = reader.GetString(3);
                objProveedor.Estado = reader.GetByte(4);
            }
            return objProveedor;
        }
        /// <summary>
        /// Realiza una búsqueda avanzada de proveedores con filtros opcionales.
        /// </summary>
        /// <param name="pProveedor">Objeto Proveedor con los filtros a aplicar.</param>
        /// <returns>Lista de objetos Proveedor que cumplen con los filtros aplicados.</returns>
        // Metodo de busqueda avanzada
        public static List<Proveedor> Buscar(Proveedor pProveedor)
        {
            byte Contador = 0;

            string consulta = @"SELECT TOP 50 Id, Nombre, Direccion, Telefono,Estado
                                FROM Proveedores";
            string whereSQL = "";

            SqlCommand comando = ComunDB.ObtenerComando();

            // Defininir los filtros WHERE de la consulta
            if (!string.IsNullOrWhiteSpace(pProveedor.Nombre))
            {
                if (Contador > 0)
                {
                    whereSQL += " AND ";
                }
                Contador++;
                whereSQL += " Nombre LIKE @Nombre ";
                comando.Parameters.AddWithValue("@Nombre", "%" + pProveedor.Nombre + "%");
            }

            if (pProveedor.Estado > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " Estado = @Estado ";
                comando.Parameters.AddWithValue("@Estado", pProveedor.Estado);
            }

            // Comprobar que existen filtros agregados al WHERE
            if (whereSQL.Trim().Length > 0)
            {
                whereSQL = " WHERE " + whereSQL;
            }
            // Concatenar consulta SQL completa
            comando.CommandText = consulta + whereSQL;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            List<Proveedor> lista = new List<Proveedor>();
            while (reader.Read())
            {
                Proveedor objProveedor = new Proveedor();
                objProveedor.Id = reader.GetInt32(0);
                objProveedor.Nombre = reader.GetString(1);
                objProveedor.Direccion = reader.GetString(2);
                objProveedor.Telefono = reader.GetString(3);
                objProveedor.Estado = reader.GetByte(4);
                lista.Add(objProveedor);
            }
            return lista;
        }
        #endregion
    }
}
