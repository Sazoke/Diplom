using Infrastructure.Dtos.Base;
using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface IActivityService : IComponentService<Activity>
{
    public Task EditDate(long id, DateTime date);
}