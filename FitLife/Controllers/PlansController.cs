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
using FitLife.Models;
using FitLife.Models.DTO;
using AutoMapper;
using System.Collections;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;



namespace FitLife.Controllers
{
    public class PlansController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public PlansController()  { }
        public PlansController(ApplicationUserManager userManager)
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

        // GET: api/Users/051c9cf0-48d1-48dd-a58f-fcc6572c740b/FollowingPlans
        [Route("api/Users/{id}/FollowingPlans")]
        public IQueryable<PlanDTO> GetFollowingPlans(string id)
        {
            ApplicationUser user = UserManager.FindById(id);
            if (user == null)
            {
                return null;
            }

            List<Plan> list = user.FollowingPlans.ToList();

            var plans = Mapper.Map<List<PlanDTO>>(list);
            return plans.AsQueryable<PlanDTO>();

        
        }

        // GET: api/Plans
        public IQueryable<Plan> GetPlans()
        {
            return db.Plans;
        }

        // GET: api/Plans/5
        [ResponseType(typeof(Plan))]
        public async Task<IHttpActionResult> GetPlan(int id)
        {
            Plan plan = await db.Plans.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }

            return Ok(plan);
        }

        // PUT: api/Plans/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlan(int id, Plan plan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plan.ID)
            {
                return BadRequest();
            }

            db.Entry(plan).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlanExists(id))
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

        // POST: api/Plans
        [ResponseType(typeof(Plan))]
        public async Task<IHttpActionResult> PostPlan(Plan plan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Plans.Add(plan);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = plan.ID }, plan);
        }

        // DELETE: api/Plans/5
        [ResponseType(typeof(Plan))]
        public async Task<IHttpActionResult> DeletePlan(int id)
        {
            Plan plan = await db.Plans.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }

            db.Plans.Remove(plan);
            await db.SaveChangesAsync();

            return Ok(plan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlanExists(int id)
        {
            return db.Plans.Count(e => e.ID == id) > 0;
        }
    }
}