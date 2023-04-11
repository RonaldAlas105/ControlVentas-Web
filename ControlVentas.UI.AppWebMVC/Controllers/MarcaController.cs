using ControlVentas.BL;
using ControlVentas.EN;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace ControlVentas.UI.AppWebMVC.Controllers
{
    public class MarcaController : Controller
    {
        //Acciones
        public ActionResult Index()
        {
            return View();

        }
        #region acciones de guardar , moificar y eliminar
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

        public JsonResult Modificar(Marca pMarca)
        {
            int resultado = 0;
            try
            {
                if (pMarca.Id > 0 && ValidarDatos(pMarca) == true)
                {
                    resultado = new MarcaBL().Modificar(pMarca);

                }
            }
            catch (Exception ex)
            {
                resultado = 0;

            }
            return Json(resultado);
        }
        public JsonResult Eliminar(Marca pMarca)
        {
            int resultado = 0;
            try
            {
                if (pMarca.Id > 0)
                {
                    resultado = new MarcaBL().Eliminar(pMarca);
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
            Marca objMarca = new MarcaBL().BuscarPorId(pId);
            return Json(objMarca);
        }
        public JsonResult Buscar(Marca pMarca)
        {
            List<Marca> lista = new MarcaBL().Buscar(pMarca);
            return Json(lista);
        }
    }
}