using System;
using System.Collections.Generic;
//using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using comercializadora.DataBase;
using comercializadora.Models;
namespace comercializadora.Controllers
{
    public class PrecioComprasController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: PrecioCompras
        public ActionResult Index()
        {
            var precioCompraInsumo = db.vPrecioCompraInsumos.ToList();
            var precioCompraProducto = db.vPrecioCompraProductos.ToList();

            var viewModel = new PrecioCompraViewModel
            {
                vPrecioCompraInsumos = precioCompraInsumo,
                vPrecioCompraProductos = precioCompraProducto
            };
            return View(viewModel);
        }

        // GET: PrecioCompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrecioCompra precioCompra = db.PrecioCompra.Find(id);
            if (precioCompra == null)
            {
                return HttpNotFound();
            }
            return View(precioCompra);
        }

        // GET: PrecioCompras/Create
        public ActionResult Create()
        {
            ViewBag.InsumoId = new SelectList(db.Insumo, "InsumoID", "Descripcion");
            ViewBag.ListaPrecios = new SelectList(db.ListaPrecio, "Codigo", "Descripcion");
            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Descripcion");
            return View();
        }

        // POST: PrecioCompras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "precioCompraId,esInsumo,ListaPrecios,ProductoId,InsumoId,Precio,FechaDesde,FechaHasta")] PrecioCompra precioCompra ,bool? esInsumo)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;
                if(precioCompra.esInsumo)
                {
                    precioCompra.ProductoId = null;
                }
                else
                {
                    precioCompra.InsumoId = null ;
                }

                list = db.SP_InsertPrecioCompra(precioCompra.ListaPrecios,
                                               precioCompra.ProductoId,
                                               precioCompra.InsumoId,
                                               precioCompra.Precio,
                                               precioCompra.FechaDesde,
                                               precioCompra.FechaHasta);
                foreach (SP_InsertPrecioCompra_Result result in list)
                    MensajeError = result.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }

                return RedirectToAction("Index");
            }

            ViewBag.InsumoId = new SelectList(db.Insumo, "InsumoID", "Descripcion", precioCompra.InsumoId);
            ViewBag.ListaPrecios = new SelectList(db.ListaPrecio, "Codigo", "Descripcion", precioCompra.ListaPrecios);
            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Descripcion", precioCompra.ProductoId);
            return View(precioCompra);
        }

        // GET: PrecioCompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrecioCompra precioCompra = db.PrecioCompra.Find(id);
            if (precioCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.InsumoId = new SelectList(db.Insumo, "InsumoID", "Descripcion", precioCompra.InsumoId);
            ViewBag.ListaPrecios = new SelectList(db.ListaPrecio, "Codigo", "Descripcion", precioCompra.ListaPrecios);
            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Descripcion", precioCompra.ProductoId);
            return View(precioCompra);
        }

        // POST: PrecioCompras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "precioCompraId,ListaPrecios,ProductoId,InsumoId,Precio,FechaDesde,FechaHasta")] PrecioCompra precioCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(precioCompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InsumoId = new SelectList(db.Insumo, "InsumoID", "Descripcion", precioCompra.InsumoId);
            ViewBag.ListaPrecios = new SelectList(db.ListaPrecio, "Codigo", "Descripcion", precioCompra.ListaPrecios);
            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Descripcion", precioCompra.ProductoId);
            return View(precioCompra);
        }

        // GET: PrecioCompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrecioCompra precioCompra = db.PrecioCompra.Find(id);
            if (precioCompra == null)
            {
                return HttpNotFound();
            }
            return View(precioCompra);
        }

        // POST: PrecioCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrecioCompra precioCompra = db.PrecioCompra.Find(id);
            db.PrecioCompra.Remove(precioCompra);
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
