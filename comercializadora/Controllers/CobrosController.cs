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
    public class CobrosController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: Cobros
        public ActionResult Index()
        {
            var cobros = db.Cobros.Include(c => c.Cliente).Include(c => c.Factura).Include(c => c.TipoPago);
            return View(cobros.ToList());
        }

        // GET: Cobros/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cobros cobros = db.Cobros.Find(id);
            if (cobros == null)
            {
                return HttpNotFound();
            }
            return View(cobros);
        }

        public JsonResult getFacturas(int clienteId)
        {
            var listaFacturas = db.Factura.Where(x => x.ClienteId == clienteId && x.SaldoPendiente > 0).Select(x => new SelectListItem
            {
                Text = x.CodigoFactura + " - L." + x.SaldoPendiente,
                Value = x.FacturaID.ToString()
            }).ToList();

            return Json(listaFacturas, JsonRequestBehavior.AllowGet);
        }

        // GET: Cobros/Create
        public ActionResult Create()
        {
            ViewBag.clienteId = new SelectList(db.Cliente, "ClienteID", "Nombre");
            ViewBag.facturaId = new SelectList(db.Factura, "FacturaID", "CodigoFactura");
            ViewBag.TipoPagoID = new SelectList(db.TipoPago, "TipoPagoId", "Descripcion");
            return View();
        }

        // POST: Cobros/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigoCobro,clienteId,facturaId,TipoPagoID,Fecha,Monto")] Cobros cobros)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;
               
                list = db.sp_InsertCobros(cobros.clienteId,
                                            cobros.facturaId,
                                            cobros.TipoPagoID,
                                            cobros.Fecha,
                                            cobros.Monto);
                foreach (sp_InsertCobros_Result p in list)
                    MensajeError = p.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }
                return RedirectToAction("Index");
            }

            ViewBag.clienteId = new SelectList(db.Cliente, "ClienteID", "Nombre", cobros.clienteId);
            ViewBag.facturaId = new SelectList(db.Factura, "FacturaID", "CodigoFactura", cobros.facturaId);
            ViewBag.TipoPagoID = new SelectList(db.TipoPago, "TipoPagoId", "Descripcion", cobros.TipoPagoID);
            return View(cobros);
        }

        // GET: Cobros/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cobros cobros = db.Cobros.Find(id);
            if (cobros == null)
            {
                return HttpNotFound();
            }
            ViewBag.clienteId = new SelectList(db.Cliente, "ClienteID", "Nombre", cobros.clienteId);
            ViewBag.facturaId = new SelectList(db.Factura, "FacturaID", "CodigoFactura", cobros.facturaId);
            ViewBag.TipoPagoID = new SelectList(db.TipoPago, "TipoPagoId", "Descripcion", cobros.TipoPagoID);
            return View(cobros);
        }

        // POST: Cobros/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigoCobro,clienteId,facturaId,TipoPagoID,Fecha,Monto")] Cobros cobros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cobros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.clienteId = new SelectList(db.Cliente, "ClienteID", "Nombre", cobros.clienteId);
            ViewBag.facturaId = new SelectList(db.Factura, "FacturaID", "CodigoFactura", cobros.facturaId);
            ViewBag.TipoPagoID = new SelectList(db.TipoPago, "TipoPagoId", "Descripcion", cobros.TipoPagoID);
            return View(cobros);
        }

        // GET: Cobros/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cobros cobros = db.Cobros.Find(id);
            if (cobros == null)
            {
                return HttpNotFound();
            }
            return View(cobros);
        }

        // POST: Cobros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Cobros cobros = db.Cobros.Find(id);
            db.Cobros.Remove(cobros);
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
