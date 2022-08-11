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
    public class ProveedorsController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: Proveedors
        public ActionResult Index()
        {
            return View(db.Proveedor.ToList());
        }

        // GET: Proveedors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = db.Proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // GET: Proveedors/Create
        public ActionResult Create()
        {
            ViewBag.ListaPrecioID = new SelectList(db.ListaPrecio, "Codigo", "Descripcion");
            return View();
        }

        // POST: Proveedors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombre,RTN,Telefono,Direccion,EMail,ListaPrecioID,DiasCredito")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;

                list = db.spInsertProveedor(proveedor.Nombre, proveedor.RTN, proveedor.Telefono, proveedor.Direccion, proveedor.EMail, proveedor.ListaPrecioID, proveedor.DiasCredito);
                foreach (spInsertProveedor_Result tbProveedor in list)
                    MensajeError = tbProveedor.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }

                return RedirectToAction("Index");
            }

            return View(proveedor);
        }

        // GET: Proveedors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = db.Proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProveedorID,Nombre,RTN,Telefono,Direccion,EMail")] Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(proveedor).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
                var MensajeError = "";
                IEnumerable<object> list;

                list = db.spUpdateProveedor(proveedor.ProveedorID, proveedor.Nombre, proveedor.RTN, proveedor.Telefono, /*proveedor.Direccion,*/ proveedor.EMail);
                foreach (spUpdateProveedor_Result tbProveedor in list)
                    MensajeError = tbProveedor.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }

                return RedirectToAction("Index");
            }
            return View(proveedor);
        }

        // GET: Proveedors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = db.Proveedor.Find(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proveedor proveedor = db.Proveedor.Find(id);
            db.Proveedor.Remove(proveedor);
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
