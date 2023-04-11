using ControlVentas.BL;
using ControlVentas.EN;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace ControlVentas.UI.AppWebMVC.Controllers
{
    public class ClienteController : Controller
    {
        //Acciones
        public ActionResult Index()
        {
            return View();

        }
        #region acciones de guardar , moificar y eliminar
        public bool ValidarDatos(Cliente pCliente)
        {
            bool resultado = true;

            if (string.IsNullOrWhiteSpace(pCliente.NombreCompleto))

            {
                resultado = false;
            }
            return resultado;

        }
        public JsonResult Guardar(Cliente pCliente)
        {
            int resultado = 0;
            try
            {
                if (ValidarDatos(pCliente) == true)
                {
                    resultado = new ClienteBL().Guardar(pCliente);
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            return Json(resultado);

        }

        public JsonResult Modificar(Cliente pCliente)
        {
            int resultado = 0;
            try
            {
                if (pCliente.Id > 0 && ValidarDatos(pCliente) == true)
                {
                    resultado = new ClienteBL().Modificar(pCliente);

                }
            }
            catch (Exception ex)
            {
                resultado = 0;

            }
            return Json(resultado);
        }
        public JsonResult Eliminar(Cliente pCliente)
        {
            int resultado = 0;
            try
            {
                if (pCliente.Id > 0)
                {
                    resultado = new ClienteBL().Eliminar(pCliente);
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
            Cliente objCliente = new ClienteBL().BuscarPorId(pId);
            return Json(objCliente);
        }
        public JsonResult Buscar(Cliente pCliente)
        {
            List<Cliente> lista = new ClienteBL().Buscar(pCliente);
            return Json(lista);
        }
    }
}