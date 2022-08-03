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
    public class PagosController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: Pagos
        public ActionResult Index()
        {
            var pagos = db.Pagos.Include(p => p.Compra).Include(p => p.Productor).Include(p => p.Proveedor).Include(p => p.TipoPago);
            return View(pagos.ToList());
        }

        // GET: Pagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagos pagos = db.Pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            return View(pagos);
        }

        // GET: Pagos/Create
        public ActionResult Create()
        {
            ViewBag.CompraId = new SelectList(db.Compra, "CompraID", "CompraID");
            ViewBag.ProductorId = new SelectList(db.Productor, "ProductorID", "Nombre");
            ViewBag.ProveedorId = new SelectList(db.Proveedor, "ProveedorID", "Nombre");
            ViewBag.TipoPagoId = new SelectList(db.TipoPago, "TipoPagoId", "Descripcion");
            return View();
        }

        // POST: Pagos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PagoId,ProductorId,ProveedorId,cosechaId,CompraId,Concepto,TipoPagoId,Fecha,Monto")] Pagos pagos)
        {
            if (ModelState.IsValid)
            {
                db.Pagos.Add(pagos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompraId = new SelectList(db.Compra, "CompraID", "CompraID", pagos.CompraId);
            ViewBag.ProductorId = new SelectList(db.Productor, "ProductorID", "Nombre", pagos.ProductorId);
            ViewBag.ProveedorId = new SelectList(db.Proveedor, "ProveedorID", "Nombre", pagos.ProveedorId);
            ViewBag.TipoPagoId = new SelectList(db.TipoPago, "TipoPagoId", "Descripcion", pagos.TipoPagoId);
            return View(pagos);
        }

        // GET: Pagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagos pagos = db.Pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompraId = new SelectList(db.Compra, "CompraID", "CompraID", pagos.CompraId);
            ViewBag.ProductorId = new SelectList(db.Productor, "ProductorID", "Nombre", pagos.ProductorId);
            ViewBag.ProveedorId = new SelectList(db.Proveedor, "ProveedorID", "Nombre", pagos.ProveedorId);
            ViewBag.TipoPagoId = new SelectList(db.TipoPago, "TipoPagoId", "Descripcion", pagos.TipoPagoId);
            return View(pagos);
        }

        // POST: Pagos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PagoId,ProductorId,ProveedorId,cosechaId,CompraId,Concepto,TipoPagoId,Fecha,Monto")] Pagos pagos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompraId = new SelectList(db.Compra, "CompraID", "CompraID", pagos.CompraId);
            ViewBag.ProductorId = new SelectList(db.Productor, "ProductorID", "Nombre", pagos.ProductorId);
            ViewBag.ProveedorId = new SelectList(db.Proveedor, "ProveedorID", "Nombre", pagos.ProveedorId);
            ViewBag.TipoPagoId = new SelectList(db.TipoPago, "TipoPagoId", "Descripcion", pagos.TipoPagoId);
            return View(pagos);
        }

        // GET: Pagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagos pagos = db.Pagos.Find(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            return View(pagos);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pagos pagos = db.Pagos.Find(id);
            db.Pagos.Remove(pagos);
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
