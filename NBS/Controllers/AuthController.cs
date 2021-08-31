using System.Linq;
using System.Web.Mvc;

namespace NBS.Controllers
{
    public class AuthController : Controller
    {
        private DbCon db = new DbCon();
        // GET: /Auth/Index
        public ActionResult Index()
        {
            return View();
            //[DataType(DataType.Password)]
        }
        // POST: Auth/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Tb_Users u)
        {
            var oVal = db.Sp_Login(u.User, u.Pwd);
            foreach (var oItem in oVal)
            {
                if (oItem.User == u.User && oItem.Pwd == u.Pwd)
                {
                    Session["ulog"] = oItem.Id;
                    Session["role"] = oItem.Role;
                    Session["login"] = oItem.Name;
                    return RedirectToAction("Main");
                }
                else return HttpNotFound();
            }
            return View();
        }

        // GET: Auth/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Tb_Users u)
        {
            var oVal = db.Sp_Login(u.User, u.Pwd);
            foreach (var oItem in oVal)
            {
                if (oItem.User == u.User && oItem.Pwd == u.Pwd)
                {
                    Session["ulog"] = oItem.Id;
                    Session["role"] = oItem.Role;
                    Session["login"] = oItem.Name;
                    return RedirectToAction("Main");
                }
                else return HttpNotFound();
            }
            return View();
        }

        // GET: Auth/Main
        public ActionResult Main()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Index", "Auth");
            }
            else return View();
        }

        // GET: Auth/Logout
        public ActionResult Logout()
        {
            Session.Remove("ulog");
            Session.Remove("role");
            Session.Remove("login");
            return RedirectToAction("Index", "Auth");
        }
    }
}