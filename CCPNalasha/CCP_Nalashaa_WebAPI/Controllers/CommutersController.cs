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
using CCP_Nalashaa_WebAPI.Models;

namespace CCP_Nalashaa_WebAPI.Controllers
{
    public class CommutersController : ApiController
    {
        private CCPNalashaaEntities db = new CCPNalashaaEntities();

        // GET: api/Commuters
        public IQueryable<Commuter> GetCommuters()
        {
            return db.Commuters;
        }

        // GET: api/Commuters/5
        [ResponseType(typeof(Commuter))]
        public IHttpActionResult GetCommuter(int id)
        {
            Commuter commuter = db.Commuters.Find(id);
            if (commuter == null)
            {
                return NotFound();
            }

            return Ok(commuter);
        }

        // PUT: api/Commuters/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCommuter(int id, Commuter commuter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != commuter.CommuterId)
            {
                return BadRequest();
            }

            db.Entry(commuter).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommuterExists(id))
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

        // POST: api/Commuters
        [ResponseType(typeof(Commuter))]
        public IHttpActionResult PostCommuter(Commuter commuter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Commuters.Add(commuter);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = commuter.CommuterId }, commuter);
        }

        // DELETE: api/Commuters/5
        [ResponseType(typeof(Commuter))]
        public IHttpActionResult DeleteCommuter(int id)
        {
            Commuter commuter = db.Commuters.Find(id);
            if (commuter == null)
            {
                return NotFound();
            }

            db.Commuters.Remove(commuter);
            db.SaveChanges();

            return Ok(commuter);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommuterExists(int id)
        {
            return db.Commuters.Count(e => e.CommuterId == id) > 0;
        }
    }
}