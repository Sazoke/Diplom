using Infrastructure.Dtos.Activity;
using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface IActivityService : IComponentService<Activity>
{
    public Task AddOrUpdateAsync(ActivityEditDto activityDto);
}