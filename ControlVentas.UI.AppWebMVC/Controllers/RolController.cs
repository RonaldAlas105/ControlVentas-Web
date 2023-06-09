﻿using ControlVentas.BL;
using ControlVentas.EN;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace ControlVentas.UI.AppWebMVC.Controllers
{
    public class RolController : Controller
    {
        //Acciones
        public ActionResult Index()
        {
            return View();

        }
        #region acciones de guardar , moificar y eliminar
        public bool ValidarDatos(Rol pRol)
        {
            bool resultado = true;

            if (string.IsNullOrWhiteSpace(pRol.Nombre))

            {
                resultado = false;
            }
            return resultado;
        }
        public JsonResult Guardar(Rol pRol)
        {
            int resultado = 0;
            try
            {
                if (ValidarDatos(pRol) == true)
                {
                    resultado = new RolBL().Guardar(pRol);
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            return Json(resultado);

        }

        public JsonResult Modificar(Rol pRol)
        {
            int resultado = 0;
            try
            {
                if (pRol.Id > 0 && ValidarDatos(pRol) == true)
                {
                    resultado = new RolBL().Modificar(pRol);

                }
            }
            catch (Exception ex)
            {
                resultado = 0;

            }
            return Json(resultado);
        }
        public JsonResult Eliminar(Rol pRol)
        {
            int resultado = 0;
            try
            {
                if (pRol.Id > 0)
                {
                    resultado = new RolBL().Eliminar(pRol);
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
            Rol objRol = new RolBL().BuscarPorId(pId);
            return Json(objRol);
        }
        public JsonResult Buscar(Rol pRol)
        {
            List<Rol> lista = new RolBL().Buscar(pRol);
            return Json(lista);
        }
    }
}