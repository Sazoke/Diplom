using Infrastructure.Dtos.Activity;
using Infrastructure.Dtos.Base;
using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface IActivityService : IComponentService<Activity>
{
    public Task AddOrUpdateAsync(ActivityDto activityDto);
}