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
using FitLife.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using AutoMapper;
using FitLife.Models.DTO;
using FitLife.Infrastructure;

namespace FitLife.Controllers.MvcControllers
{
    public class PlansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;
      
        public PlansController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }
        public PlansController()
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

        // GET: Plans
        public ActionResult Index()
        {
            ApplicationUser user =  UserManager.FindById(User.Identity.GetUserId());
            var myPlans =  user.FollowingPlans;
            var plans = db.Plans.Include(p => p.Author);
            return View(myPlans);
        }

        // GET: MyPlans
        public ActionResult MyPlans()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            var myPlans = user.Plans;
            var plans = Mapper.Map<List<PlanDTO>>(myPlans);
            return View(plans);
        }

        // GET: Plans/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = await db.Plans.FindAsync(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            return View(plan);
        }

        // GET: Plans/Create
        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(UserManager.Users, "Id", "FirstName");
            return View();
        }

        // POST: Plans/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      //  [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,AuthorID,Description,DifficultyLevel,Sex")] Plan plan, FormCollection form)
        {
            plan.AuthorID = User.Identity.GetUserId();    
            
            
            if (ModelState.IsValid)
            {
                db.Plans.Add(plan);
                await db.SaveChangesAsync();

                 HttpPostedFileBase hpf = Request.Files["imagefile"] as HttpPostedFileBase;
                 UploadImage up = new UploadImage();

                 string planName = "plan_" + Convert.ToString(plan.ID);
                 
                 up.SavePlanImage(hpf, planName);

                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(UserManager.Users, "Id", "FirstName", plan.AuthorID);
            return View(plan);
        }

        // GET: Plans/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = await db.Plans.FindAsync(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.AuthorID = new SelectList(UserManager.Users, "Id", "FirstName", plan.AuthorID);
            return View(plan);
        }

        // POST: Plans/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,AuthorID,Description")] Plan plan)
        {
            plan.AuthorID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(plan).State = EntityState.Modified;
                await db.SaveChangesAsync();

                HttpPostedFileBase hpf = Request.Files["imagefile"] as HttpPostedFileBase;
                UploadImage up = new UploadImage();

                string planName = "plan_" + Convert.ToString(plan.ID);

                up.SavePlanImage(hpf, planName);

                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(UserManager.Users, "Id", "FirstName", plan.AuthorID);
            return View(plan);
        }


        // GET: Plans/PlansWorkouts/5
        public async Task<ActionResult> PlansWorkouts(int? id)
        {
             Plan plan = await db.Plans.FindAsync(id);
            //plan.Author = UserManager.FindById(plan.AuthorID);

            if (plan == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlanID = id;
            ViewBag.PlanName = plan.Name;
            var workouts = db.Workouts.Where(c => c.PlanID == id);
            return View(workouts);
        }

        // GET: Plans/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = await db.Plans.FindAsync(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            return View(plan);
        }

        // POST: Plans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Plan plan = await db.Plans.FindAsync(id);
            db.Plans.Remove(plan);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult Uploader()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Uploader(FormCollection form)
        {
            HttpPostedFileBase hpf = Request.Files["imagefile"] as HttpPostedFileBase;
            UploadImage up = new UploadImage();
            up.SavePlanImage(hpf, "plan");

            return RedirectToAction("uploader");
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
