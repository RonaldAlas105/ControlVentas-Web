using System;
// Referencias
using System.Data;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace ControlVentas.DAL
{
    public class ComunDB
    {

        // Recuerden hacer la conexion a sus maquinas  lovio bbs 

        //Pc Ronald base de datos BookShops
        //const  string StrConexion = @"Data Source=.;Initial Catalog=BookShop;Integrated Security=True";

        //StrConexion BD Will
        const string StrConexion = @"Data Source=.;Initial Catalog=BookShop;Integrated Security=True";

        //StrConexion BD Alexis
        // const String StrConexion = @"Data Source=.;Initial Catalog=BookShop;Integrated Security=True";

        //Desarrollo online
        //const string StrConexion = @"workstation id=2022ControlVentasLibrary.mssql.somee.com;packet size=4096;user id=Kathe_Samayoa_SQLLogin_1;pwd=3pu4vbxst1;data source=2022ControlVentasLibrary.mssql.somee.com;persist security info=False;initial catalog=2022ControlVentasLibrary";

        //const string StrConexion = @"Data Source=.;Initial Catalog=BookShop;Integrated Security=True";

        /// <summary>
        /// Obtenie la conexion con la base de datos
        /// </summary>
        /// <returns>StrConexion</returns>
        private static SqlConnection ObtenerConexion()
        {
            SqlConnection connection = new SqlConnection(StrConexion);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Obtenie los camandos a ejecutar en la base de datos
        /// </summary>
        /// <returns>Establece la conexion la base de datos</returns>
        public static SqlCommand ObtenerComando()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = ObtenerConexion();
            return command;
        }

        /// <summary>
        /// Ejecuta los camandos en la base de datos
        /// </summary>
        /// <param name="pComando">Es la variable que recoje los comandos a ejecutar</param>
        /// <returns>Realiza uno de los procedimientos CRUD</returns>
        public static int EjecutarComando(SqlCommand pComando)
        {
            int resultado = pComando.ExecuteNonQuery();
            pComando.Connection.Close();
            return resultado;
        }
        /// <summary>
        /// Ejcuta los comando antes leidos en la varible pComando
        /// </summary>
        /// <param name="pComando">Es la variable que recoje los comandos a ejecutar</param>
        /// <returns>Ejecuta el comando antes leido en la variable pComando</returns>
        public static SqlDataReader EjecutarComandoReader(SqlCommand pComando)
        {
            SqlDataReader reader = pComando.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
        public static DateTime ObtenerFechaIni(DateTime pFecha)
        {
            return new DateTime(pFecha.Year, pFecha.Month, pFecha.Day, 0, 0, 0);
        }
        public static DateTime ObtenerFechaFin(DateTime pFecha)
        {
            return new DateTime(pFecha.Year, pFecha.Month, pFecha.Day, 23, 59, 59);
        }
       

    }
}

