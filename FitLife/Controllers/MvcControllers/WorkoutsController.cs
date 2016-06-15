using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FitLife.Models.DBModels;

namespace FitLife.Controllers.MvcControllers
{
    public class WorkoutsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Workouts
        public async Task<ActionResult> Index()
        {
            var workouts = db.Workouts.Include(w => w.Plan);
            return View(await workouts.ToListAsync());
        }

        // GET: Workouts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = await db.Workouts.FindAsync(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            return View(workout);
        }

        // GET: Workouts/Create
        public ActionResult AddWorkout()
        {
            ViewBag.PlanID = new SelectList(db.Plans, "ID", "Name");
            return View();
        }

        // POST: Workouts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddWorkout([Bind(Include = "ID,Date,Description,PlanID")] Workout workout, int planID)
        {
            workout.PlanID = planID;
            workout.Plan = db.Plans.Find(planID);
          //  workout.Date = DateTime.Now;
           
            if ((ModelState.IsValid))
            {
                db.Workouts.Add(workout);
                await db.SaveChangesAsync();
                return RedirectToAction("PlansWorkouts", "Plans", new { id = planID });
            }

            ViewBag.PlanID = new SelectList(db.Plans, "ID", "Name", workout.PlanID);
            return View(workout);
        }

        // GET: Workouts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = await db.Workouts.FindAsync(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlanID = new SelectList(db.Plans, "ID", "Name", workout.PlanID);
            return View(workout);
        }

        // POST: Workouts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Date,Description,PlanID")] Workout workout)
        {
            int planID = workout.PlanID;
            workout.Plan = await db.Plans.FindAsync(planID);
            if (!(ModelState.IsValid))
            {
                db.Entry(workout).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("PlansWorkouts", "Plans", new { id = workout.PlanID });
            }
            ViewBag.PlanID = new SelectList(db.Plans, "ID", "Name", workout.PlanID);
            return View(workout);
        }

        // GET: Workouts/WorkoutsSets/5
        public async Task<ActionResult> WorkoutsSets (int? id)
        {
            Workout workout = await db.Workouts.FindAsync(id);
               
            if (workout == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkoutID = id;
            
            var sets = db.Sets.Where(c => c.WorkoutID == id);
            foreach (Set set in sets.ToList())
            {
                set.Exercise = db.Exercises.Find(set.ExerciseID);
            }
            return View(sets);
        }

        // GET: Workouts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Workout workout = await db.Workouts.FindAsync(id);
            if (workout == null)
            {
                return HttpNotFound();
            }
            return View(workout);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Workout workout = await db.Workouts.FindAsync(id);
            db.Workouts.Remove(workout);
            await db.SaveChangesAsync();
            return RedirectToAction("PlansWorkouts", "Plans", new { id = workout.PlanID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
