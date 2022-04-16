using Infrastructure.Dtos.Activity;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Realizations;

public class ActivityService : BaseComponentService<Activity>, IActivityService
{
    public ActivityService(BaseRepository<Activity> repository) : base(repository)
    {
    }

    public async Task<Activity> GetByIdAsync(long id) =>
        await Repository.GetById(id, q => q.Include(a => a.Tags));

    public Task AddOrUpdateAsync(ActivityDto activityDto)
    {
        throw new NotImplementedException();
    }
}