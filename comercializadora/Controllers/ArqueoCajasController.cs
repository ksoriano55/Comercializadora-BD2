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
    public class ArqueoCajasController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: ArqueoCajas
        public ActionResult Index()
        {
            var arqueoCaja = db.ArqueoCaja.Include(a => a.Cajero);
            return View(arqueoCaja.ToList());
        }

        // GET: ArqueoCajas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArqueoCaja arqueoCaja = db.ArqueoCaja.Find(id);
            if (arqueoCaja == null)
            {
                return HttpNotFound();
            }
            return View(arqueoCaja);
        }

        public JsonResult getDenominacion()
        {
            var listaCompras = db.Denominacion.Select(x => new SelectListItem
            {
                Text =x.Tipo + " - " + x.Denominación,
                Value = x.Codigo.ToString()
            }).ToList();

            return Json(listaCompras, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertEfectivoArqueo(EfectivoArqueo efectivoArqueo)
        {
            List<EfectivoArqueo> sessionEfectivoArqueo = new List<EfectivoArqueo>();

            var list = (List<EfectivoArqueo>)Session["EfectivoArqueo"];

            if (list == null)
            {
                sessionEfectivoArqueo.Add(efectivoArqueo);
                Session["EfectivoArqueo"] = sessionEfectivoArqueo;
            }
            else
            {
                list.Add(efectivoArqueo);
                Session["EfectivoArqueo"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertChequeArqueo(ChequeArqueo chequeArqueo)
        {
            List<ChequeArqueo> sessionEfectivoArqueo = new List<ChequeArqueo>();

            var list = (List<ChequeArqueo>)Session["ChequeArqueo"];

            if (list == null)
            {
                sessionEfectivoArqueo.Add(chequeArqueo);
                Session["ChequeArqueo"] = sessionEfectivoArqueo;
            }
            else
            {
                list.Add(chequeArqueo);
                Session["ChequeArqueo"] = list;
            }
            return Json("Exito", JsonRequestBehavior.AllowGet);
        }

        // GET: ArqueoCajas/Create
        public ActionResult Create()
        {
            ViewBag.CajeroID = new SelectList(db.Cajero, "CajeroID", "Nombre");
            ViewBag.Denominacion = new SelectList(db.Denominacion, "Codigo", "Denominacion");
            ViewBag.bancoId = new SelectList(db.Cajero, "bancoId", "Descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CodigoArqueo,FechaInicio,FechaFinal,CajeroID")] ArqueoCaja arqueoCaja)
        {
            if (ModelState.IsValid)
            {
                db.ArqueoCaja.Add(arqueoCaja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CajeroID = new SelectList(db.Cajero, "CajeroID", "Codigo", arqueoCaja.CajeroID);
            return View(arqueoCaja);
        }

        // GET: ArqueoCajas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArqueoCaja arqueoCaja = db.ArqueoCaja.Find(id);
            if (arqueoCaja == null)
            {
                return HttpNotFound();
            }
            ViewBag.CajeroID = new SelectList(db.Cajero, "CajeroID", "Codigo", arqueoCaja.CajeroID);
            return View(arqueoCaja);
        }

        // POST: ArqueoCajas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CodigoArqueo,FechaInicio,FechaFinal,CajeroID")] ArqueoCaja arqueoCaja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arqueoCaja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CajeroID = new SelectList(db.Cajero, "CajeroID", "Codigo", arqueoCaja.CajeroID);
            return View(arqueoCaja);
        }

        // GET: ArqueoCajas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArqueoCaja arqueoCaja = db.ArqueoCaja.Find(id);
            if (arqueoCaja == null)
            {
                return HttpNotFound();
            }
            return View(arqueoCaja);
        }

        // POST: ArqueoCajas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ArqueoCaja arqueoCaja = db.ArqueoCaja.Find(id);
            db.ArqueoCaja.Remove(arqueoCaja);
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
