using BuberDinner.Domain.Bills;
using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Dinners;
using BuberDinner.Domain.Guests;
using BuberDinner.Domain.Hosts;
using BuberDinner.Domain.MenuReview;
using BuberDinner.Domain.Menus;
using BuberDinner.Domain.Users;

using Microsoft.EntityFrameworkCore;

namespace BuberDinner.Infrastructure.Persistence;

public sealed class BuberDinnerDbContext : DbContext
{

    public BuberDinnerDbContext(
        DbContextOptions<BuberDinnerDbContext> options
    ) : base(options)
    {

    }

    public DbSet<Bill> Bills { get; set; } = null!;
    public DbSet<Dinner> Dinners { get; set; } = null!;
    public DbSet<Guest> Guests { get; set; } = null!;
    public DbSet<Host> Hosts { get; set; } = null!;
    public DbSet<Menu> Menus { get; set; } = null!;
    public DbSet<MenuReview> MenuReviews { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(BuberDinnerDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}