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
using FitLife.Models;
using FitLife.Models.DBModels;
using FitLife.Models.DTO;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;

namespace FitLife.Controllers
{
    public class UsersController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public UsersController()
        { }

        public UsersController(ApplicationUserManager userManager)
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

        // GET: api/Users
        [HttpGet]
        [Route("api/Users")]
        [ResponseType(typeof(UserProfileDTO))]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetApplicationUsers()
        {
            return Ok(Mapper.Map<ApplicationUser[], IEnumerable<UserProfileDTO>>(await UserManager.Users.ToArrayAsync()));
        }

     /*   // GET: api/Users/5
        [ResponseType(typeof(UserProfileDTO))]
        public async Task<IHttpActionResult> GetApplicationUser(string id)
        {
            ApplicationUser applicationUser = await db.ApplicationUsers.FindAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutApplicationUser(string id, ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationUser.Id)
            {
                return BadRequest();
            }

            db.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(ApplicationUser))]
        public async Task<IHttpActionResult> PostApplicationUser(ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ApplicationUsers.Add(applicationUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApplicationUserExists(applicationUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = applicationUser.Id }, applicationUser);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(ApplicationUser))]
        public async Task<IHttpActionResult> DeleteApplicationUser(string id)
        {
            ApplicationUser applicationUser = await db.ApplicationUsers.FindAsync(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            db.ApplicationUsers.Remove(applicationUser);
            await db.SaveChangesAsync();

            return Ok(applicationUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationUserExists(string id)
        {
            return db.ApplicationUsers.Count(e => e.Id == id) > 0;
        }*/
    }
}