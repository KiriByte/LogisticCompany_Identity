using LogisticCompany_Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LogisticCompany_Identity.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<FreightRequest> FreightRequests { get; set; }
    public DbSet<Waybill> Waybills { get; set; }

}