using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using taskCompanySystem.Identity;
using taskCompanySystem.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Department> Departments { get; set; }
    public DbSet<EmployeeTask> Tasks { get; set; }
    public DbSet<ContactFeedback> ContactFeedbacks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>().ToTable("Employees");
        builder.Entity<Department>().ToTable("Departments");
        builder.Entity<EmployeeTask>().ToTable("EmployeeTasks");
        builder.Entity<ContactFeedback>().ToTable("CustomerFeedback");

        builder.Entity<ApplicationUser>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<EmployeeTask>()
            .HasOne(t => t.Employee)
            .WithMany(e => e.Tasks)
            .HasForeignKey(t => t.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}