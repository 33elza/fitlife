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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using FitLife.Infrastructure;
using FitLife.Models;

namespace FitLife.Controllers.MvcControllers
{
    public class ExercisesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
         public ExercisesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
         public ExercisesController()
        {

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

        // GET: Exercises
        public ActionResult Index()
        {
            ApplicationUser user =  UserManager.FindById(User.Identity.GetUserId());
            var exercises = user.Exercises;
            return View(exercises);
        }

        // GET: Exercises/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = await db.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // GET: Exercises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,ExcerciseName,Description")] Exercise exercise)
        {
            exercise.AuthorID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Exercises.Add(exercise);
                await db.SaveChangesAsync();

                HttpPostedFileBase hpf = Request.Files["imagefile"] as HttpPostedFileBase;
                UploadImage up = new UploadImage();

                string exerciseName = Convert.ToString(exercise.ID);

                up.SaveImage(hpf, exerciseName);

                return RedirectToAction("Index");
            }

            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = await db.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,ExcerciseName,Description")] Exercise exercise)
        {
            exercise.AuthorID = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Entry(exercise).State = EntityState.Modified;
                await db.SaveChangesAsync();

                HttpPostedFileBase hpf = Request.Files["imagefile"] as HttpPostedFileBase;
                UploadImage up = new UploadImage();

                string exerciseName = Convert.ToString(exercise.ID);

                up.SaveImage(hpf, exerciseName);

                return RedirectToAction("Index");
            }
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = await db.Exercises.FindAsync(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Exercise exercise = await db.Exercises.FindAsync(id);
            db.Exercises.Remove(exercise);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
