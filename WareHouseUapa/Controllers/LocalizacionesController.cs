using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WareHouseUapa.Models;

namespace WareHouseUapa.Controllers
{
    public class LocalizacionesController : ApiController
    {
        private warehouseuapaEntities1 db = new warehouseuapaEntities1();

        // GET: api/Localizaciones
        public IQueryable<localizaciones> Getlocalizaciones()
        {
            return db.localizaciones;
        }

        // GET: api/Localizaciones/5
        [ResponseType(typeof(localizaciones))]
        public IHttpActionResult Getlocalizaciones(int id)
        {
            localizaciones localizaciones = db.localizaciones.Find(id);
            if (localizaciones == null)
            {
                return NotFound();
            }

            return Ok(localizaciones);
        }

        // PUT: api/Localizaciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlocalizaciones(int id, localizaciones localizaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != localizaciones.id)
            {
                return BadRequest();
            }

            db.Entry(localizaciones).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!localizacionesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Localizaciones
        [ResponseType(typeof(localizaciones))]
        public IHttpActionResult Postlocalizaciones(localizaciones localizaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.localizaciones.Add(localizaciones);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = localizaciones.id }, localizaciones);
        }

        // DELETE: api/Localizaciones/5
        [ResponseType(typeof(localizaciones))]
        public IHttpActionResult Deletelocalizaciones(int id)
        {
            localizaciones localizaciones = db.localizaciones.Find(id);
            if (localizaciones == null)
            {
                return NotFound();
            }

            db.localizaciones.Remove(localizaciones);
            db.SaveChanges();

            return Ok(localizaciones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool localizacionesExists(int id)
        {
            return db.localizaciones.Count(e => e.id == id) > 0;
        }
    }
}