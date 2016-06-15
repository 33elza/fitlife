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
    public class SetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Sets
        public async Task<ActionResult> Index()
        {
            var sets = db.Sets.Include(s => s.Workout);
            return View(await sets.ToListAsync());
        }
        // GET: Sets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Set set = await db.Sets.FindAsync(id);
            if (set == null)
            {
                return HttpNotFound();
            }
            return View(set);
        }
        // GET: Sets/Create
        public ActionResult Create()
        {
            ViewBag.WorkoutID = new SelectList(db.Workouts, "ID", "Description");
            ViewData["Exercises"] = new SelectList(db.Exercises, "ID", "ExcerciseName", "Description");
            ViewBag.Exercises = new SelectList(db.Exercises, "ID", "ExcerciseName", "Description");
            return View();
        }
        // POST: Sets/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Weight,Quantity,Time,Description,WorkoutID,ResultID,ExerciseID")] Set set, int workoutID)
        {
            set.WorkoutID = workoutID;
            set.Workout = await db.Workouts.FindAsync(workoutID);
            
           
           // set.ExerciseID = set.Exercise.ID;
            set.Exercise = db.Exercises.Find(set.ExerciseID);
            
          
            if (ModelState.IsValid)
            {
                db.Sets.Add(set);
                await db.SaveChangesAsync();         
                return RedirectToAction("WorkoutsSets", "Workouts", new { id = workoutID });
            }

            ViewBag.WorkoutID = workoutID;
            return View(set);
        }
        //public Result CreateResult(Set set)
        //{
        //    var result = new Result();
        //    result.Set = set;
        //    result.SetID = set.ID;
        //    db.Results.Add(result);
        //    db.SaveChanges();
        //    return (result);
        //}
        // GET: Sets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Set set = await db.Sets.FindAsync(id);
            if (set == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkoutID = new SelectList(db.Workouts, "ID", "Description", set.WorkoutID);
            return View(set);
        }
        // POST: Sets/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Weight,Quantity,Time,Description,WorkoutID")] Set set)
        {
            int workoutID = set.WorkoutID;
            set.Workout = await db.Workouts.FindAsync(workoutID);
            if (ModelState.IsValid)
            {
                db.Entry(set).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("WorkoutsSets", "Workouts", new { id = set.WorkoutID });
            }
            ViewBag.WorkoutID = new SelectList(db.Workouts, "ID", "Description", set.WorkoutID);
            return View(set);
        }
        // GET: Sets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Set set = await db.Sets.FindAsync(id);
            if (set == null)
            {
                return HttpNotFound();
            }
            return View(set);
        }
        // POST: Sets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Set set = await db.Sets.FindAsync(id);
            db.Sets.Remove(set);
            await db.SaveChangesAsync();
            return RedirectToAction("WorkoutsSets", "Workouts", new { id = set.WorkoutID });
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
