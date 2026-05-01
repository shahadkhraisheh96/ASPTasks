using companySystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace companySystem.data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<employee> Employees { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // 1. Employee -> ApplicationUser (One-to-One)
            modelBuilder.Entity<employee>()
                .HasOne(e => e.User)
                .WithOne() 
                .HasForeignKey<employee>("UserId") 
                .OnDelete(DeleteBehavior.NoAction); 
            
            // 2. Employee -> Department (Many-to-One)
            modelBuilder.Entity<employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            // 3. TaskItem -> Employee (Many-to-One)
            modelBuilder.Entity<TaskItem>()
                .HasOne(t => t.AssignedEmployee)
                .WithMany(e => e.Tasks)
                .HasForeignKey(t => t.AssignedToEmployeeId)
                .OnDelete(DeleteBehavior.Cascade); 

            // 4. NationalId Unique Index
            modelBuilder.Entity<employee>()
                .HasIndex(e => e.NationalID)
                .IsUnique();

        }


    }
}
