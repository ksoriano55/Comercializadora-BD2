using System;
using System.Collections.Generic;
//using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using comercializadora.DataBase;

namespace comercializadora.Controllers
{
    public class CosechasController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: Cosechas
        public ActionResult Index()
        {
            return View(db.Cosecha.ToList());
        }

        // GET: Cosechas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cosecha cosecha = db.Cosecha.Find(id);
            if (cosecha == null)
            {
                return HttpNotFound();
            }
            return View(cosecha);
        }

        // GET: Cosechas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cosechas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CosechaId,Descripcion,FechaInicio,FechaFinal")] Cosecha cosecha)
        {
            if (ModelState.IsValid)
            {
                db.Cosecha.Add(cosecha);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cosecha);
        }

        // GET: Cosechas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cosecha cosecha = db.Cosecha.Find(id);
            if (cosecha == null)
            {
                return HttpNotFound();
            }
            return View(cosecha);
        }

        // POST: Cosechas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CosechaId,Descripcion,FechaInicio,FechaFinal")] Cosecha cosecha)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cosecha).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cosecha);
        }

        // GET: Cosechas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cosecha cosecha = db.Cosecha.Find(id);
            if (cosecha == null)
            {
                return HttpNotFound();
            }
            return View(cosecha);
        }

        // POST: Cosechas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cosecha cosecha = db.Cosecha.Find(id);
            db.Cosecha.Remove(cosecha);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
