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
    public class CommuterRequestsController : ApiController
    {
        private CCPNalashaaEntities db = new CCPNalashaaEntities();

        // GET: api/CommuterRequests
        public IQueryable<CommuterRequest> GetCommuterRequests()
        {
            return db.CommuterRequests;
        }

        // GET: api/CommuterRequests/5
        [ResponseType(typeof(CommuterRequest))]
        public IHttpActionResult GetCommuterRequest(int id)
        {
            CommuterRequest commuterRequest = db.CommuterRequests.Find(id);
            if (commuterRequest == null)
            {
                return NotFound();
            }

            return Ok(commuterRequest);
        }

        // PUT: api/CommuterRequests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCommuterRequest(int id, CommuterRequest commuterRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != commuterRequest.CommuterRequestId)
            {
                return BadRequest();
            }

            db.Entry(commuterRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommuterRequestExists(id))
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

        // POST: api/CommuterRequests
        [ResponseType(typeof(CommuterRequest))]
        public IHttpActionResult PostCommuterRequest(CommuterRequest commuterRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CommuterRequests.Add(commuterRequest);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = commuterRequest.CommuterRequestId }, commuterRequest);
        }

        // DELETE: api/CommuterRequests/5
        [ResponseType(typeof(CommuterRequest))]
        public IHttpActionResult DeleteCommuterRequest(int id)
        {
            CommuterRequest commuterRequest = db.CommuterRequests.Find(id);
            if (commuterRequest == null)
            {
                return NotFound();
            }

            db.CommuterRequests.Remove(commuterRequest);
            db.SaveChanges();

            return Ok(commuterRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommuterRequestExists(int id)
        {
            return db.CommuterRequests.Count(e => e.CommuterRequestId == id) > 0;
        }
    }
}