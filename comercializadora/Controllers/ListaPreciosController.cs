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
    public class ListaPreciosController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: ListaPrecios
        public ActionResult Index()
        {
            return View(db.ListaPrecio.ToList());
        }

        // GET: ListaPrecios/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaPrecio listaPrecio = db.ListaPrecio.Find(id);
            if (listaPrecio == null)
            {
                return HttpNotFound();
            }
            return View(listaPrecio);
        }

        // GET: ListaPrecios/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo,Descripcion")] ListaPrecio listaPrecio)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;

                list = db.SP_InsertListaPrecio(listaPrecio.Codigo,
                                               listaPrecio.Descripcion);
                foreach (SP_InsertListaPrecio_Result tbListaPrecio in list)
                    MensajeError = tbListaPrecio.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }

                return RedirectToAction("Index");
            }

            return View(listaPrecio);
        }

        // GET: ListaPrecios/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaPrecio listaPrecio = db.ListaPrecio.Find(id);
            if (listaPrecio == null)
            {
                return HttpNotFound();
            }
            return View(listaPrecio);
        }

        // POST: ListaPrecios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo,Descripcion")] ListaPrecio listaPrecio)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;

                list = db.SP_UpdateListaPrecio(listaPrecio.Codigo,
                                               listaPrecio.Descripcion);
                foreach (SP_UpdateListaPrecio_Result tbListaPrecio in list)
                    MensajeError = tbListaPrecio.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: ListaPrecios/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListaPrecio listaPrecio = db.ListaPrecio.Find(id);
            if (listaPrecio == null)
            {
                return HttpNotFound();
            }
            return View(listaPrecio);
        }

        // POST: ListaPrecios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ListaPrecio listaPrecio = db.ListaPrecio.Find(id);
            db.ListaPrecio.Remove(listaPrecio);
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
