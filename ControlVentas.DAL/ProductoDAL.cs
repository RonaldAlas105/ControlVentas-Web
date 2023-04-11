using ControlVentas.EN;
using System;
using System.Collections.Generic;
using System.Data;
//Referencias
using System.Data.SqlClient;

namespace ControlVentas.DAL
{
    public class ProductoDAL
    {
        #region Metodos Crear, Modificar y Eliminar

        /// <summary>
        /// Guarda un nuevo registro de producto en la base de datos.
        /// </summary>
        /// <param name="pProducto">El objeto Producto que se desea guardar.</param>
        /// <returns>El número de filas afectadas por la inserción del nuevo registro.</returns>
        public static int Guardar(Producto pProducto)
        {
            string consulta = @"INSERT INTO Productos(IdCategoria, IdMarca, Nombre, Descripcion, Img, Estado) 
        VALUES(@IdCategoria, @IdMarca, @Nombre, @Descripcion, @Img, @Estado)";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@IdCategoria", pProducto.IdCategoria);
            comando.Parameters.AddWithValue("@IdMarca", pProducto.IdMarca);
            comando.Parameters.AddWithValue("@Nombre", pProducto.Nombre);
            comando.Parameters.AddWithValue("@Descripcion", pProducto.Descripcion);
            comando.Parameters.AddWithValue("@Img", pProducto.Img);
            comando.Parameters.AddWithValue("@Estado", pProducto.Estado);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        /// Modifica un producto en la base de datos.
        /// </summary>
        /// <param name="pProducto">El producto a modificar.</param>
        /// <returns>El número de filas afectadas en la base de datos.</returns>
        public static int Modificar(Producto pProducto)
        {
            string consulta = @"UPDATE Productos 
                                SET IdCategoria = @IdCategoria,IdMarca = @IdMarca,Nombre = @Nombre,Img = @Img,Descripcion = @Descripcion
                                WHERE Id = @Id";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@IdCategoria", pProducto.IdCategoria);
            comando.Parameters.AddWithValue("@IdMarca", pProducto.IdMarca);
            comando.Parameters.AddWithValue("@Nombre", pProducto.Nombre);
            comando.Parameters.AddWithValue("@Descripcion", pProducto.Descripcion);
            comando.Parameters.AddWithValue("@Img", pProducto.Img);
            comando.Parameters.AddWithValue("@Id", pProducto.Id);

            return ComunDB.EjecutarComando(comando);
        }
        /// <summary>
        /// Realiza una eliminación lógica del producto en la base de datos, cambiando su estado a "0".
        /// </summary>
        /// <param name="pProducto">Producto a eliminar.</param>
        /// <returns>Número de filas afectadas en la base de datos.</returns>
        public static int Eliminar(Producto pProducto)
        {
            // Eliminacion logica
            string consulta = @"UPDATE Productos
                                SET Estado = 0
                                WHERE Id = @Id";

            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pProducto.Id);

            return ComunDB.EjecutarComando(comando);
        }
        #endregion

        #region Metodos de busqueda
        /// <summary>
        /// Obtiene el valor máximo de Id en la tabla de Productos.
        /// </summary>
        /// <returns>El valor máximo de Id en la tabla de Productos o cero si no se encontró ningún valor.</returns>
        public static int ObtenerMaxId()
        {
            string consulta = @"SELECT MAX(Id) FROM Productos";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            if (reader.Read())
            {
                return reader.GetInt32(0);
            }
            return 0;
        }
        /// <summary>
        /// Busca un producto por su ID en la base de datos.
        /// </summary>
        /// <param name="pId">El ID del producto que se desea buscar.</param>
        /// <returns>Un objeto Producto con la información del producto encontrado.</returns>
        public static Producto BuscarPorId(int pId)
        {
            string consulta = @"SELECT Id, IdCategoria, IdMarca, Nombre, Descripcion, Estado
                                FROM Productos 
                                WHERE Id = @Id";
            SqlCommand comando = ComunDB.ObtenerComando();
            comando.CommandText = consulta;
            comando.Parameters.AddWithValue("@Id", pId);

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            Producto objProducto = new Producto();
            while (reader.Read())
            {
                objProducto.Id = reader.GetInt32(0);
                objProducto.IdCategoria = reader.GetByte(1);
                objProducto.IdMarca = reader.GetInt32(2);
                objProducto.Nombre = reader.GetString(3);
                objProducto.Descripcion = reader.GetString(4);
                objProducto.Estado = reader.GetByte(5);
            }

            return objProducto;
        }
        /// <summary>
        /// Busca productos en la base de datos según ciertos criterios de búsqueda.
        /// </summary>
        /// <param name="pProducto">Objeto Producto con los criterios de búsqueda.</param>
        /// <returns>Lista de objetos Producto que coinciden con los criterios de búsqueda.</returns>
        /// 
        //Metodo de busqueda avanzada
        public static List<Producto> Buscar(Producto pProducto)
        {
            byte Contador = 0;

            //Definir consulta SQL base
            string consulta = @"SELECT TOP 50 Id, IdCategoria, IdMarca, Nombre, Descripcion,Img, Estado
                        FROM Productos";
            string whereSQL = " ";

            SqlCommand comando = ComunDB.ObtenerComando();

            // Definir filtros WHERE de la consulta
            if (pProducto.IdCategoria > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND";
                }
                Contador++;
                whereSQL += "IdCategoria = @IdCategoria";
                comando.Parameters.AddWithValue("@IdCategoria", pProducto.IdCategoria);
            }
            if (pProducto.IdMarca > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += "AND";
                }
                Contador++;
                whereSQL += "IdMarca = @IdMarca";
                comando.Parameters.AddWithValue("@IdMarca", pProducto.IdMarca);
            }
            if (!string.IsNullOrWhiteSpace(pProducto.Nombre))
            {
                if (Contador > 0)
                {
                    whereSQL += " AND ";
                }
                Contador++;
                whereSQL += " Nombre Like @Nombre ";
                comando.Parameters.AddWithValue("@Nombre", "%" + pProducto.Nombre + "%");
            }
            if (!string.IsNullOrWhiteSpace(pProducto.Descripcion))
            {
                if (Contador > 0)
                {
                    whereSQL += " AND ";
                }
                Contador++;
                whereSQL += " Descripcion Like @Descripcion ";
                comando.Parameters.AddWithValue("@Descripcion", "%" + pProducto.Descripcion + "%");
            }

            if (pProducto.Estado > 0)
            {
                if (Contador > 0)
                {
                    whereSQL += " AND ";
                }
                Contador++;
                whereSQL += " Estado = @Estado ";
                comando.Parameters.AddWithValue("@Estado", pProducto.Estado);
            }

            // Comprobar que existen filtros
            if (whereSQL.Trim().Length > 0)
            {
                whereSQL = " WHERE " + whereSQL;
            }

            // Definir consulta SQL completa
            comando.CommandText = consulta + whereSQL;

            SqlDataReader reader = ComunDB.EjecutarComandoReader(comando);
            List<Producto> lista = new List<Producto>();
            while (reader.Read())
            {
                Producto objProducto = new Producto();
                objProducto.Id = reader.GetInt32(0);
                objProducto.IdCategoria = reader.GetByte(1);
                objProducto.IdMarca = reader.GetInt32(2);
                objProducto.Nombre = reader.GetString(3);
                objProducto.Descripcion = reader.GetString(4);
                objProducto.Img = (byte[])reader["Img"];
                objProducto.Estado = reader.GetByte(6);
                lista.Add(objProducto);
            }
            return lista;
        }
        #endregion
    }
}
