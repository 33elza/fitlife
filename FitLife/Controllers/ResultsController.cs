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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace FitLife.Controllers
{
    public class ResultsController : ApiController
    {
        private ApplicationUserManager _userManager;
        public ResultsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
     public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Results
        public IQueryable<Result> GetResults()
        {
            return db.Results;
        }

        // GET: api/Results/5
        [ResponseType(typeof(Result))]
        public async Task<IHttpActionResult> GetResult(int id)
        {
            Result result = await db.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Results/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutResult(int id, Result result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != result.ID)
            {
                return BadRequest();
            }

            db.Entry(result).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultExists(id))
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

        // POST: api/Results
        [ResponseType(typeof(Result))]
        public async Task<IHttpActionResult> PostResult(Result result)
        {
            result.UserID = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Results.Add(result);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = result.ID }, result);
        }

        // DELETE: api/Results/5
        [ResponseType(typeof(Result))]
        public async Task<IHttpActionResult> DeleteResult(int id)
        {
            Result result = await db.Results.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            db.Results.Remove(result);
            await db.SaveChangesAsync();

            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResultExists(int id)
        {
            return db.Results.Count(e => e.ID == id) > 0;
        }
    }
}