using ControlVentas.BL;
using ControlVentas.DAL;
//Referencias
using ControlVentas.EN;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace ControlVentas.UI.AppWebMVC.Controllers
{

    public class ProductoController : Controller
    {
        static string cadena = "Data Source=.;Initial Catalog=BookShop;Integrated Security=True";
        public ActionResult Index()

        {

            return View();
        }



        [HttpPost]
        public ActionResult Guardar(string Nombre, byte IdCategoria, int IdMarca, string Descripcion, HttpPostedFileBase Img, decimal PrecioCompra, decimal PrecioVenta, int Cantidad)
        {
            string Extesion = Path.GetExtension(Img.FileName);
            MemoryStream ms = new MemoryStream();
            Img.InputStream.CopyTo(ms);
            byte[] data = ms.ToArray();

            using (SqlConnection oconexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Productos(IdCategoria, IdMarca, Nombre, Descripcion, Img, Estado)values(@IdCategoria, @IdMarca, @Nombre, @Descripcion, @Img, @Estado); SELECT SCOPE_IDENTITY()", oconexion);
                cmd.Parameters.AddWithValue("@IdCategoria", IdCategoria);
                cmd.Parameters.AddWithValue("@IdMarca", IdMarca);
                cmd.Parameters.AddWithValue("@Nombre", Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                cmd.Parameters.AddWithValue("@Img", data);
                cmd.Parameters.AddWithValue("@Estado", 1);
                cmd.CommandType = CommandType.Text;
                oconexion.Open();
                int IdProducto = Convert.ToInt32(cmd.ExecuteScalar());

                SqlCommand cmdInventario = new SqlCommand("INSERT INTO Inventario(IdProducto, PrecioCompra, PrecioVenta, FechaHoraIngreso, Cantidad, Estado)values(@IdProducto, @PrecioCompra, @PrecioVenta, @FechaHoraIngreso, @Cantidad, @Estado)", oconexion);
                cmdInventario.Parameters.AddWithValue("@IdProducto", IdProducto);
                cmdInventario.Parameters.AddWithValue("@PrecioCompra", PrecioCompra);
                cmdInventario.Parameters.AddWithValue("@PrecioVenta", PrecioVenta);
                cmdInventario.Parameters.AddWithValue("@FechaHoraIngreso", DateTime.Now);
                cmdInventario.Parameters.AddWithValue("@Cantidad", Cantidad);
                cmdInventario.Parameters.AddWithValue("@Estado", 1);
                cmdInventario.CommandType = CommandType.Text;
                cmdInventario.ExecuteNonQuery();

            }
            TempData["SuccessMessage"] = "Los datos se han guardado correctamente";
            return RedirectToAction("VerProductos", "Producto");
        }



        public ActionResult VerProductos()
        {
            List<Producto> productos = new List<Producto>();

            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("SELECT Productos.*, Inventario.*, Categorias.Nombre as NombreCategoria, Marcas.Nombre as NombreMarca FROM Productos " +
                                                "INNER JOIN Categorias ON Productos.IdCategoria = Categorias.Id " +
                                                "INNER JOIN Marcas ON Productos.IdMarca = Marcas.Id " +
                                                "INNER JOIN Inventario ON Productos.Id = Inventario.IdProducto", conexion);
                conexion.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Producto producto = new Producto();
                    producto.Id = Convert.ToInt32(reader["Id"]);
                    producto.IdCategoria = Convert.ToByte(reader["IdCategoria"]);
                    producto.IdMarca = Convert.ToInt32(reader["IdMarca"]);
                    producto.Nombre = reader["Nombre"].ToString();
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Img = (byte[])reader["Img"];
                    producto.Estado = Convert.ToByte(reader["Estado"]);
                    producto.NombreCategoria = reader["NombreCategoria"].ToString();
                    producto.NombreMarca = reader["NombreMarca"].ToString();
                    producto.PrecioCompra = Convert.ToDecimal(reader["PrecioCompra"]);
                    producto.PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"]);
                    producto.FechaHoraIngreso = Convert.ToDateTime(reader["FechaHoraIngreso"]);
                    producto.Cantidad = Convert.ToInt32(reader["Cantidad"]);
                    productos.Add(producto);
                }
            }
            return View(productos);
        }


        public bool ValidarDatos(Producto pProducto)
        {
            //Metodo para validar todos los campos que son obligatorios para guardaro modificar
            //
            //
            bool resultado = true;
            if (pProducto.IdCategoria == 0)
            {
                resultado = false;
            }
            if (pProducto.IdMarca == 0)
            {
                resultado = false;
            }
            if (string.IsNullOrWhiteSpace(pProducto.Nombre))
            {
                resultado = false;
            }
            if (string.IsNullOrWhiteSpace(pProducto.Descripcion))
            {
                resultado = false;
            }
            return resultado;
        }

        public JsonResult Modificar(Producto pProducto)
        {
            int resultado = 0;
            try
            {
                if (pProducto.Id > 0 && ValidarDatos(pProducto) == true)
                {
                    resultado = new ProductoBL().Modificar(pProducto);
                }
            }
            catch (Exception ex)
            {

                resultado = 0;
            }
            return Json(resultado);
        }
        public JsonResult Eliminar(Producto pProducto)
        {
            int resultado = 0;
            try
            {
                if (pProducto.Id > 0)
                {
                    resultado = new ProductoBL().Eliminar(pProducto);
                }
            }
            catch (Exception ex)
            {

                resultado = 0;
            }
            return Json(resultado);
        }
        public JsonResult BuscarPorId(int pId)
        {
            Producto objProducto = new ProductoBL().BuscarPorId(pId);
            return Json(objProducto, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Buscar(Producto pProducto)
        {
            ProductoBL productoBL = new ProductoBL();
            List<Producto> lista = productoBL.Buscar(pProducto);
            //Ejecutar el metodo para llenar las propiedades virtuales de Producto y saber a que Categoria y Marca pertenece el producto
            productoBL.CargarPropiedadVirtualCategoria(lista);
            productoBL.CargarPropiedadVirtualMarca(lista);
            productoBL.CargarPropiedadVirtualInventario(lista);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerCategorias()
        {
            List<Categoria> lista = new CategoriaBL().Buscar(new Categoria { Estado = 1 });
            return Json(lista);
        }
        public JsonResult ObtenerMarcas()
        {
            List<Marca> lista = new MarcaBL().Buscar(new Marca { Estado = 1 });
            return Json(lista);
        }

        #region Crear registro en el canva
        //Categoria
        public bool ValidarDatos(Categoria pCategoria)
        {
            bool resultado = true;

            if (string.IsNullOrWhiteSpace(pCategoria.Nombre))

            {
                resultado = false;
            }
            return resultado;
        }
        public JsonResult Guardar(Categoria pCategoria)
        {
            int resultado = 0;
            try
            {
                if (ValidarDatos(pCategoria) == true)
                {
                    resultado = new CategoriaBL().Guardar(pCategoria);
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            return Json(resultado);

        }
        //Marcas
        public bool ValidarDatos(Marca pMarca)
        {
            bool resultado = true;

            if (string.IsNullOrWhiteSpace(pMarca.Nombre))

            {
                resultado = false;
            }
            return resultado;
        }
        public JsonResult Guardar(Marca pMarca)
        {
            int resultado = 0;
            try
            {
                if (ValidarDatos(pMarca) == true)
                {
                    resultado = new MarcaBL().Guardar(pMarca);
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            return Json(resultado);

        }
        #endregion
    }

}








