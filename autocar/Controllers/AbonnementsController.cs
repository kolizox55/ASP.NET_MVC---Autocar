using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using autocar.Models;


namespace Navette.Controllers
{
    public class AbonnementsController : Controller
    {
        private NavetteEntities db = new NavetteEntities();
        public ActionResult Valider(int? id)
        {
          
            Abonnement abonnement = db.Abonnements.Find(id);
            abonnement.isvalide = "Validé";
            db.SaveChanges();
            return RedirectToAction("AfficherLesAbonnements");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AjouterTraitement(Abonnement abonnement)
        {   

            using (NavetteEntities dbz = new NavetteEntities()) {
                if (!db.Abonnements.Any(
                o => 
                o.Date_arrivee == abonnement.Date_arrivee 
                && o.Date_depart == abonnement.Date_depart 
                && o.Duree == abonnement.Duree 
                && o.Ville_arrivee == abonnement.Ville_arrivee
                && o.Ville_depart == abonnement.Ville_depart       
                )) { 
                    
                Session["success"] = "valide";
                abonnement.id_utilisateur = Convert.ToInt32(Session["UserID"]);
                abonnement.DateDeMise = DateTime.Now;
                abonnement.isvalide = "En attente" ;
                dbz.Abonnements.Add(abonnement);
                Utilisateur Ut = dbz.Utilisateurs.Find(abonnement.id_utilisateur);
                Ut.Abonnements.Add(abonnement);
                dbz.SaveChanges();
                }
                else
                {
                Session["success"] = "error";
                }
            }
            return RedirectToAction("AfficherLesAbonnements");

        }
        public ActionResult AfficherLesAbonnements()
        {
            List<Abonnement> listeAbonnements = db.Abonnements.ToList();
            return View(listeAbonnements);
        }

        public ActionResult Display(Abonnement abonnement)
        {
            return RedirectToAction("AfficherLesAbonnements");
        }
       
        public ActionResult DisplayAbonnement()
        {
            return View();
        }
        public ActionResult DisplayUtilisateur()
        {
            return View();
        }
        public ActionResult displayuti()
        {
            List<Utilisateur> listeUtilisateurs = db.Utilisateurs.ToList();
            return View(listeUtilisateurs);
        }


        public ActionResult Modifier(int? id)
        {
            
            Abonnement abonnement = db.Abonnements.Find(id); 
            return View(abonnement);
        }
       
       [HttpPost]
        public ActionResult ModifierTraitement([Bind(Include = "Id,Duree,Ville_depart,Ville_arrivee,Date_depart,Date_arrivee,id_utilisateur,DateDeModification,DateDeMise,isvalide")] Abonnement abonnement)
        {
            
                db.Entry(abonnement).State = EntityState.Modified;
                 abonnement.DateDeModification = DateTime.Now;
                 db.SaveChanges();
                return RedirectToAction("AfficherLesAbonnements");
        }

        public ActionResult Supprimer(int id)
        {
            Abonnement abonnement = db.Abonnements.Find(id);
            db.Abonnements.Remove(abonnement);
            db.SaveChanges();
            return RedirectToAction("AfficherLesAbonnements");
        }



    }
}
