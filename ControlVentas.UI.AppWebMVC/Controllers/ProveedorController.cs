using ControlVentas.BL;
using ControlVentas.EN;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace ControlVentas.UI.AppWebMVC.Controllers
{
    public class ProveedorController : Controller
    {
        //Acciones
        public ActionResult Index()
        {
            return View();

        }
        #region acciones de guardar , moificar y eliminar
        public bool ValidarDatos(Proveedor pProveedor)
        {
            bool resultado = true;

            if (string.IsNullOrWhiteSpace(pProveedor.Nombre))

            {
                resultado = false;
            }
            return resultado;
        }
        public JsonResult Guardar(Proveedor pProveedor)
        {
            int resultado = 0;
            try
            {
                if (ValidarDatos(pProveedor) == true)
                {
                    resultado = new ProveedorBL().Guardar(pProveedor);
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            return Json(resultado);

        }

        public JsonResult Modificar(Proveedor pProveedor)
        {
            int resultado = 0;
            try
            {
                if (pProveedor.Id > 0 && ValidarDatos(pProveedor) == true)
                {
                    resultado = new ProveedorBL().Modificar(pProveedor);

                }
            }
            catch (Exception ex)
            {
                resultado = 0;

            }
            return Json(resultado);
        }
        public JsonResult Eliminar(Proveedor pProveedor)
        {
            int resultado = 0;
            try
            {
                if (pProveedor.Id > 0)
                {
                    resultado = new ProveedorBL().Eliminar(pProveedor);
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
            Proveedor objProveedor = new ProveedorBL().BuscarPorId(pId);
            return Json(objProveedor);
        }
        public JsonResult Buscar(Proveedor pProveedor)
        {
            List<Proveedor> lista = new ProveedorBL().Buscar(pProveedor);
            return Json(lista);
        }
    }
}