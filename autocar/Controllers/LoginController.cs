using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using autocar.Models;

namespace Navette.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();

        }

       
        public ActionResult Logout()
        {
            Session["Nom"] = null;
            return RedirectToAction("Index");
        }

        // GET: Login/Details/5

        [HttpPost]
        public ActionResult connexion(Utilisateur objUser)
        {
            NavetteEntities db = new NavetteEntities();
            var resultat = db.Utilisateurs.Where(a => a.Username.Equals(objUser.Username) && a.Password.Equals(objUser.Password)).ToList();
            if (resultat.Count() > 0)
            {
                Session["UserID"] = resultat.FirstOrDefault().Id;
                Session["Nom"] = resultat.FirstOrDefault().Nom;
                Session["resultat"] = resultat;
                if (resultat.FirstOrDefault().isadmin == 1)
                {
                    Session["isadmin"] = true;
                }
                else
                {
                    Session["isadmin"] = null;
                }
                Session["Actuel"] = resultat;

            }
            return RedirectToAction("AfficherLesAbonnements", "Abonnements");

        }





    }
}
