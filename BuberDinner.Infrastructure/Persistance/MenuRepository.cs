
using BuberDinner.Application.Common.Persistence;
using BuberDinner.Domain.Menus;

namespace BuberDinner.Infrastructure.Persistence.Repositories;

public sealed class MenuRepository : IMenuRepository
{
    //     private readonly BuberDinnerDbContext _dbContext;
    // BuberDinnerDbContext dbContext



    private static List<Menu> _menus = new();

    public MenuRepository()
    {
        // _dbContext = dbContext;
    }

    public void Add(Menu menu)
    {
        // _dbContext.Add(menu);
        // _dbContext.SaveChanges();

        _menus.Add(menu);
    }
}