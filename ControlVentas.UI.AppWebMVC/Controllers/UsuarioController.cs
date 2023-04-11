using ControlVentas.BL;
//Referencias
using ControlVentas.EN;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ControlVentas.UI.AppWebMVC.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }
        public bool ValidarDatos(Usuario pUsuario)
        {
            //Metodos para validar todos los campos que son obligatorios para guardar o modificar
            //True=> si los datos son validos
            //False=> si los datos no son validos
            bool resultado = true;
            if (pUsuario.IdRol == 0)
            {
                resultado = false;
            }
            if (string.IsNullOrWhiteSpace(pUsuario.Nombre))
            {
                resultado = false;
            }
            if (string.IsNullOrWhiteSpace(pUsuario.Apellido))
            {
                resultado = false;
            }
            if (string.IsNullOrWhiteSpace(pUsuario.NombreUsuario))
            {
                resultado = false;
            }
            if (pUsuario.Id == 0 && string.IsNullOrWhiteSpace(pUsuario.ClaveUsuario))
            {
                //La clavr solo es obligatoria cuando se guarda un nuevo usuario, si Id es igual a 0 significa que se desea guardar
                resultado = false;
            }
            return resultado;
        }
        // Aciones CRUD
        public JsonResult Guardar(Usuario pUsuario)
        {
            int resultado = 0;
            try
            {
                if (ValidarDatos(pUsuario) == true)
                {
                    resultado = new UsuarioBL().Guardar(pUsuario);
                }
            }
            catch (Exception ex)
            {

                resultado = 0;
            }
            return Json(resultado);
        }
        public JsonResult Modificar(Usuario pUsuario)
        {
            int resultado = 0;
            try
            {
                if (pUsuario.Id > 0 && ValidarDatos(pUsuario) == true)
                {
                    resultado = new UsuarioBL().Modificar(pUsuario);
                }
            }
            catch (Exception ex)
            {

                resultado = 0;
            }
            return Json(resultado);
        }
        public JsonResult Eliminar(Usuario pUsuario)
        {
            int resultado = 0;
            try
            {
                if (pUsuario.Id > 0)
                {
                    resultado = new UsuarioBL().Eliminar(pUsuario);
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
            Usuario objUsuario = new UsuarioBL().BuscarPorId(pId);
            return Json(objUsuario);
        }
        public JsonResult Buscar(Usuario pUsuario)
        {
            UsuarioBL productoBL = new UsuarioBL();
            List<Usuario> lista = productoBL.Buscar(pUsuario);
            //Ejecutar el metodo para llenar las propiedades virtuales de Usuario y saber a que Rol y Marca pertenece el producto
            productoBL.CargarPropiedadVirtualRol(lista);
            return Json(lista);
        }
        public JsonResult ObtenerRoles()
        {
            List<Rol> lista = new RolBL().Buscar(new Rol { Estado = 1 });
            return Json(lista);
        }

    }
}