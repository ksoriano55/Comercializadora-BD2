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
    public class ProductorController : Controller
    {
        private ComercializadoraDBEntities db = new ComercializadoraDBEntities();

        // GET: Productor
        public ActionResult Index()
        {
            var productor = db.Productor.Include(p => p.CuentaBancaria);
            return View(productor.ToList());
        }

        // GET: Productor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productor productor = db.Productor.Find(id);
            if (productor == null)
            {
                return HttpNotFound();
            }
            return View(productor);
        }

        // GET: Productor/Create
        public ActionResult Create()
        {
            ViewBag.CuentaBancariaID = new SelectList(db.CuentaBancaria, "CuantaID", "Banco");
            return View();
        }

        // POST: Productor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductorID,Nombre,Identidad,RTN,Telefono,EMail,SaldoDisponible,DiasCredito,CuentaBancariaID")] Productor productor)
        {
            if (ModelState.IsValid)
            {
                var MensajeError = "";
                IEnumerable<object> list;

                list = db.SP_InsertProductores( productor.Nombre,
                                                productor.Identidad,
                                                productor.RTN,
                                                productor.Telefono,
                                                productor.EMail,
                                                productor.SaldoDisponible,
                                                productor.DiasCredito,
                                                productor.CuentaBancariaID);
                foreach (SP_InsertProductores_Result tbProductores in list)
                    MensajeError = tbProductores.MessageError;
                if (MensajeError.StartsWith("-1"))
                {
                    return Json("No se pudo registrar, favor contacte al administrador.", JsonRequestBehavior.AllowGet);
                }
                return RedirectToAction("Index");
            }

            ViewBag.CuentaBancariaID = new SelectList(db.CuentaBancaria, "CuantaID", "Banco", productor.CuentaBancariaID);
            return View(productor);
        }

        // GET: Productor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productor productor = db.Productor.Find(id);
            if (productor == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuentaBancariaID = new SelectList(db.CuentaBancaria, "CuantaID", "Banco", productor.CuentaBancariaID);
            return View(productor);
        }

        // POST: Productor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductorID,Nombre,Identidad,RTN,Telefono,EMail,SaldoDisponible,DiasCredito,CuentaBancariaID")] Productor productor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CuentaBancariaID = new SelectList(db.CuentaBancaria, "CuantaID", "Banco", productor.CuentaBancariaID);
            return View(productor);
        }

        // GET: Productor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productor productor = db.Productor.Find(id);
            if (productor == null)
            {
                return HttpNotFound();
            }
            return View(productor);
        }

        // POST: Productor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Productor productor = db.Productor.Find(id);
            db.Productor.Remove(productor);
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
