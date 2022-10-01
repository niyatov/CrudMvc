using CrudMvc.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrudMvc.Data;
public class AppDbContext : IdentityDbContext
{
    public DbSet<Player>? Players { get; set; }
    public DbSet<Team>? Teams { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

}