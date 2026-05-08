using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using taskCompanySystem.Data;
using taskCompanySystem.Identity;
using taskCompanySystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Connection and DB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Identity Setup
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); // Fixes the MapRazorPages error

var app = builder.Build();

if (app.Environment.IsDevelopment()) { app.UseMigrationsEndPoint(); }
else { app.UseExceptionHandler("/Home/Error"); app.UseHsts(); }

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Trigger the Seed
using (var scope = app.Services.CreateScope())
{
    await SeedManagerAsync(scope.ServiceProvider);
}

app.MapStaticAssets();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

async Task SeedManagerAsync(IServiceProvider serviceProvider)
{
    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Keep: Department creation, but Change: Let SQL generate the ID[cite: 3]
    var adminDept = await context.Departments.FirstOrDefaultAsync(d => d.Name == "Administration");
    if (adminDept == null)
    {
        adminDept = new Department { Name = "Administration" };
        context.Departments.Add(adminDept);
        await context.SaveChangesAsync();
    }

    // Keep: Role creation[cite: 3]
    if (!await roleManager.RoleExistsAsync("Manager"))
        await roleManager.CreateAsync(new IdentityRole("Manager"));

    // Change: Add ALL required fields to the manager object[cite: 15]
    var email = "manager@company.com";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var manager = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = "System Manager",
            NationalID = "0000000000",
            BirthDate = new DateTime(1990, 1, 1), 
            Nationality = "Jordanian",             
            MaritalStatus = "Single",              
            PhotoPath = "default.png",             
            EntryDate = DateTime.Now,              
            EmailConfirmed = true,
            DepartmentId = adminDept.Id            
        };
        await userManager.CreateAsync(manager, "Manager@123");
        await userManager.AddToRoleAsync(manager, "Manager");
    }
}