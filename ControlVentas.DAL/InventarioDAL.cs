using ControlVentas.EN;
using System.Collections.Generic;
//Referencias
using System.Data.SqlClient;
using System.Linq;

namespace ControlVentas.DAL
{
    public class InventarioDAL
    {
        //Enums
        public enum TipoMovimiento
        {
            AUMENTAR = 1,
            DISMINUIR = 2
        }
        //Metodos CRUD
        /// <summary>
        /// Guarda un registro en la tabla Inventario.
        /// </summary>
        /// <param name="pInventario">Objeto de tipo Inventario con los datos del registro a guardar.</param>
        /// <returns>Un entero que indica la cantidad de registros afectados.</returns>
        public static int Guardar(Inventario pInventario)
        {
            string consulta = @"INSERT INTO Inventario(IdProducto, PrecioCompra, PrecioVenta, FechaHoraIngreso, Cantidad, Estado)
                                VALUES(@IdProducto, @PrecioCompra, @PrecioVenta, @FechaHoraIngreso, @Cantidad,@Estado)";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@IdProducto", pInventario.IdProducto);
            comando.Parameters.AddWithValue("@PrecioCompra", pInventario.PrecioCompra);
            comando.Parameters.AddWithValue("@PrecioVenta", pInventario.PrecioVenta);
            comando.Parameters.AddWithValue("@FechaHoraIngreso", pInventario.FechaHoraIngreso);
            comando.Parameters.AddWithValue("@Cantidad", pInventario.Cantidad);
            comando.Parameters.AddWithValue("@Estado", pInventario.Estado);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        /// Modifica un registro en la tabla Inventario.
        /// </summary>
        /// <param name="pInventario">Objeto de tipo Inventario con los datos del registro a modificar.</param>
        /// <returns>Un entero que indica la cantidad de registros afectados.</returns>

        public static int Modificar(Inventario pInventario)
        {
            string consulta = @"UPDATE Inventario
                                SET IdProducto = @IdProducto PrecioCompra=@PrecioCompra, PrecioVenta=@PrecioVenta,FechaHoraIngreso=@FechaHoraIngreso, Cantidad = @Cantidad
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@IdProducto", pInventario.IdProducto);
            comando.Parameters.AddWithValue("@PrecioCompra", pInventario.PrecioCompra);
            comando.Parameters.AddWithValue("@PrecioVenta", pInventario.PrecioVenta);
            comando.Parameters.AddWithValue("@FechaHoraIngreso", pInventario.FechaHoraIngreso);
            comando.Parameters.AddWithValue("@Cantidad", pInventario.Cantidad);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        /// Elimina un registro en la tabla Inventario.
        /// </summary>
        /// <param name="pInventario">Objeto de tipo Inventario con los datos del registro a eliminar.</param>
        /// <returns>Un entero que indica la cantidad de registros afectados.</returns>
        public static int Eliminar(Inventario pInventario)
        {
            //ELIMINACION LOGICA
            string consulta = @"UPDATE Inventario
                                SET Estado = 0
                                WHERE Id = @Id";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pInventario.Id);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        /// Actualiza el stock de un producto en la tabla Inventario.
        /// </summary>
        /// <param name="pInventario">Objeto de tipo Inventario con los datos del registro a actualizar.</param>
        /// <param name="pTipoMovimiento">Enumeración que indica el tipo de movimiento a realizar.</param>
        /// <returns>Un entero que indica la cantidad de registros afectados.</returns>
        public static int ActualizarStock(Inventario pInventario, TipoMovimiento pTipoMovimiento)
        {
            //ELIMINACION LOGICA
            Inventario objInventario = Buscar(new Inventario { IdProducto = pInventario.IdProducto, Estado = 1 }).FirstOrDefault();
            if (pTipoMovimiento == TipoMovimiento.AUMENTAR)
            {
                pInventario.Cantidad = objInventario.Cantidad + pInventario.Cantidad;
            }
            if (pTipoMovimiento == TipoMovimiento.DISMINUIR)
            {
                pInventario.Cantidad = objInventario.Cantidad - pInventario.Cantidad;
            }
            string consulta = @"UPDATE Inventario
                                SET Cantidad = @Cantidad 
                                WHERE IdProducto = @IdProducto";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Cantidad", pInventario.Cantidad);
            comando.Parameters.AddWithValue("@IdProducto", pInventario.IdProducto);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        /// Busca un registro en la tabla Inventario por su Id.
        /// </summary>
        /// <param name="pId">Entero que indica el Id del registro a buscar.</param>
        /// <returns>Un objeto de tipo Inventario con los datos del registro encontrado.</returns>
        //Busqueda de registro por Id
        public static Inventario BuscarPorId(int pId)
        {
            string consulta = @"SELECT Id, IdProducto, PrecioCompra,PrecioVenta, FechaHoraIngreso,Cantidad,Estado
                                FROM Inventario
                                WHERE Id = @Id";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Inventario objInventario = new Inventario();
            while (reader.Read())
            {
                objInventario.Id = reader.GetInt32(0);
                objInventario.IdProducto = reader.GetInt32(1);
                objInventario.PrecioCompra = reader.GetDecimal(2);
                objInventario.PrecioVenta = reader.GetDecimal(3);
                objInventario.FechaHoraIngreso = reader.GetDateTime(4);
                objInventario.Cantidad = reader.GetInt32(5);
                objInventario.Estado = reader.GetByte(6);
            }
            return objInventario;
        }

        /// <summary>
        /// Busca registros en la tabla Inventario con filtros.
        /// </summary>
        /// <param name="pInventario">Objeto de tipo Inventario con los filtros de búsqueda.</param>
        /// <returns>Una lista de objetos de tipo Inventario con los registros encontrados.</returns>
        //Busqueda avanzada con filtros
        public static List<Inventario> Buscar(Inventario pInventario)
        {
            byte contador = 0;

            //Definir consulta Sql
            string consulta = @"SELECT TOP 50 Id,IdProducto, PrecioCompra,PrecioVenta, FechaHoraIngreso,Cantidad, Estado
                                  FROM Inventario ";
            string whereSQL = "";

            SqlCommand comando = ComunDB.ObtenerComando();

            //Definir filtros where de la consulta;
            if (pInventario.IdProducto > 0)
            {
                if (contador > 0)
                {
                    whereSQL += "AND";
                }
                contador++;
                whereSQL += " IdProducto = @IdProducto ";
                comando.Parameters.AddWithValue("@IdProducto", pInventario.IdProducto);
            }

            if (pInventario.Cantidad > 0)
            {
                if (contador > 0)
                {
                    whereSQL += "AND";
                }
                contador++;
                whereSQL += " Cantidad = @Cantidad ";
                comando.Parameters.AddWithValue("@Cantidad", pInventario.Cantidad);
            }

            if (pInventario.Estado > 0)
            {
                if (contador > 0)
                {
                    whereSQL += "AND";
                }
                contador++;
                whereSQL += " Estado = @Estado ";
                comando.Parameters.AddWithValue("@Estado", pInventario.Estado);
            }
            //Definir que existen los filtros
            if (whereSQL.Trim().Length > 0)
            {
                whereSQL = " WHERE " + whereSQL;
            }
            //Definir consulta SQL completa
            comando.CommandText = consulta + whereSQL;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            List<Inventario> lista = new List<Inventario>();
            while (reader.Read())
            {
                Inventario objInventario = new Inventario();
                objInventario.Id = reader.GetInt32(0);
                objInventario.IdProducto = reader.GetInt32(1);
                objInventario.PrecioCompra = reader.GetDecimal(2);
                objInventario.PrecioVenta = reader.GetDecimal(3);
                objInventario.FechaHoraIngreso = reader.GetDateTime(4);
                objInventario.Cantidad = reader.GetInt32(5);
                objInventario.Estado = reader.GetByte(6);

                lista.Add(objInventario);
            }
            comando.Connection.Dispose();
            return lista;
        }
    }
}
