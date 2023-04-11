using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Referencias
using ControlVentas.EN;
using ControlVentas.BL;

namespace ControlVentas.UI.AppWebMVC.Controllers
{
    public class DetalleCompraController : Controller
    {
        // GET: DetalleCompra
        public ActionResult Index()
        {
            return View();
        }
      

    }
}