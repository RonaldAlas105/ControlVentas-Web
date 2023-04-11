// Referencias
using ControlVentas.EN;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControlVentas.DAL
{
    public class MarcaDAL
    {
        #region Metodos Crear, Modificar y Eliminar
        /// <summary>
        /// Guarda una marca en la base de datos.
        /// </summary>
        /// <param name="pMarca">Objeto de tipo Marca a guardar.</param>
        /// <returns>El número de filas afectadas.</returns>
        public static int Guardar(Marca pMarca)
        {
            string consulta = @"INSERT INTO Marcas(Nombre, Estado) 
                                VALUES (@Nombre, @Estado)";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Nombre", pMarca.Nombre);
            comando.Parameters.AddWithValue("@Estado", pMarca.Estado);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        /// Modifica una marca en la base de datos.
        /// </summary>
        /// <param name="pMarca">La marca a modificar.</param>
        /// <returns>El número de filas afectadas.</returns>
        public static int Modificar(Marca pMarca)
        {
            string consulta = @"UPDATE Marcas
                                SET Nombre = @Nombre
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Nombre", pMarca.Nombre);

            comando.Parameters.AddWithValue("@Id", pMarca.Id);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        /// Elimina una marca de la base de datos.
        /// </summary>
        /// <param name="pMarca">La marca a eliminar.</param>
        /// <returns>El número de filas afectadas.</returns>

        public static int Eliminar(Marca pMarca)
        {
            // Eliminacion logica
            string consulta = @"UPDATE Marcas SET Estado = 0 WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pMarca.Id);

            return ComunDB.EjecutarComando(comando);
        }
        #endregion
        /// <summary>
        /// Busca una Marca por su Id.
        /// </summary>
        /// <param name="pId">Id de la Marca a buscar.</param>
        /// <returns>Objeto de tipo Marca si se encuentra, o null si no se encuentra.</returns>
        #region Metodos de busqueda
        public static Marca BuscarPorId(int pId)
        {
            string consulta = @"SELECT Id, Nombre, Estado 
                                FROM Marcas
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Marca objMarca = new Marca();
            while (reader.Read())
            {
                objMarca.Id = reader.GetInt32(0);
                objMarca.Nombre = reader.GetString(1);
                objMarca.Estado = reader.GetByte(2);
            }
            return objMarca;
        }
        /// <summary>
        /// Busca una lista de Marcas aplicando filtros avanzados.
        /// </summary>
        /// <param name="pMarca">Objeto de tipo Marca con los filtros a aplicar. Los campos nulos o vacíos no se tendrán en cuenta.</param>
        /// <returns>Lista de objetos de tipo Marca que cumplen con los filtros especificados.</returns>

        // Metodo de busqueda avanzada
        public static List<Marca> Buscar(Marca pMarca)
        {
            byte Contador = 0;

            string consulta = @"SELECT TOP 50 Id, Nombre, Estado
                                FROM Marcas";
            string whereSQL = "";

            SqlCommand comando = ComunDB.ObtenerComando();

            // Defininir los filtros WHERE de la consulta
            if (!string.IsNullOrWhiteSpace(pMarca.Nombre))
            {
                if (Contador > 0)
                {
                    whereSQL += " AND ";
                }
                Contador++;
                whereSQL += " Nombre LIKE @Nombre ";
                comando.Parameters.AddWithValue("@Nombre", "%" + pMarca.Nombre + "%");
            }

            if (pMarca.Estado > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " Estado = @Estado ";
                comando.Parameters.AddWithValue("@Estado", pMarca.Estado);
            }

            // Comprobar que existen filtros agregados al WHERE
            if (whereSQL.Trim().Length > 0)
            {
                whereSQL = " WHERE " + whereSQL;
            }
            // Concatenar consulta SQL completa
            comando.CommandText = consulta + whereSQL;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            List<Marca> lista = new List<Marca>();
            while (reader.Read())
            {
                Marca objMarca = new Marca();
                objMarca.Id = reader.GetInt32(0);
                objMarca.Nombre = reader.GetString(1);
                objMarca.Estado = reader.GetByte(2);
                lista.Add(objMarca);
            }
            return lista;
        }
        #endregion
    }
}
