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

        public JsonResult getCompras(int proveedorId)
        {
            var listaCompras = db.Compra.Where(x => x.ProveedorID == proveedorId && x.SaldoPendiente > 0).Select(x => new SelectListItem
            {
                Text = x.CodigoCompra + " - L." + x.SaldoPendiente,
                Value = x.CompraID.ToString()
            }).ToList();

            return Json(listaCompras, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateAnticipo()
        {
            ViewBag.ProductorId = new SelectList(db.Productor, "ProductorID", "Nombre");
            ViewBag.TipoPagoId = new SelectList(db.TipoPago, "TipoPagoId", "Descripcion");
            return View();
        }
        // GET: Pagos/Create
        public ActionResult Create()
        {
            var compraBD = db.Compra.Where(x=> x.SaldoPendiente>0).Select(x => new
            {
                CompraID = x.CompraID,
                Descripcion = x.CodigoCompra + " - L." + x.SaldoPendiente,
            });
            ViewBag.CompraId = new SelectList(compraBD, "CompraID", "Descripcion");
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
                var MensajeError = "";
                IEnumerable<object> list;
                var concepto = pagos.Concepto == null ? "Anticipo" : pagos.Concepto;
                list = db.SP_InsertPagos(pagos.ProductorId,
                                         pagos.ProveedorId,
                                         pagos.CompraId,
                                         pagos.TipoPagoId,
                                         concepto,
                                         pagos.Fecha,
                                         pagos.Monto);
                foreach (SP_InsertPagos_Result p in list)
                    MensajeError = p.MensajeError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }
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
