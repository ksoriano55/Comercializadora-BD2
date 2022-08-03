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
    public class BodegaController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: Bodega
        public ActionResult Index()
        {
            return View(db.Bodega.ToList());
        }

        // GET: Bodega/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodega bodega = db.Bodega.Find(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            return View(bodega);
        }

        // GET: Bodega/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bodega/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BodegaId,Codigo,Nombre")] Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;

                list = db.SP_InsertBodega(bodega.Codigo,
                                            bodega.Nombre);
                foreach (SP_InsertBodega_Result tbBodega in list)
                    MensajeError = tbBodega.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }
                return RedirectToAction("Index");
            }

            return View(bodega);
        }

        // GET: Bodega/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodega bodega = db.Bodega.Find(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            return View(bodega);
        }

        // POST: Bodega/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BodegaId,Codigo,Nombre")] Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;

                list = db.SP_UpdateBodega(  bodega.BodegaId,
                                            bodega.Codigo,
                                            bodega.Nombre);
                foreach (SP_UpdateBodega_Result tbBodega in list)
                    MensajeError = tbBodega.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }

                return RedirectToAction("Index");
            }
            return View(bodega);
        }

        // GET: Bodega/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodega bodega = db.Bodega.Find(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            return View(bodega);
        }

        // POST: Bodega/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bodega bodega = db.Bodega.Find(id);
            db.Bodega.Remove(bodega);
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
