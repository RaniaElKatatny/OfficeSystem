using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;


namespace MVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }



        public System.Data.Entity.DbSet<MVC.Models.mvcEmployeeModel> mvcEmployeeModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Employees");

        }

        public System.Data.Entity.DbSet<MVC.Models.mvcDepartmentModel> mvcDepartmentModels { get; set; }
    }
}