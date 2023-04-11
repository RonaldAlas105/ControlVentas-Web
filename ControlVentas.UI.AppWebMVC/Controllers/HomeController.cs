using ControlVentas.BL;
using ControlVentas.EN;
using System.Web.Mvc;
using System.Web.Security;

namespace ControlVentas.UI.AppWebMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Mant()
        {
            return View();
        }
        public ActionResult Inventario()
        {
            return View();
        }
        public ActionResult Informacion()
        {
            return View();
        }
        public ActionResult Login(string pMsg = null)
        {
            //Agregar System web security
            ViewBag.msg = pMsg;
            FormsAuthentication.SignOut();
            return View();
        }
        [HttpPost]
        public ActionResult Login(Usuario pUsuario)
        {
            Usuario usuarioSesion = new UsuarioBL().IniciarSesion(pUsuario);
            if (usuarioSesion != null && usuarioSesion.Id > 0)
            {
                Session["Usuario"] = usuarioSesion;
                Session["RolSesion"] = new RolBL().BuscarPorId(usuarioSesion.IdRol).Nombre;

                // Guardar nombre de usuario en la sesión
                Session["Nombre"] = usuarioSesion.Nombre;

                TempData["SwalAlert1"] = "Bienvenido/a " + usuarioSesion.Nombre;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["SwalAlert2"] = "Credenciales incorrectas, por favor vuelva a intentarlo";
            }
            return View();
        }

        public ActionResult Salir()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
        //xd
    }
}