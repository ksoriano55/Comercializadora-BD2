using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using comercializadora.DataBase;
using EntityState = System.Data.Entity.EntityState;

namespace comercializadora.Controllers
{
    public class FincasController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: Fincas
        public ActionResult Index()
        {
            return View(db.Finca.ToList());
        }

        // GET: Fincas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Finca finca = db.Finca.Find(id);
            if (finca == null)
            {
                return HttpNotFound();
            }
            return View(finca);
        }

        // GET: Fincas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fincas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombre,ProductorID")] Finca finca)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;

                list = db.spInsertFinca(finca.Nombre, finca.ProductorID);
                foreach (spInsertFinca_Result tbFinca in list)
                    MensajeError = tbFinca.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }

                return RedirectToAction("Index");
            }

            return View(finca);
        }

        // GET: Fincas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Finca finca = db.Finca.Find(id);
            if (finca == null)
            {
                return HttpNotFound();
            }
            return View(finca);
        }

        // POST: Fincas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FincaID,Nombre,ProductorID")] Finca finca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(finca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(finca);
        }

        // GET: Fincas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Finca finca = db.Finca.Find(id);
            if (finca == null)
            {
                return HttpNotFound();
            }
            return View(finca);
        }

        // POST: Fincas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Finca finca = db.Finca.Find(id);
            db.Finca.Remove(finca);
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
