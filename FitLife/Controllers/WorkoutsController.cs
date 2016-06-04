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
using FitLife.Models.DTO;
using AutoMapper;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace FitLife.Controllers
{
    public class WorkoutsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

         public WorkoutsController() { }
         public WorkoutsController(ApplicationUserManager userManager)
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

        // GET: api/Plans/2/PlansWorkouts
        [Route("api/Plans/{id}/PlansWorkouts")]
        [ResponseType(typeof(WorkoutDTO))]
        public async Task<IHttpActionResult> GetPlansWorkouts(int id)
        {
            Plan plan = await db.Plans.FindAsync(id);
            plan.Author = UserManager.FindById(plan.AuthorID);
            if (plan == null)
            {
                return NotFound();
            }

            GetWorkoutsWithSets(plan);

            List<Workout> workoutList = plan.Workouts.ToList();
           foreach (Workout workout in workoutList)
           {
                var sets = Mapper.Map<List<SetDTO>>(workout.Sets);
           }
            var workouts = Mapper.Map<List<WorkoutDTO>>(workoutList);

            return Ok(workouts);    
        }

        public void GetWorkoutsWithSets(Plan plan)
        {

            foreach (Workout workout in db.Workouts)
            {
                if (workout.PlanID == plan.ID)
                {
                    plan.Workouts.Add(workout);

                }
            }
            foreach (Workout workout in plan.Workouts)
            {
                foreach (Set set in db.Sets)
                {
                    if (set.WorkoutID == workout.ID)
                        workout.Sets.Add(set);
                }
            }
        }

        // GET: api/Workouts
        public IQueryable<Workout> GetWorkouts()
        {
            return db.Workouts;
        }

        // GET: api/Workouts/5
        [ResponseType(typeof(Workout))]
        public async Task<IHttpActionResult> GetWorkout(int id)
        {
            Workout workout = await db.Workouts.FindAsync(id);
            if (workout == null)
            {
                return NotFound();
            }

            return Ok(workout);
        }

        // PUT: api/Workouts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWorkout(int id, Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workout.ID)
            {
                return BadRequest();
            }

            db.Entry(workout).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(id))
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

        // POST: api/Workouts
        [ResponseType(typeof(Workout))]
        public async Task<IHttpActionResult> PostWorkout(Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Workouts.Add(workout);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = workout.ID }, workout);
        }

        // DELETE: api/Workouts/5
        [ResponseType(typeof(Workout))]
        public async Task<IHttpActionResult> DeleteWorkout(int id)
        {
            Workout workout = await db.Workouts.FindAsync(id);
            if (workout == null)
            {
                return NotFound();
            }

            db.Workouts.Remove(workout);
            await db.SaveChangesAsync();

            return Ok(workout);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkoutExists(int id)
        {
            return db.Workouts.Count(e => e.ID == id) > 0;
        }
    }
}