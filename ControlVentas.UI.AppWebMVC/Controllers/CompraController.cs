using ControlVentas.BL;
//Referencias
using ControlVentas.EN;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace ControlVentas.UI.AppWebMVC.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Imprimir(int pId)
        {
            Compra objCompra = new CompraBL().BuscarPorId(pId);
            objCompra.Proveedor = new ProveedorBL().BuscarPorId(objCompra.IdProveedor);
            return View(objCompra);
        }

        #region acciones de guardar, modificar, eliminar
        public bool ValidarDatos(Compra pCompra)
        {
            bool resultado = true;
            //cambio W
            if (pCompra.IdProveedor <= 0)
            {
                resultado = false;
            }
            if (pCompra.Estatus <= 0)
            {
                resultado = false;
            }
            // Grid Detail - Validar que traiga detalles
            if (pCompra.Detalles == null || pCompra.Detalles != null && pCompra.Detalles.Count == 0)
            {
                resultado = false;
            }
            // Fin Grid Detail

            return resultado;
        }
        // Acciones CRUD
        public JsonResult Guardar(Compra pCompra)
        {
            int resultado = 0;

            try
            {
                if (ValidarDatos(pCompra) == true)
                {
                    // Tomar automaticamente usuario en sesion 
                    Usuario usuario = Session["Usuario"] as Usuario;
                    pCompra.IdUsuario = usuario.Id;

                    resultado = new CompraBL().Guardar(pCompra);
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }

            return Json(resultado);
        }

        public JsonResult Modificar(Compra pCompra)
        {
            int resultado = 0;
            try
            {
                if (pCompra.Id > 0 && ValidarDatos(pCompra) == true)
                {
                    Usuario usuario = Session["Usuario"] as Usuario;
                    pCompra.IdUsuario = usuario.Id;
                    resultado = new CompraBL().Modificar(pCompra);
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }
            return Json(resultado);
        }

        public JsonResult Eliminar(Compra pCompra)
        {
            int resultado = 0;

            try
            {
                if (pCompra.Id > 0)
                {
                    resultado = new CompraBL().Eliminar(pCompra);
                }
            }
            catch (Exception ex)
            {
                resultado = 0;
            }

            return Json(resultado);
        }

        #endregion

        //Busqueda
        public JsonResult BuscarPorId(int pId)
        {
            Compra objCompra = new CompraBL().BuscarPorId(pId);
            return Json(objCompra);
        }

        public JsonResult Buscar(Compra pCompra)
        {
            CompraBL compraBL = new CompraBL();
            List<Compra> lista = new CompraBL().Buscar(pCompra);
            compraBL.CargarPropiedadVirtualProveedor(lista);
            return Json(lista);
        }
        //Obtencion de otras tablas 
        public JsonResult ObtenerProductos()
        {
            List<Producto> lista = new ProductoBL().Buscar(new Producto { Estado = 1 });
            new ProductoBL().CargarPropiedadVirtualInventario(lista);
            return Json(lista);

        }
        public JsonResult ObtenerUsuarios()
        {
            List<Usuario> lista = new UsuarioBL().Buscar(new Usuario { Estado = 1 });
            return Json(lista);
        }
        public JsonResult ObtenerProveedores()
        {
            List<Proveedor> lista = new ProveedorBL().Buscar(new Proveedor { Estado = 1 });
            return Json(lista);
        }




    }
}