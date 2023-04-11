// Referencias
using ControlVentas.EN;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControlVentas.DAL
{
    public class CompraDAL
    {
        #region Metodos Crear, Modificar y Eliminar
        public static int Guardar(Compra pCompra)
        {
            string consulta = @"INSERT INTO Compras(IdUsuario, IdProveedor, FechaRegistro, Estatus, Estado) 
                                VALUES (@IdUsuario, @IdProveedor, @FechaHora, @Total, @Estado)";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@IdUsuario", pCompra.IdUsuario);
            comando.Parameters.AddWithValue("@IdProveedor", pCompra.IdProveedor);
            comando.Parameters.AddWithValue("@FechaHora", pCompra.FechaRegistro);
            comando.Parameters.AddWithValue("@Total", pCompra.Estatus);
            comando.Parameters.AddWithValue("@Estado", pCompra.Estado);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Modificar(Compra pCompra)
        {
            string consulta = @"UPDATE Compras
                                SET Estatus = @Estatus, Estado = @Estado 
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Estatus", pCompra.Estatus);
            comando.Parameters.AddWithValue("@Estado", pCompra.Estado);
            comando.Parameters.AddWithValue("@Id", pCompra.Id);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Eliminar(Compra pCompra)
        {
            // Eliminacion logica
            string consulta = @"UPDATE Compras SET Estado = 0 WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pCompra.Id);

            return ComunDB.EjecutarComando(comando);
        }
        #endregion

        #region Metodos de busqueda
        public static long ObtenerMaxId()
        {
            string consulta = @"SELECT MAX(Id) FROM Compras";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            if (reader.Read())
            {
                return reader.GetInt64(0);
            }
            return 0;
        }
        public static Compra BuscarPorId(int pId)
        {
            string consulta = @"SELECT Id, IdUsuario, IdProveedor, FechaRegistro,Estatus, Estado 
                                FROM Compras
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Compra objCompra = new Compra();
            while (reader.Read())
            {
                objCompra.Id = reader.GetInt64(0);
                objCompra.IdUsuario = reader.GetInt32(1);
                objCompra.IdProveedor = reader.GetInt32(2);
                objCompra.FechaRegistro = reader.GetDateTime(3);
                objCompra.Estatus = reader.GetByte(4);
                objCompra.Estado = reader.GetByte(5);
            }
            return objCompra;
        }

        // Metodo de busqueda avanzada
        public static List<Compra> Buscar(Compra pCompra)
        {
            byte Contador = 0;

            string consulta = @"SELECT TOP 50 Id, IdUsuario, IdProveedor, FechaRegistro, Estatus, Estado 
                                FROM Compras";
            string whereSQL = "";

            SqlCommand comando = ComunDB.ObtenerComando();

            // Defininir los filtros WHERE de la consulta
            if (pCompra.IdUsuario > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " IdUsuario = @IdUsuario ";
                comando.Parameters.AddWithValue("@IdUsuario", pCompra.IdUsuario);
            }
            if (pCompra.IdProveedor > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += " AND ";
                }
                Contador++;
                whereSQL += " IdProveedor LIKE @IdProveedor ";
                comando.Parameters.AddWithValue("@IdProveedor", "%" + pCompra.IdProveedor + "%");
            }

            if (pCompra.FechaRegistro != null && pCompra.FechaRegistro != DateTime.MinValue)
            {
                if (Contador > 0)
                {
                    whereSQL += " AND ";
                }
                Contador++;
                whereSQL += " FechaRegistro BETWEEN  @FechaRegistroIni AND @FechaRegistroFin ";
                comando.Parameters.AddWithValue("@FechaRegistroIni", ComunDB.ObtenerFechaIni(pCompra.FechaRegistro));
                comando.Parameters.AddWithValue("@FechaRegistroFin", ComunDB.ObtenerFechaFin(pCompra.FechaRegistro));
            }

            if (pCompra.Estatus > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " Estatus = @Estatus ";
                comando.Parameters.AddWithValue("@Estatus", pCompra.Estatus);
            }

            if (pCompra.Estado > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " Estado = @Estado ";
                comando.Parameters.AddWithValue("@Estado", pCompra.Estado);
            }

            // Comprobar que existen filtros agregados al WHERE
            if (whereSQL.Trim().Length > 0)
            {
                whereSQL = " WHERE " + whereSQL;
            }
            // Definir consulta SQL Completa
            comando.CommandText = consulta + whereSQL;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            List<Compra> lista = new List<Compra>();
            while (reader.Read())
            {
                Compra objCompra = new Compra();
                objCompra.Id = reader.GetInt64(0);
                objCompra.IdUsuario = reader.GetInt32(1);
                objCompra.IdProveedor = reader.GetInt32(2);
                objCompra.FechaRegistro = reader.GetDateTime(3);
                objCompra.Estatus = reader.GetByte(4);
                objCompra.Estado = reader.GetByte(5);
                lista.Add(objCompra);
            }
            return lista;
        }
        #endregion
    }
}
