//using ControlVentas.BL;
//using ControlVentas.EN;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;

//namespace ControlVentas.UI.AppWebMVC.Controllers
//{
//    public class InventarioController : Controller
//    {
//        // GET: Inventario
//        public ActionResult Index()
//        {
//            return View();
//        }
//        public bool ValidarDatos(Inventario pInventario)
//        {
//            //Metodo para validar todos los campos que son obligatorios para guardaro modificar
//            //
//            //
//            bool resultado = true;
//            if (pInventario.Id == 0)
//            {
//                resultado = false;
//            }
//            if (pInventario.PrecioVenta == 0)
//            {
//                resultado = false;
//            }
//            return resultado;
//        }
//        //Acciones CRUD
//        public JsonResult Guardar(Inventario pInventario)
//        {
//            int resultado = 0;
//            try
//            {
//                if (ValidarDatos(pInventario) == true)
//                {
//                    resultado = new InventarioBL().Guardar(pInventario);
//                }
//            }
//            catch (Exception ex)
//            {

//                resultado = 0;
//            }
//            return Json(resultado);
//        }
//        public JsonResult Modificar(Inventario pInventario)
//        {
//            int resultado = 0;
//            try
//            {
//                if (pInventario.Id > 0 && ValidarDatos(pInventario) == true)
//                {
//                    resultado = new InventarioBL().Modificar(pInventario);
//                }
//            }
//            catch (Exception ex)
//            {

//                resultado = 0;
//            }
//            return Json(resultado);
//        }
//        public JsonResult Eliminar(Inventario pInventario)
//        {
//            int resultado = 0;
//            try
//            {
//                if (pInventario.Id > 0)
//                {
//                    resultado = new InventarioBL().Eliminar(pInventario);
//                }
//            }
//            catch (Exception ex)
//            {

//                resultado = 0;
//            }
//            return Json(resultado);
//        }
//        public JsonResult BuscarPorId(int pId)
//        {
//            Inventario objInventario = new InventarioBL().BuscarPorId(pId);
//            return Json(objInventario);
//        }
//        public JsonResult Buscar(Inventario pInventario)
//        {
//            InventarioBL inventarioBL = new InventarioBL();
//            List<Inventario> lista = inventarioBL.Buscar(pInventario);
//            //Ejecutar el metodo para llenar las propiedades virtuales de Inventario y saber a que Producto y Marca pertenece el inventario
//            inventarioBL.CargarPropiedadVirtualProducto(lista);
//            return Json(lista);
//        }
//        public JsonResult ObtenerProductos()
//        {
//            List<Producto> lista = new ProductoBL().Buscar(new Producto { Estado = 1 });
//            return Json(lista);
//        }
//    }

//}