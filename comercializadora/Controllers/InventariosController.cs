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
    public class InventariosController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: Inventarios
        public ActionResult Index()
        {
            //var inventario = db.Inventario.Include(i => i.Bodega).Include(i => i.Insumo).Include(i => i.Producto);
            var inventarioInsumo = db.vInventarioInsumos.ToList();
            var inventarioProducto = db.vInventarioProductos.ToList();
            //return View(inventario.ToList());

            var viewModel = new InventariosViewModel
            {
                vInventarioInsumos = inventarioInsumo,
                vInventarioProductos = inventarioProducto
            };
            return View(viewModel);
        }

        public ActionResult IndexProductos()
        {
            var inventario = db.vInventarioInsumos;
            return View(inventario.ToList());
        }

        // GET: Inventarios/Create
        public ActionResult Create()
        {
            ViewBag.BodegaId = new SelectList(db.Bodega, "BodegaId", "Codigo");
            ViewBag.InsumoId = new SelectList(db.Insumo, "InsumoID", "Descripcion");
            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Descripcion");
            Session["Inventario"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InventarioId,BodegaId,ProductoId,InsumoId,Disponible")] Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                var list = (List<Inventario>)Session["Inventario"];
                var MensajeError = "";
                IEnumerable<object> listaInventario = null;

                if (list != null)
                {
                    if (list.Count != 0)
                    {
                        foreach (Inventario i in list)
                        {

                            listaInventario = db.SP_InsertInventario(
                                i.BodegaId,
                                i.ProductoId,
                                i.InsumoId,
                                i.Disponible
                                );
                            foreach (SP_InsertInventario_Result invent in listaInventario)
                            {
                                MensajeError = invent.MessageError;
                                if (MensajeError.StartsWith("-1"))
                                {
                                  ///Poner que hacer en caso de que ocurriera un error
                                }
                            }
                        }
                    }
                }
                return RedirectToAction("Index");
            }

            ViewBag.BodegaId = new SelectList(db.Bodega, "BodegaId", "Codigo", inventario.BodegaId);
            ViewBag.InsumoId = new SelectList(db.Insumo, "InsumoID", "Descripcion", inventario.InsumoId);
            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Descripcion", inventario.ProductoId);
            return View(inventario);
        }

        [HttpPost]
        public JsonResult InsertInventario(Inventario Inventario)
        {
            List<Inventario> sessionInventario = new List<Inventario>();

            var list = (List<Inventario>)Session["Inventario"];

            if (list == null)
            {
                sessionInventario.Add(Inventario);
                Session["Inventario"] = sessionInventario;
            }
            else
            {
                list.Add(Inventario);
                Session["Inventario"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }
        // GET: Inventarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventario inventario = db.Inventario.Find(id);
            if (inventario == null)
            {
                return HttpNotFound();
            }
            ViewBag.BodegaId = new SelectList(db.Bodega, "BodegaId", "Codigo", inventario.BodegaId);
            ViewBag.InsumoId = new SelectList(db.Insumo, "InsumoID", "Descripcion", inventario.InsumoId);
            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Descripcion", inventario.ProductoId);
            return View(inventario);
        }

        // POST: Inventarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InventarioId,BodegaId,ProductoId,InsumoId,Disponible")] Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BodegaId = new SelectList(db.Bodega, "BodegaId", "Codigo", inventario.BodegaId);
            ViewBag.InsumoId = new SelectList(db.Insumo, "InsumoID", "Descripcion", inventario.InsumoId);
            ViewBag.ProductoId = new SelectList(db.Producto, "ProductoID", "Descripcion", inventario.ProductoId);
            return View(inventario);
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
