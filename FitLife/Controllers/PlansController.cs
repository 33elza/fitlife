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
using System.Diagnostics;


namespace FitLife.Controllers
{
     [Authorize]
    public class PlansController : ApiController
    {
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public PlansController() { }
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
      // GET: api/Users/FollowingPlans
        [HttpGet]
        [Route("api/Users/FollowingPlans")]
        [ResponseType(typeof(PlanDTO))]
        public async Task<IHttpActionResult> GetFollowingPlans()
        {
           // ApplicationUser user = UserManager.FindById(id);
            string userId = User.Identity.GetUserId();
            ApplicationUser user = await UserManager.FindByIdAsync(userId);
           
            if (user == null)
            {
                return null;
            }

            List<Plan> planList = user.FollowingPlans.ToList();

            var plans = Mapper.Map<List<PlanDTO>>(planList);
            return Ok(plans.AsQueryable<PlanDTO>());
        }

       // GET: api/Plans
        [HttpGet]
        [Route("api/Plans")]
        [ResponseType(typeof(PlanDTO))]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetPlans()
        {
            var plans = db.Plans;
            foreach (Plan plan in plans)
            {
                plan.Author = UserManager.FindById(plan.AuthorID);
            }
            return Ok(Mapper.Map<Plan[], IEnumerable<PlanDTO>>(await plans.ToArrayAsync()));
        }

        // GET: api/Plans/5
        [ResponseType(typeof(PlanDTO))]
        public async Task<IHttpActionResult> GetPlan(int id)
        {
            
            Plan plan = await db.Plans.FindAsync(id);
            plan.Author = UserManager.FindById(plan.AuthorID);        
            if (plan == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Plan,PlanDTO>(plan));
        }

        // GET: api/Plans
        [HttpGet]
        [Route("api/Plans/by_categories/{difficultylevel}/{sex}")]
        public IQueryable<PlanDTO> FindPlanByCategories(string DifficultyLevel, string Sex)
        {
            var plans = new List<PlanDTO>();
            foreach (Plan plan in db.Plans)
            {
                if ((plan.DifficultyLevel == DifficultyLevel) && (plan.Sex == Sex))
                {
                     plans.Add(Mapper.Map<PlanDTO>(plan));
                }
            }
            return plans.AsQueryable<PlanDTO>();
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