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
    public class CosechaDetallesController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: CosechaDetalles
        public ActionResult Index()
        {
            var cosechaDetalle = db.CosechaDetalle.Include(c => c.Cosecha).Include(c => c.Producto);
            return View(cosechaDetalle.ToList());
        }

        // GET: CosechaDetalles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CosechaDetalle cosechaDetalle = db.CosechaDetalle.Find(id);
            if (cosechaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(cosechaDetalle);
        }

        // GET: CosechaDetalles/Create
        public ActionResult Create()
        {
            ViewBag.CosechaID = new SelectList(db.Cosecha, "CosechaId", "Descripcion");
            ViewBag.ProductoID = new SelectList(db.Producto, "ProductoID", "Descripcion");
            return View();
        }

        // POST: CosechaDetalles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CosechaDetalleID,CosechaID,ProductoID,Precio,Cantidad")] CosechaDetalle cosechaDetalle)
        {
            if (ModelState.IsValid)
            {
                db.CosechaDetalle.Add(cosechaDetalle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CosechaID = new SelectList(db.Cosecha, "CosechaId", "Descripcion", cosechaDetalle.CosechaID);
            ViewBag.ProductoID = new SelectList(db.Producto, "ProductoID", "Descripcion", cosechaDetalle.ProductoID);
            return View(cosechaDetalle);
        }

        // GET: CosechaDetalles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CosechaDetalle cosechaDetalle = db.CosechaDetalle.Find(id);
            if (cosechaDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.CosechaID = new SelectList(db.Cosecha, "CosechaId", "Descripcion", cosechaDetalle.CosechaID);
            ViewBag.ProductoID = new SelectList(db.Producto, "ProductoID", "Descripcion", cosechaDetalle.ProductoID);
            return View(cosechaDetalle);
        }

        // POST: CosechaDetalles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CosechaDetalleID,CosechaID,ProductoID,Precio,Cantidad")] CosechaDetalle cosechaDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cosechaDetalle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CosechaID = new SelectList(db.Cosecha, "CosechaId", "Descripcion", cosechaDetalle.CosechaID);
            ViewBag.ProductoID = new SelectList(db.Producto, "ProductoID", "Descripcion", cosechaDetalle.ProductoID);
            return View(cosechaDetalle);
        }

        // GET: CosechaDetalles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CosechaDetalle cosechaDetalle = db.CosechaDetalle.Find(id);
            if (cosechaDetalle == null)
            {
                return HttpNotFound();
            }
            return View(cosechaDetalle);
        }

        // POST: CosechaDetalles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CosechaDetalle cosechaDetalle = db.CosechaDetalle.Find(id);
            db.CosechaDetalle.Remove(cosechaDetalle);
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
