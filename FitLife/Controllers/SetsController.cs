using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FitLife.Models.DBModels;

namespace FitLife.Controllers
{
    public class SetsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
     // GET: api/Sets
        public IQueryable<Set> GetSets()
        {
            return db.Sets;
        }
     // GET: api/Sets/5
        [ResponseType(typeof(Set))]
        public async Task<IHttpActionResult> GetSet(int id)
        {
            Set set = await db.Sets.FindAsync(id);
            if (set == null)
            {
                return NotFound();
            }

            return Ok(set);
        }
       // PUT: api/Sets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSet(int id, Set set)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != set.ID)
            {
                return BadRequest();
            }

            db.Entry(set).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetExists(id))
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
       // POST: api/Sets
        [ResponseType(typeof(Set))]
        public async Task<IHttpActionResult> PostSet(Set set)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sets.Add(set);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = set.ID }, set);
        }
       // DELETE: api/Sets/5
        [ResponseType(typeof(Set))]
        public async Task<IHttpActionResult> DeleteSet(int id)
        {
            Set set = await db.Sets.FindAsync(id);
            if (set == null)
            {
                return NotFound();
            }

            db.Sets.Remove(set);
            await db.SaveChangesAsync();

            return Ok(set);
        }
       protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SetExists(int id)
        {
            return db.Sets.Count(e => e.ID == id) > 0;
        }
    }
}