using autocar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Navette.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Inscription()
        {

            return View();
        }

    [HttpPost]
        public ActionResult Ajouter(Utilisateur User)
        {
            using (NavetteEntities db = new NavetteEntities())
            {
                if(ModelState.IsValid){ 
                db.Utilisateurs.Add(User);
                db.SaveChanges();
                }

            }

            return RedirectToAction("Inscription");
        }
      
    }
}