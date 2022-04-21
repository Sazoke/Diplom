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

    public Activity GetById(long id) =>
        Repository.GetById(id, q => q.Include(a => a.Tags));

    public async Task RemoveAsync(long id) => await Repository.RemoveAsync(id);

    public async Task AddOrUpdateAsync(ActivityEditDto activityDto)
    {
        var activity = activityDto.Id is null ? new Activity() : GetById(activityDto.Id.Value);
        UpdateActivity(activity, activityDto);
        if (activityDto.Id is null)
        {
            await Repository.AddAsync(activity);
            activity.Tags = _tagRepository.GetByIds(activityDto.Tags).ToList();
            await Repository.UpdateAsync(activity);
        }
        else
            await Repository.UpdateAsync(activity);
    }
    
    private static void UpdateActivity(Activity activity, ActivityEditDto activityDto)
    {
        activity.Name = activityDto.Name;
        activity.Description = activityDto.Description;
        activity.Image = activityDto.Image;
        activity.Date = activityDto.DateTime;
        activity.AreaId = activityDto.AreaId;
    }  
}