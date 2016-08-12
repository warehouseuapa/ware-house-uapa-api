using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using WareHouseUapa.Models;
using System.Web.Http;

namespace WareHouseUapa.Controllers
{
    public class ProductosController : ApiController
    {
        private warehouseuapaEntitiesAzure db = new warehouseuapaEntitiesAzure();

        [System.Web.Http.Route("api/getProductByItemAndLocation")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult getProductByItemAndLocation(string item, string location)
        {
            Productos productos = db.Productos.FirstOrDefault(p => p.codigo == item && p.localizacion == location);
            if (productos == null)
            {
                return Json(new { errorCode = 0, mensaje = "No se enctontro." });
            }

            return Ok(productos);
        }

        [System.Web.Http.Route("api/getProductByLocation")]
        [System.Web.Http.HttpGet]
        public IQueryable<Productos> getProductByLocation(string location)
        {
            return  db.Productos.Where(p => p.localizacion == location);
        }

        [System.Web.Http.Route("api/getProductsByCodigo")]
        [System.Web.Http.HttpGet]
        public IQueryable<Productos> getProductsByCodigo(string codigo)
        {
           return  db.Productos.Where(p => p.codigo == codigo);
            
        }

        // GET: api/Productos
        public IQueryable<Productos> GetProductos()
        {
            return db.Productos;
        }

        // GET: api/Productos/5
        [ResponseType(typeof(Productos))]
        public IHttpActionResult GetProductos(string codigo)
        {
            Productos productos = db.Productos.FirstOrDefault(p => p.codigo == codigo);
            if (productos == null)
            {
                return NotFound();
            }

            return Ok(productos);
        }

        // PUT: api/Productos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductos(int id, Productos productos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productos.id)
            {
                return BadRequest();
            }

            db.Entry(productos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductosExists(id))
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

        // POST: api/Productos
        [ResponseType(typeof(Productos))]
        public IHttpActionResult PostProductos(Productos productos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existe = db.Productos.Where(p => p.codigo == productos.codigo && p.localizacion == productos.localizacion);

            if (existe.Count() > 0)
            {
                return Json(new { errorCode = 1, message = "Ya existe un producto con ese codigo en esa localizacion" });
            }

            db.Productos.Add(productos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productos.id }, productos);
        }

        // DELETE: api/Productos/5
        [ResponseType(typeof(Productos))]
        public IHttpActionResult DeleteProductos(int id)
        {
            Productos productos = db.Productos.Find(id);
            if (productos == null)
            {
                return NotFound();
            }

            db.Productos.Remove(productos);
            db.SaveChanges();

            return Ok(productos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductosExists(int id)
        {
            return db.Productos.Count(e => e.id == id) > 0;
        }
    }
}