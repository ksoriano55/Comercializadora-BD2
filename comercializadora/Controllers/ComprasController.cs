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
    public class ComprasController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: Compras
        public ActionResult Index()
        {
            var compra = db.Compra.Include(c => c.Proveedor);
            return View(compra.ToList());
        }

        // GET: Compras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            return View(compra);
        }

        [HttpPost]

        public JsonResult InsertCompraDetalle(CompraDetalle compraDetalle)
        {
            List<CompraDetalle> sessionCompraDetalle = new List<CompraDetalle>();

            var list = (List<CompraDetalle>)Session["CompraDetalle"];

            if (list == null)
            {
                sessionCompraDetalle.Add(compraDetalle);
                Session["CompraDetalle"] = sessionCompraDetalle;
            }
            else
            {
                list.Add(compraDetalle);
                Session["CompraDetalle"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        // GET: Compras/Create
        public ActionResult Create()
        {
            ViewBag.ProveedorID = new SelectList(db.Proveedor, "ProveedorID", "Nombre");
            ViewBag.InsumoId = new SelectList(db.Insumo, "InsumoID", "Descripcion");
            Session["CompraDetalle"] = null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompraID,ProveedorID,CodigoCompra,ValorCompra,SaldoPendiente,Fecha,FechaVencimiento")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;
                var listaCompraDetalle = (List<CompraDetalle>)Session["CompraDetalle"];
                var MensajeErrorDetalle = "";
                IEnumerable<object> listaDetalle = null;
                list = db.SP_InsertCompra(compra.ProveedorID,0 ,0);
                foreach (SP_InsertCompra_Result result in list)
                    MensajeError = result.MensajeError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (listaCompraDetalle != null)
                    {
                        if (listaCompraDetalle.Count != 0)
                        {
                            foreach (CompraDetalle compraDetalle in listaCompraDetalle)
                            {
                                compraDetalle.CompraID = Convert.ToInt32(MensajeError);
                                listaDetalle = db.SP_InsertCompraDetalle(
                                    compraDetalle.CompraID,
                                    compraDetalle.InsumoId,
                                    compraDetalle.Cantidad,
                                    compra.ProveedorID);
                                foreach (SP_InsertCompraDetalle_Result result in listaDetalle)
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

                return RedirectToAction("Index");
            }

            ViewBag.ProveedorID = new SelectList(db.Proveedor, "ProveedorID", "Nombre", compra.ProveedorID);
            return View(compra);
        }
      
        // GET: Compras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compra compra = db.Compra.Find(id);
            if (compra == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProveedorID = new SelectList(db.Proveedor, "ProveedorID", "Nombre", compra.ProveedorID);
            return View(compra);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompraID,ProveedorID,CodigoCompra,ValorCompra,SaldoPendiente,Fecha,FechaVencimiento")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProveedorID = new SelectList(db.Proveedor, "ProveedorID", "Nombre", compra.ProveedorID);
            return View(compra);
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
