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
    public class ProductosController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: Productos
        public ActionResult Index()
        {
            var producto = db.Producto.Include(p => p.Lotes);
            return View(producto.ToList());
        }

        // GET: Productos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        public JsonResult getLotes(int idFinca)
        {
            var listaLotes =  db.Lotes.Where(y => y.FincaId == idFinca).Select(x => new SelectListItem
            {
                Text = x.TipoSuelo + " - " + x.Extencion,
                Value = x.LoteId.ToString()
            }).ToList();
            return Json(listaLotes, JsonRequestBehavior.AllowGet);

        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.LoteID = new SelectList(db.Lotes, "LoteId", "Extencion");
            ViewBag.FincaId = new SelectList(db.Finca, "FincaID", "Nombre");
            return View();
        }

        // POST: Productos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Descripcion,LoteID")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;

                list = db.SP_InsertProductos(producto.LoteID,
                                             producto.Descripcion);
                foreach (SP_InsertProductos_Result product in list)
                    MensajeError = product.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.LoteID = new SelectList(db.Lotes, "LoteId", "Extencion", producto.LoteID);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.LoteID = new SelectList(db.Lotes, "LoteId", "Extencion", producto.LoteID);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductoID,Descripcion,LoteID")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                var productodb = db.Producto.FirstOrDefault(x => x.ProductoID == producto.ProductoID);
                productodb.Descripcion = producto.Descripcion;

                db.Entry(productodb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LoteID = new SelectList(db.Lotes, "LoteId", "Extencion", producto.LoteID);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
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
