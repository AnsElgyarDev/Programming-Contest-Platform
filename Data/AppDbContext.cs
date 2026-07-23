using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Programming_Contest_Platform.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Programming_Contest_Platform.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Problem>? Problems { get; set; }
    public DbSet<Contest>? Contests { get; set; }
    public DbSet<Submission>? Submissions { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var configuartions = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var connectionString = configuartions.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}