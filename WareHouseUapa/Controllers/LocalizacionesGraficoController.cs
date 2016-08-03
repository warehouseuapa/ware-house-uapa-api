using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WareHouseUapa.Models;

namespace WareHouseUapa.Controllers
{
    public class LocalizacionesGraficoController : Controller
    {
        private warehouseuapaEntitiesAzure db = new warehouseuapaEntitiesAzure();

        // GET: LocalizacionesGrafico
        public ActionResult Index()
        {
            return View(db.localizaciones.ToList());
        }

        // GET: LocalizacionesGrafico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            localizaciones localizaciones = db.localizaciones.Find(id);
            if (localizaciones == null)
            {
                return HttpNotFound();
            }
            return View(localizaciones);
        }

        // GET: LocalizacionesGrafico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LocalizacionesGrafico/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,localizacion")] localizaciones localizaciones)
        {
            if (ModelState.IsValid)
            {
                db.localizaciones.Add(localizaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(localizaciones);
        }

        // GET: LocalizacionesGrafico/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            localizaciones localizaciones = db.localizaciones.Find(id);
            if (localizaciones == null)
            {
                return HttpNotFound();
            }
            return View(localizaciones);
        }

        // POST: LocalizacionesGrafico/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,localizacion")] localizaciones localizaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(localizaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(localizaciones);
        }

        // GET: LocalizacionesGrafico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            localizaciones localizaciones = db.localizaciones.Find(id);
            if (localizaciones == null)
            {
                return HttpNotFound();
            }
            return View(localizaciones);
        }

        // POST: LocalizacionesGrafico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            localizaciones localizaciones = db.localizaciones.Find(id);
            db.localizaciones.Remove(localizaciones);
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
