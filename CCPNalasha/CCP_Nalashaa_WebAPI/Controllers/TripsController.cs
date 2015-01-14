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
using System.Web.Http.Cors;

namespace CCP_Nalashaa_WebAPI.Controllers
{
    [EnableCors("http://localhost", "*", "*")]
    public class TripsController : ApiController
    {
        private CCPNalashaaEntities db = new CCPNalashaaEntities();

        // GET: api/Trips
        public IQueryable<Trip> GetTrips()
        {
            return db.Trips;
        }

        // GET: api/Trips/5
        [ResponseType(typeof(Trip))]
        public IHttpActionResult GetTrip(int id)
        {
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return NotFound();
            }

            return Ok(trip);
        }

        public IQueryable<Trip> GetTrips(double source_lat, double source_ln, double destination_lat, double destination_ln)
        {
            //"Origin_Lat":25.75949409,"Origin_Ln":35.7594049,"Destination_Lat":23.574939,"Destination_Ln":23.239404,
            source_lat = 25.75949409;
            source_ln = 35.7594049;
            destination_lat = 23.574939;
            destination_ln = 23.239404;

            var allTrips = db.Trips;
            var trips = GetTrips().Where(x => x.Origin_Lat.Value.TruncateDecimalPlaces(2) == source_lat && x.Origin_Ln.Value.TruncateDecimalPlaces(2) == source_ln && x.Destination_Lat.Value.TruncateDecimalPlaces(2) == destination_lat && x.Destination_Ln.Value.TruncateDecimalPlaces(2) == destination_ln);

            //GeoHelper gp = new GeoHelper();

            return trips;
        }

        // PUT: api/Trips/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTrip(int id, Trip trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trip.TripId)
            {
                return BadRequest();
            }

            db.Entry(trip).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TripExists(id))
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

        // POST: api/Trips
        
        [ResponseType(typeof(Trip))]
        [HttpPost()]
        public IHttpActionResult PostTrip(Trip trip)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var context = new ApplicationDbContext())
            {
                db.Trips.Add(trip);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = trip.TripId }, trip);
            }
        }

        // DELETE: api/Trips/5
        [ResponseType(typeof(Trip))]
        public IHttpActionResult DeleteTrip(int id)
        {
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return NotFound();
            }

            db.Trips.Remove(trip);
            db.SaveChanges();

            return Ok(trip);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TripExists(int id)
        {
            return db.Trips.Count(e => e.TripId == id) > 0;
        }
    }
}