// Referencias
using ControlVentas.EN;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControlVentas.DAL
{
    public class DetallesCompraDAL
    {
        #region Metodos Crear, Modificar y Eliminar
        public static int Guardar(DetallesCompra pDetallesCompra)
        {
            string consulta = @"INSERT INTO DetallesCompra(IdProducto, IdCompra, Precio, Cantidad, SubTotal, Estado) 
                                VALUES (@IdProducto, @IdCompra, @Precio, @Cantidad, @SubTotal, @Estado)";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@IdProducto", pDetallesCompra.IdProducto);
            comando.Parameters.AddWithValue("@IdCompra", pDetallesCompra.IdCompra);
            comando.Parameters.AddWithValue("@Precio", pDetallesCompra.Precio);
            comando.Parameters.AddWithValue("@Cantidad", pDetallesCompra.Cantidad);
            comando.Parameters.AddWithValue("@SubTotal", pDetallesCompra.SubTotal);
            comando.Parameters.AddWithValue("@Estado", pDetallesCompra.Estado);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Modificar(DetallesCompra pDetallesCompra)
        {
            string consulta = @"UPDATE DetallesCompra
                                SET Estado = @Estado 
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@IdProducto", pDetallesCompra.IdProducto);
            comando.Parameters.AddWithValue("@IdCompra", pDetallesCompra.IdCompra);
            comando.Parameters.AddWithValue("@Precio", pDetallesCompra.Precio);
            comando.Parameters.AddWithValue("@Estado", pDetallesCompra.Estado);
            comando.Parameters.AddWithValue("@Id", pDetallesCompra.Id);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Eliminar(DetallesCompra pDetallesCompra)
        {
            // Eliminacion logica
            string consulta = @"UPDATE DetallesCompra SET Estado = 0 WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pDetallesCompra.Id);

            return ComunDB.EjecutarComando(comando);
        }
        #endregion

        #region Metodos de busqueda
        public static DetallesCompra BuscarPorId(long pId)
        {
            string consulta = @"SELECT Id, IdProducto, IdCompra, Precio, Cantidad, SubTotal, Estado 
                                FROM DetallesCompra
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            DetallesCompra objdetalle = new DetallesCompra();
            while (reader.Read())
            {
                objdetalle.Id = reader.GetInt64(0);
                objdetalle.IdProducto = reader.GetInt32(1);
                objdetalle.IdCompra = reader.GetInt64(2);
                objdetalle.Precio = reader.GetDecimal(3);
                objdetalle.Cantidad = reader.GetInt32(4);
                objdetalle.SubTotal = reader.GetDecimal(5);
                objdetalle.Estado = reader.GetByte(6);
            }
            return objdetalle;
        }

        // Metodo de busqueda avanzada
        public static List<DetallesCompra> Buscar(DetallesCompra pDetallesCompra)
        {
            byte Contador = 0;

            string consulta = @"SELECT TOP 50 Id, IdProducto, IdCompra, Precio, Cantidad, SubTotal, Estado 
                                FROM DetallesCompra";
            string whereSQL = "";

            SqlCommand comando = ComunDB.ObtenerComando();

            // Defininir los filtros WHERE de la consulta
            if (pDetallesCompra.IdProducto > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " IdProducto = @IdProducto ";
                comando.Parameters.AddWithValue("@IdProducto", pDetallesCompra.IdProducto);
            }

            if (pDetallesCompra.IdCompra > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " IdCompra = @IdCompra ";
                comando.Parameters.AddWithValue("@IdCompra", pDetallesCompra.IdCompra);
            }

            if (pDetallesCompra.Estado > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " Estado = @Estado ";
                comando.Parameters.AddWithValue("@Estado", pDetallesCompra.Estado);
            }

            // Comprobar que existen filtros agregados al WHERE
            if (whereSQL.Trim().Length > 0)
            {
                whereSQL = " WHERE " + whereSQL;
            }
            // Definir consulta SQL Completa
            comando.CommandText = consulta + whereSQL;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            List<DetallesCompra> lista = new List<DetallesCompra>();
            while (reader.Read())
            {
                DetallesCompra objdetalle = new DetallesCompra();
                objdetalle.Id = reader.GetInt64(0);
                objdetalle.IdProducto = reader.GetInt32(1);
                objdetalle.IdCompra = reader.GetInt64(2);
                objdetalle.Precio = reader.GetDecimal(3);
                objdetalle.Cantidad = reader.GetInt32(4);
                objdetalle.SubTotal = reader.GetDecimal(5);
                objdetalle.Estado = reader.GetByte(6);
                lista.Add(objdetalle);
            }
            return lista;
        }
        #endregion
    }
}
