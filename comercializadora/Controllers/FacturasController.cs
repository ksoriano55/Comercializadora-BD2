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
    public class FacturasController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: Facturas
        public ActionResult Index()
        {
            var factura = db.Factura.Where(x => x.ClienteId != null).Include(f => f.Cliente);
            return View(factura.ToList());
        }
        public ActionResult IndexFacturaProductor()
        {
            var factura = db.Factura.Where(x => x.ProductorId != null).Include(f => f.Productor);
            return View(factura.ToList());
        }
        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            var clienteList =db.Cliente.Select(x => new
            {
               ClienteID = x.ClienteID,
               Nombre = x.RTN + " - " + x.Nombre
            });

            ViewBag.ClienteId = new SelectList(clienteList, "ClienteID", "Nombre");
            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Descripcion");
            return View();
        }
        public ActionResult CreateFacturaProductor()
        {
            var ProductorList = db.Productor.Select(x => new
            {
                ProductorID = x.ProductorID,
                Nombre = x.RTN + " - " + x.Nombre
            });

            ViewBag.ProductorId = new SelectList(ProductorList, "ProductorID", "Nombre");
            ViewBag.InsumoId = new SelectList(db.Insumo, "InsumoID", "Descripcion");
            return View();
        }

        public JsonResult InsertFacturaDetalle(FacturaDetalle facturaDetalle)
        {
            List<FacturaDetalle> sessionFacturaDetalle = new List<FacturaDetalle>();

            var list = (List<FacturaDetalle>)Session["FacturaDetalle"];

            if (list == null)
            {
                sessionFacturaDetalle.Add(facturaDetalle);
                Session["FacturaDetalle"] = sessionFacturaDetalle;
            }
            else
            {
                list.Add(facturaDetalle);
                Session["FacturaDetalle"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FacturaID,CodigoFactura,ClienteId,ProductorId")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;
                var listaFacturaDetalle = (List<FacturaDetalle>)Session["FacturaDetalle"];
                var MensajeErrorDetalle = "";
                IEnumerable<object> listaDetalle = null;
                var clienteId = factura.ClienteId != null ? factura.ClienteId : factura.ProductorId;
                var pantalla = factura.ClienteId != null ?  "Index" : "IndexFacturaProductor";
                list = db.SP_InsertFactura(factura.ClienteId, factura.ProductorId);
                foreach (SP_InsertFactura_Result result in list)
                    MensajeError = result.MensajeError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (listaFacturaDetalle != null)
                    {
                        if (listaFacturaDetalle.Count != 0)
                        {
                            foreach (FacturaDetalle facturaDetalle in listaFacturaDetalle)
                            {
                                facturaDetalle.FacturaID = Convert.ToInt32(MensajeError);
                                listaDetalle = db.SP_InsertFacturaDetalle(
                                    facturaDetalle.FacturaID,
                                    facturaDetalle.ProductoID,
                                    facturaDetalle.InsumoId,
                                    facturaDetalle.Cantidad,
                                    clienteId);
                                foreach (SP_InsertFacturaDetalle_Result result in listaDetalle)
                                {
                                    MensajeErrorDetalle = result.MessageError;
                                    if (MensajeErrorDetalle.StartsWith("-1"))
                                    {
                                        ///Poner accion en caso de ocurrir error
                                    }
                                }
                            }
                        }
                    }
                }

                return RedirectToAction(pantalla);
            }

            ViewBag.ClienteId = new SelectList(db.Cliente, "ClienteID", "Nombre", factura.ClienteId);
            ViewBag.ProductorId = new SelectList(db.Productor, "ProductorID", "Nombre", factura.ProductorId);
            Session["FacturaDetalle"] = null;
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Cliente, "ClienteID", "Nombre", factura.ClienteId);
            ViewBag.ProductorId = new SelectList(db.Productor, "ProductorID", "Nombre", factura.ProductorId);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FacturaID,CodigoFactura,ClienteId,ProductorId,TipoFactura,FechaFactura,FechaVencimiento,ValorFactura,SaldoPendiente")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Cliente, "ClienteID", "Nombre", factura.ClienteId);
            ViewBag.ProductorId = new SelectList(db.Productor, "ProductorID", "Nombre", factura.ProductorId);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Factura.Find(id);
            db.Factura.Remove(factura);
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
