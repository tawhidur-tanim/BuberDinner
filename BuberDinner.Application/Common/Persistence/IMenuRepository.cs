using BuberDinner.Domain.Menus;

namespace BuberDinner.Application.Common.Persistence;

public interface IMenuRepository
{
    void Add(Menu menu);
}