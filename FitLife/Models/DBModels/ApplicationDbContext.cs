using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FitLife.Models.DBModels
{    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Exercise> Exercises { get; set; }

        public System.Data.Entity.DbSet<FitLife.Models.DBModels.Plan> Plans { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.FollowingPlans).WithMany(i => i.Followers)
                .Map(x => x.MapLeftKey("UserId").MapRightKey("FollowingPlanId").ToTable("UserFollowingPlan"));

            modelBuilder.Entity<Plan>()
                .HasMany(c => c.Followers).WithMany(t => t.FollowingPlans);

            modelBuilder.Entity<Plan>()
                .HasRequired(c => c.Author).WithMany(t => t.Plans).WillCascadeOnDelete(false);

           // modelBuilder.Entity<Workout>()
            //    .HasRequired(c => c.Plan).WithMany(c => c.Workouts).WillCascadeOnDelete(true);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Plans).WithRequired(t => t.Author).WillCascadeOnDelete(false);
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.Plans).WithRequired(t => t.Author).HasForeignKey(f => f.AuthorID);
                
        }
    }
}