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
    public class PrecioVentaController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: PrecioVenta
        public ActionResult Index()
        {
            var precioventaInsumo = db.vPrecioVentaInsumos.ToList();
            var precioventaProducto = db.vPrecioVentaProductos.ToList();

            var viewModel = new Models.PrecioVentaViewModel
            {
                vPrecioVentaInsumos = precioventaInsumo,
                vPrecioVentaProductos = precioventaProducto
            };
            return View(viewModel);


            //var precioVenta = db.PrecioVenta.Include(p => p.ListaPrecio1).Include(p => p.Producto).Include(p => p.Insumo);
            //return View(precioVenta.ToList());
        }

        // GET: PrecioVenta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrecioVenta precioVenta = db.PrecioVenta.Find(id);
            if (precioVenta == null)
            {
                return HttpNotFound();
            }
            return View(precioVenta);
        }

        // GET: PrecioVenta/Create
        public ActionResult Create()
        {
            ViewBag.ListaPrecio = new SelectList(db.ListaPrecio, "Codigo", "Descripcion");
            ViewBag.ProductoID = new SelectList(db.Producto, "ProductoID", "Descripcion");
            ViewBag.InsumoId = new SelectList(db.Insumo, "InsumoID", "Descripcion");
            return View();
        }

        // POST: PrecioVenta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrecioVentaID,ProductoID,Precio,FechaDesde,FechaHasta,ListaPrecio,InsumoId")] PrecioVenta precioVenta)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;

                list = db.SP_InsertPrecioVenta( 
                                                precioVenta.ProductoID,
                                                precioVenta.Precio,
                                                precioVenta.FechaDesde,
                                                precioVenta.FechaHasta,
                                                precioVenta.ListaPrecio,
                                                precioVenta.InsumoId);
                foreach (SP_InsertPrecioVenta_Result tbprecioventa in list)
                    MensajeError = tbprecioventa.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.ListaPrecio = new SelectList(db.ListaPrecio, "Codigo", "Descripcion", precioVenta.ListaPrecio);
            ViewBag.ProductoID = new SelectList(db.Producto, "ProductoID", "Descripcion", precioVenta.ProductoID);
            ViewBag.InsumoId = new SelectList(db.Insumo, "InsumoID", "Descripcion", precioVenta.InsumoId);
            return View(precioVenta);
        }

        // GET: PrecioVenta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrecioVenta precioVenta = db.PrecioVenta.Find(id);
            if (precioVenta == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListaPrecio = new SelectList(db.ListaPrecio, "Codigo", "Descripcion", precioVenta.ListaPrecio);
            ViewBag.ProductoID = new SelectList(db.Producto, "ProductoID", "Descripcion", precioVenta.ProductoID);
            ViewBag.InsumoId = new SelectList(db.Insumo, "InsumoID", "Descripcion", precioVenta.InsumoId);
            return View(precioVenta);
        }

        // POST: PrecioVenta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrecioID,ProductoID,Precio,FechaDesde,FechaHasta,ListaPrecio,InsumoId")] PrecioVenta precioVenta)
        {
            if (ModelState.IsValid)
            {

                var MensajeError = "";
                IEnumerable<object> list;

                list = db.SP_UpdatePrecioVenta(
                                                precioVenta.ProductoID,
                                                precioVenta.Precio,
                                                precioVenta.FechaDesde,
                                                precioVenta.FechaHasta,
                                                precioVenta.ListaPrecio,
                                                precioVenta.InsumoId);
                foreach (SP_UpdatePrecioVenta_Result tbprecioventa1 in list)
                    MensajeError = tbprecioventa1.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }
                
                return RedirectToAction("Index");
            }
            ViewBag.ListaPrecio = new SelectList(db.ListaPrecio, "Codigo", "Descripcion", precioVenta.ListaPrecio);
            ViewBag.ProductoID = new SelectList(db.Producto, "ProductoID", "Descripcion", precioVenta.ProductoID);
            ViewBag.InsumoId = new SelectList(db.Insumo, "InsumoID", "Descripcion", precioVenta.InsumoId);
            return View(precioVenta);
        }

        // GET: PrecioVenta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrecioVenta precioVenta = db.PrecioVenta.Find(id);
            if (precioVenta == null)
            {
                return HttpNotFound();
            }
            return View(precioVenta);
        }

        // POST: PrecioVenta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrecioVenta precioVenta = db.PrecioVenta.Find(id);
            db.PrecioVenta.Remove(precioVenta);
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
