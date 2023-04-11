// Referencias
using ControlVentas.EN;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControlVentas.DAL
{
    public class ClienteDAL
    {
        #region Metodos Crear, Modificar y Eliminar
        public static int Guardar(Cliente pCliente)
        {
            string consulta = @"INSERT INTO Clientes(NombreCompleto, DUI, Telefono, Direccion, Estado) 
                                VALUES (@NombreCompleto, @DUI, @Telefono, @Direccion, @Estado)";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@NombreCompleto", pCliente.NombreCompleto);
            comando.Parameters.AddWithValue("@DUI", pCliente.DUI);
            comando.Parameters.AddWithValue("@Telefono", pCliente.Telefono);
            comando.Parameters.AddWithValue("@Direccion", pCliente.Direccion);
            comando.Parameters.AddWithValue("@Estado", pCliente.Estado);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Modificar(Cliente pCliente)
        {
            string consulta = @"UPDATE Clientes
                                SET NombreCompleto = @NombreCompleto,  DUI = @DUI, Telefono = @Telefono, Direccion = @Direccion
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@NombreCompleto", pCliente.NombreCompleto);
            comando.Parameters.AddWithValue("@DUI", pCliente.DUI);
            comando.Parameters.AddWithValue("@Telefono", pCliente.Telefono);
            comando.Parameters.AddWithValue("@Direccion", pCliente.Direccion);
            comando.Parameters.AddWithValue("@Id", pCliente.Id);

            return ComunDB.EjecutarComando(comando);
        }
        public static int Eliminar(Cliente pCliente)
        {
            // Eliminacion logica
            string consulta = @"UPDATE Clientes SET Estado = 0 WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pCliente.Id);

            return ComunDB.EjecutarComando(comando);
        }
        #endregion

        #region Metodos de busqueda
        public static Cliente BuscarPorId(int pId)
        {
            string consulta = @"SELECT Id,NombreCompleto, DUI, Telefono, Direccion, Estado
                                FROM Clientes
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Cliente objCliente = new Cliente();
            while (reader.Read())
            {
                objCliente.Id = reader.GetInt32(0);
                objCliente.NombreCompleto = reader.GetString(1);
                objCliente.DUI = reader.GetString(2);
                objCliente.Telefono = reader.GetString(3);
                objCliente.Direccion = reader.GetString(4);
                objCliente.Estado = reader.GetByte(5);
            }
            return objCliente;
        }

        // Metodo de busqueda avanzada
        public static List<Cliente> Buscar(Cliente pCliente)
        {
            byte Contador = 0;

            string consulta = @"SELECT TOP 50 Id, NombreCompleto, DUI, Telefono, Direccion, Estado
                                FROM Clientes";
            string whereSQL = "";

            SqlCommand comando = ComunDB.ObtenerComando();

            // Defininir los filtros WHERE de la consulta
            if (!string.IsNullOrWhiteSpace(pCliente.NombreCompleto))
            {
                if (Contador > 0)
                {
                    whereSQL += " AND ";
                }
                Contador++;
                whereSQL += " NombreCompleto LIKE @NombreCompleto ";
                comando.Parameters.AddWithValue("@NombreCompleto", "%" + pCliente.NombreCompleto + "%");
            }


            // Defininir los filtros WHERE de la consulta
            if (!string.IsNullOrWhiteSpace(pCliente.DUI))
            {
                if (Contador > 0)
                {
                    whereSQL += " AND ";
                }
                Contador++;
                whereSQL += " DUI LIKE @DUI ";
                comando.Parameters.AddWithValue("@DUI", "%" + pCliente.DUI + "%");
            }
            if (pCliente.Estado > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND ";
                }
                Contador++;
                whereSQL += " Estado = @Estado ";
                comando.Parameters.AddWithValue("@Estado", pCliente.Estado);
            }



            // Comprobar que existen filtros agregados al WHERE
            if (whereSQL.Trim().Length > 0)
            {
                whereSQL = " WHERE " + whereSQL;
            }
            // Concatenar consulta SQL completa
            comando.CommandText = consulta + whereSQL;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            List<Cliente> lista = new List<Cliente>();
            while (reader.Read())
            {
                Cliente objCliente = new Cliente();
                objCliente.Id = reader.GetInt32(0);
                objCliente.NombreCompleto = reader.GetString(1);
                objCliente.DUI = reader.GetString(2);
                objCliente.Telefono = reader.GetString(3);
                objCliente.Direccion = reader.GetString(4);
                objCliente.Estado = reader.GetByte(5);


                lista.Add(objCliente);
            }
            return lista;
        }
        #endregion
    }
}
