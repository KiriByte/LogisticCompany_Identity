using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LogisticCompany_Identity.Data;
using LogisticCompany_Identity.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DBString") ??
                       throw new InvalidOperationException("Connection string 'DBString' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
//.AddDefaultUI();

builder.Services.AddRazorPages();


builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    List<string> roles = new List<string>
    {
        "admin", "employee", "user"
    };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            var addrole = new IdentityRole(role);
            await roleManager.CreateAsync(addrole);
        }
    }

    // Добавление админа
    if (await userManager.FindByEmailAsync("admin@test.com") == null)
    {
        var adminUser = new AppUser
        {
            UserName = "admin@test.com",
            Email = "admin@test.com",
            FirstName = "Admin",
            LastName = "Admin",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(adminUser, "Test1!");

        var isInRole = await userManager.IsInRoleAsync(adminUser, "admin");
        if (!isInRole)
        {
            await userManager.AddToRoleAsync(adminUser, "admin");
        }
    }
    
    if (await userManager.FindByEmailAsync("employee@test.com") == null)
    {
        var employeeUser = new AppUser
        {
            UserName = "employee@test.com",
            Email = "employee@test.com",
            FirstName = "Employee",
            LastName = "Employee",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(employeeUser, "Test1!");

        var isInRole = await userManager.IsInRoleAsync(employeeUser, "employee");
        if (!isInRole)
        {
            await userManager.AddToRoleAsync(employeeUser, "employee");
        }
    }
    
    if (await userManager.FindByEmailAsync("user@test.com") == null)
    {
        var userUser = new AppUser
        {
            UserName = "user@test.com",
            Email = "user@test.com",
            FirstName = "User",
            LastName = "User",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(userUser, "Test1!");

        var isInRole = await userManager.IsInRoleAsync(userUser, "user");
        if (!isInRole)
        {
            await userManager.AddToRoleAsync(userUser, "user");
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();