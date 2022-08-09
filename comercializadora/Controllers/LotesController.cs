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
    public class LotesController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: Lotes
        public ActionResult Index()
        {
            var lotes = db.Lotes.Include(l => l.Finca);
            return View(lotes.ToList());
        }

        // GET: Lotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lotes lotes = db.Lotes.Find(id);
            if (lotes == null)
            {
                return HttpNotFound();
            }
            return View(lotes);
        }

        // GET: Lotes/Create
        public ActionResult Create()
        {
            ViewBag.FincaId = new SelectList(db.Finca, "FincaID", "Nombre");
            return View();
        }

        // POST: Lotes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FincaId,Extencion,TipoSuelo,TipoRiego,CantidadCosecha")] Lotes lotes)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;

                list = db.spInsertLote(lotes.FincaId, lotes.Extencion, lotes.TipoSuelo, lotes.TipoRiego, lotes.CantidadCosecha);
                foreach (spInsertLote_Result tbLote in list)
                    MensajeError = tbLote.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }

                return RedirectToAction("Index");
            }

            ViewBag.FincaId = new SelectList(db.Finca, "FincaID", "Nombre", lotes.FincaId);
            return View(lotes);
        }

        // GET: Lotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lotes lotes = db.Lotes.Find(id);
            if (lotes == null)
            {
                return HttpNotFound();
            }
            ViewBag.FincaId = new SelectList(db.Finca, "FincaID", "Nombre", lotes.FincaId);
            return View(lotes);
        }

        // POST: Lotes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoteId,FincaId,Extencion,TipoSuelo,TipoRiego,CantidadCosecha")] Lotes lotes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lotes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FincaId = new SelectList(db.Finca, "FincaID", "Nombre", lotes.FincaId);
            return View(lotes);
        }

        // GET: Lotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lotes lotes = db.Lotes.Find(id);
            if (lotes == null)
            {
                return HttpNotFound();
            }
            return View(lotes);
        }

        // POST: Lotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lotes lotes = db.Lotes.Find(id);
            db.Lotes.Remove(lotes);
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
