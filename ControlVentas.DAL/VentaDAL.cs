// Referencias
using ControlVentas.EN;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControlVentas.DAL
{
    /// <summary>
    /// Método que guarda una nueva venta en la base de datos.
    /// </summary>
    /// <param name="pVenta">Objeto Venta que representa la venta a guardar.</param>
    /// <returns>El número de filas afectadas en la base de datos.</returns>
    public class VentaDAL
    {
        #region Metodos Crear, Modificar y Eliminar
        public static int Guardar(Venta pVenta)
        {
            string consulta = @"INSERT INTO Ventas(IdUsuario, IdCliente, FechaRegistro,Pago , Estatus, Estado) 
                                VALUES (@IdUsuario, @IdCliente, @FechaRegistro,@Pago ,@Estatus, @Estado)";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@IdUsuario", pVenta.IdUsuario);
            comando.Parameters.AddWithValue("@IdCliente", pVenta.IdCliente);
            comando.Parameters.AddWithValue("@FechaRegistro", pVenta.FechaRegistro);
            comando.Parameters.AddWithValue("@Pago", pVenta.Pago);
            comando.Parameters.AddWithValue("@Estatus", pVenta.Estatus);
            comando.Parameters.AddWithValue("@Estado", pVenta.Estado);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        /// Método que modifica una venta existente en la base de datos.
        /// </summary>
        /// <param name="pVenta">Objeto Venta que representa la venta a modificar.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public static int Modificar(Venta pVenta)
        {
            string consulta = @"UPDATE Ventas
                                SET Estatus = @Estatus, Estado = @Estado 
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Estatus", pVenta.Estatus);
            comando.Parameters.AddWithValue("@Estado", pVenta.Estado);
            comando.Parameters.AddWithValue("@Id", pVenta.Id);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        /// Método que elimina una venta existente en la base de datos.
        /// </summary>
        /// <param name="pVenta">Objeto Venta que representa la venta a eliminar.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public static int Eliminar(Venta pVenta)
        {
            // Eliminacion logica
            string consulta = @"UPDATE Ventas SET Estado = 0 WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pVenta.Id);

            return ComunDB.EjecutarComando(comando);
        }
        #endregion
        /// <summary>
        /// Obtiene el valor máximo de la columna Id en la tabla Ventas.
        /// </summary>
        /// <returns>El valor máximo de la columna Id.</returns>
        #region Metodos de busqueda
        public static long ObtenerMaxId()
        {
            string consulta = @"SELECT MAX(Id) FROM Ventas";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            if (reader.Read())
            {
                return reader.GetInt64(0);
            }
            return 0;
        }
        /// <summary>
        /// Busca una venta por su Id.
        /// </summary>
        /// <param name="pId">El Id de la venta a buscar.</param>
        /// <returns>Un objeto Venta que representa la venta encontrada, o null si no se encontró ninguna venta con el Id especificado.</returns>
        public static Venta BuscarPorId(int pId)
        {
            string consulta = @"SELECT Id, IdUsuario, IdCliente, FechaRegistro,Pago, Estatus, Estado 
                                FROM Ventas
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Venta objRol = new Venta();
            while (reader.Read())
            {
                objRol.Id = reader.GetInt64(0);
                objRol.IdUsuario = reader.GetInt32(1);
                objRol.IdCliente = reader.GetInt32(2);
                objRol.FechaRegistro = reader.GetDateTime(3);
                objRol.Pago = reader.GetString(4);
                objRol.Estatus = reader.GetByte(5);
                objRol.Estado = reader.GetByte(6);
            }
            return objRol;
        }
        /// <summary>
        /// Busca ventas según criterios específicos.
        /// </summary>
        /// <param name="pVenta">Un objeto Venta que contiene los criterios de búsqueda.</param>
        /// <returns>Una lista de objetos Venta que coinciden con los criterios de búsqueda.</returns>
        // Metodo de busqueda avanzada
        public static List<Venta> Buscar(Venta pVenta)
        {
            byte Contador = 0;

            string consulta = @"SELECT TOP 50 Id, IdUsuario, IdCliente, FechaRegistro,Pago, Estatus, Estado 
                                FROM Ventas";
            string whereSQL = "";

            SqlCommand comando = ComunDB.ObtenerComando();

            // Defininir los filtros WHERE de la consulta
            if (pVenta.IdUsuario > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " IdUsuario = @IdUsuario ";
                comando.Parameters.AddWithValue("@IdUsuario", pVenta.IdUsuario);
            }
            if (pVenta.IdCliente > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += " AND ";
                }
                Contador++;
                whereSQL += " IdCliente LIKE @IdCliente ";
                comando.Parameters.AddWithValue("@IdCliente", "%" + pVenta.IdCliente + "%");
            }

            if (pVenta.FechaRegistro != null && pVenta.FechaRegistro != DateTime.MinValue)
            {
                if (Contador > 0)
                {
                    whereSQL += " AND ";
                }
                Contador++;
                whereSQL += " FechaRegistro BETWEEN  @FechaRegistroIni AND @FechaRegistroFin ";
                comando.Parameters.AddWithValue("@FechaRegistroIni", ComunDB.ObtenerFechaIni(pVenta.FechaRegistro));
                comando.Parameters.AddWithValue("@FechaRegistroFin", ComunDB.ObtenerFechaFin(pVenta.FechaRegistro));
            }

            if (pVenta.Estatus > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " Estatus = @Estatus ";
                comando.Parameters.AddWithValue("@Estatus", pVenta.Estatus);
            }

            if (pVenta.Estado > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " Estado = @Estado ";
                comando.Parameters.AddWithValue("@Estado", pVenta.Estado);
            }

            // Comprobar que existen filtros agregados al WHERE
            if (whereSQL.Trim().Length > 0)
            {
                whereSQL = " WHERE " + whereSQL;
            }
            // Definir consulta SQL Completa
            comando.CommandText = consulta + whereSQL;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            List<Venta> lista = new List<Venta>();
            while (reader.Read())
            {
                Venta objRol = new Venta();
                objRol.Id = reader.GetInt64(0);
                objRol.IdUsuario = reader.GetInt32(1);
                objRol.IdCliente = reader.GetInt32(2);
                objRol.FechaRegistro = reader.GetDateTime(3);
                objRol.Pago = reader.GetString(4);
                objRol.Estatus = reader.GetByte(5);
                objRol.Estado = reader.GetByte(6);
                lista.Add(objRol);
            }
            return lista;
        }
        #endregion


    }
}