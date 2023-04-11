// Referencias
using ControlVentas.EN;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControlVentas.DAL
{
    public class DetalleVentaDAL
    {
        #region Metodos Crear, Modificar y Eliminar
        public static int Guardar(DetalleVenta pDetalleVenta)
        {
            string consulta = @"INSERT INTO DetallesVenta(IdVenta, IdProducto, Precio, Cantidad, SubTotal, Estado) 
                                VALUES (@IdVenta, @IdProducto, @Precio, @Cantidad, @SubTotal, @Estado)";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@IdVenta", pDetalleVenta.IdVenta);
            comando.Parameters.AddWithValue("@IdProducto", pDetalleVenta.IdProducto);
            comando.Parameters.AddWithValue("@Precio", pDetalleVenta.Precio);
            comando.Parameters.AddWithValue("@Cantidad", pDetalleVenta.Cantidad);
            comando.Parameters.AddWithValue("@SubTotal", pDetalleVenta.SubTotal);
            comando.Parameters.AddWithValue("@Estado", pDetalleVenta.Estado);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Modificar(DetalleVenta pDetalleVenta)
        {
            string consulta = @"UPDATE DetallesVenta
                                SET Estado = @Estado 
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@IdVenta", pDetalleVenta.IdVenta);
            comando.Parameters.AddWithValue("@IdProducto", pDetalleVenta.IdProducto);
            comando.Parameters.AddWithValue("@Precio", pDetalleVenta.Precio);
            comando.Parameters.AddWithValue("@Estado", pDetalleVenta.Estado);
            comando.Parameters.AddWithValue("@Id", pDetalleVenta.Id);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Eliminar(DetalleVenta pDetalleVenta)
        {
            // Eliminacion logica
            string consulta = @"UPDATE DetallesVenta SET Estado = 0 WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pDetalleVenta.Id);

            return ComunDB.EjecutarComando(comando);
        }
        #endregion

        #region Metodos de busqueda
        public static DetalleVenta BuscarPorId(int pId)
        {
            string consulta = @"SELECT Id, IdVenta, IdProducto, Precio, Cantidad, SubTotal, Estado 
                                FROM DetallesVenta
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            DetalleVenta objRol = new DetalleVenta();
            while (reader.Read())
            {
                objRol.Id = reader.GetInt64(0);
                objRol.IdVenta = reader.GetInt32(1);
                objRol.IdProducto = reader.GetInt32(2);
                objRol.Precio = reader.GetDecimal(3);
                objRol.Cantidad = reader.GetInt32(4);
                objRol.SubTotal = reader.GetDecimal(5);
                objRol.Estado = reader.GetByte(6);
            }
            return objRol;
        }

        // Metodo de busqueda avanzada
        public static List<DetalleVenta> Buscar(DetalleVenta pDetalleVenta)
        {
            byte Contador = 0;

            string consulta = @"SELECT TOP 50 Id, IdVenta, IdProducto, Precio, Cantidad, SubTotal, Estado 
                                FROM DetallesVenta";
            string whereSQL = "";

            SqlCommand comando = ComunDB.ObtenerComando();

            // Defininir los filtros WHERE de la consulta
            if (pDetalleVenta.IdVenta > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " IdVenta = @IdVenta ";
                comando.Parameters.AddWithValue("@IdVenta", pDetalleVenta.IdVenta);
            }

            if (pDetalleVenta.IdProducto > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " IdProducto = @IdProducto ";
                comando.Parameters.AddWithValue("@IdProducto", pDetalleVenta.IdProducto);
            }

            if (pDetalleVenta.Estado > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " Estado = @Estado ";
                comando.Parameters.AddWithValue("@Estado", pDetalleVenta.Estado);
            }

            // Comprobar que existen filtros agregados al WHERE
            if (whereSQL.Trim().Length > 0)
            {
                whereSQL = " WHERE " + whereSQL;
            }
            // Definir consulta SQL Completa
            comando.CommandText = consulta + whereSQL;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            List<DetalleVenta> lista = new List<DetalleVenta>();
            while (reader.Read())
            {
                DetalleVenta objRol = new DetalleVenta();
                objRol.Id = reader.GetInt64(0);
                objRol.IdVenta = reader.GetInt64(1);
                objRol.IdProducto = reader.GetInt32(2);
                objRol.Precio = reader.GetDecimal(3);
                objRol.Cantidad = reader.GetInt32(4);
                objRol.SubTotal = reader.GetDecimal(5);
                objRol.Estado = reader.GetByte(6);
                lista.Add(objRol);
            }
            return lista;
        }
        #endregion
    }
}
