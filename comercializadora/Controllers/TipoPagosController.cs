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
    public class TipoPagosController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: TipoPagos
        public ActionResult Index()
        {
            return View(db.TipoPago.ToList());
        }

        // GET: TipoPagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPago tipoPago = db.TipoPago.Find(id);
            if (tipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tipoPago);
        }

        // GET: TipoPagos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoPagos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipoPagoId,Descripcion")] TipoPago tipoPago)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;

                list = db.SP_InsertTipoPago(tipoPago.Descripcion);
                foreach (SP_InsertTipoPago_Result tbTipoPago in list)
                    MensajeError = tbTipoPago.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }
                return RedirectToAction("Index");
            }

            return View(tipoPago);
        }

        // GET: TipoPagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPago tipoPago = db.TipoPago.Find(id);
            if (tipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tipoPago);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipoPagoId,Descripcion")] TipoPago tipoPago)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;

                list = db.SP_UpdateTipoPago(tipoPago.TipoPagoId, tipoPago.Descripcion);
                foreach (SP_UpdateTipoPago_Result tbTipoPago in list)
                    MensajeError = tbTipoPago.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo actualizar el registro, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }
                return RedirectToAction("Index");
            }
            return View(tipoPago);
        }

        // GET: TipoPagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoPago tipoPago = db.TipoPago.Find(id);
            if (tipoPago == null)
            {
                return HttpNotFound();
            }
            return View(tipoPago);
        }

        // POST: TipoPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoPago tipoPago = db.TipoPago.Find(id);
            db.TipoPago.Remove(tipoPago);
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
