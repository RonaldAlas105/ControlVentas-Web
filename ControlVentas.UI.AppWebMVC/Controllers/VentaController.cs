using ControlVentas.BL;
// Referencias
using ControlVentas.EN;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using OfficeOpenXml;

namespace ControlVentas.UI.WebAppMVC.Controllers
{
    public class VentaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Imprimir(int pId)
        {
            Venta objVenta = new VentaBL().BuscarPorId(pId);
            objVenta.Cliente = new ClienteBL().BuscarPorId(objVenta.IdCliente);
            return View(objVenta);
        }


        #region acciones de guardar, modificar, eliminar
        public bool ValidarDatos(Venta pVenta)
        {
            bool resultado = true;

            if (pVenta.IdCliente <= 0)
            {
                resultado = false;
            }
            if (pVenta.Estatus <= 0)
            {
                resultado = false;
            }
            // Grid Detail - Validar que traiga detalles
            if (pVenta.Detalles == null || pVenta.Detalles != null && pVenta.Detalles.Count == 0)
            {
                resultado = false;
            }
            // Fin Grid Detail

            return resultado;
        }
        // Acciones CRUD
        public JsonResult Guardar(Venta pVenta)
        {
            int resultado = 0;

            try
            {
                if (ValidarDatos(pVenta) == true)
                {
                    // Tomar automaticamente usuario en sesion 
                    Usuario usuario = Session["Usuario"] as Usuario;
                    pVenta.IdUsuario = usuario.Id;

                    resultado = new VentaBL().Guardar(pVenta);
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }

            return Json(resultado);
        }

        public JsonResult Modificar(Venta pVenta)
        {
            int resultado = 0;
            try
            {
                if (pVenta.Id > 0 && ValidarDatos(pVenta) == true)
                {
                    Usuario usuario = Session["Usuario"] as Usuario;
                    pVenta.IdUsuario = usuario.Id;
                    resultado = new VentaBL().Modificar(pVenta);
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            return Json(resultado);
        }

        public JsonResult Eliminar(Venta pVenta)
        {
            int resultado = 0;

            try
            {
                if (pVenta.Id > 0)
                {
                    resultado = new VentaBL().Eliminar(pVenta);
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }

            return Json(resultado);
        }

        #endregion

        public JsonResult BuscarPorId(int pId)
        {
            Venta objVenta = new VentaBL().BuscarPorId(pId);
            return Json(objVenta);
        }

        public JsonResult Buscar(Venta pVenta)
        {
            VentaBL ventaBL = new VentaBL();
            List<Venta> lista = new VentaBL().Buscar(pVenta);
            ventaBL.CargarPropiedadVirtualCliente(lista);
            return Json(lista);

        }


        public JsonResult ObtenerProductos()
        {
            List<Producto> lista = new ProductoBL().Buscar(new Producto { Estado = 1 });
            new ProductoBL().CargarPropiedadVirtualInventario(lista);
            return Json(lista);

        }

        public JsonResult ObtenerClientes()
        {
            List<Cliente> lista = new ClienteBL().Buscar(new Cliente { Estado = 1 });
            return Json(lista);
        }
        public void DescargarDatos(DateTime fechaInicio, DateTime fechaFin)
        {
            string connectionString = "Data Source=.;Initial Catalog=BookShop;Integrated Security=True";
            string query = @"SELECT Ventas.Id, Usuarios.Nombre AS NombreUsuario, Clientes.NombreCompleto AS NombreCliente,
                        FORMAT(Ventas.FechaRegistro, 'dd/MM/yyyy ') AS FechaRegistro,
                        Productos.Nombre AS NombreProducto, DetallesVenta.Precio, DetallesVenta.Cantidad,
                        DetallesVenta.SubTotal, Ventas.Pago
                    FROM Ventas
                    JOIN Usuarios ON Ventas.IdUsuario = Usuarios.Id
                    JOIN DetallesVenta ON Ventas.Id = DetallesVenta.IdVenta
                    JOIN Productos ON DetallesVenta.IdProducto = Productos.Id
                    JOIN Clientes ON Ventas.IdCliente = Clientes.Id
                    WHERE Ventas.FechaRegistro >= @FechaInicio AND Ventas.FechaRegistro <= @FechaFin";

            DataTable dt = new DataTable();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FechaInicio", fechaInicio);
                    command.Parameters.AddWithValue("@FechaFin", fechaFin);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }


            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Ventas");
                worksheet.Cells["A1"].LoadFromDataTable(dt, true);
                byte[] fileContents = package.GetAsByteArray();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment; filename=Ventas.xlsx");
                Response.BinaryWrite(fileContents);
                Response.End();
            }
        }
    }
}