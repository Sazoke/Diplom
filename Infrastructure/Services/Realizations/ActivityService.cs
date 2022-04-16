using Infrastructure.Dtos.Activity;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Realizations;

public class ActivityService : BaseComponentService<Activity>, IActivityService
{
    private readonly TagRepository _tagRepository;
    
    public ActivityService(BaseRepository<Activity> repository, TagRepository tagRepository) : base(repository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<Activity> GetByIdAsync(long id) =>
        await Repository.GetById(id, q => q.Include(a => a.Tags));

    public async Task RemoveAsync(long id)
    {
        await Repository.Delete(id);
    }

    public async Task AddOrUpdateAsync(ActivityEditDto activityDto)
    {
        
        var activity = activityDto.Id is null ? new Activity() : await GetByIdAsync(activityDto.Id.Value);
        await UpdateActivity(activity, activityDto);
        if (activityDto.Id is null)
            await Repository.AddAsync(activity);
        else
            await Repository.UpdateAsync(activity);
    }
    
    private async Task UpdateActivity(Activity activity, ActivityEditDto activityDto)
    {
        activity.Name = activityDto.Name;
        activity.Description = activityDto.Description;
        activity.Image = activityDto.Image;
        activity.Date = activityDto.DateTime;
        activity.AreaId = activityDto.AreaId;
        activity.Tags = (await _tagRepository.GetByIds(activityDto.Tags)).ToList();;
    }  
}