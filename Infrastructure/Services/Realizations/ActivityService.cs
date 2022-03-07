using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Realizations;

public class ActivityService : BaseTeacherComponentService<Activity>, IActivityService
{
    private readonly BaseRepository<Activity> _activityRepository;

    public ActivityService(BaseRepository<Activity> activityRepository, IBucket bucket) : base(activityRepository, bucket)
    {
        _activityRepository = activityRepository;
    }

    public async Task<IEnumerable<Activity>> GetActivitiesPagedList(int page, int pageSize)
    {
        var activitiesQuery = await _activityRepository.GetAll(q => q.Include(a => a.Files));
        return activitiesQuery.OrderByDescending(a => a.Date).Skip((page - 1) * pageSize).Take(pageSize);
    }

    public async Task<Activity> GetById(long id)
    {
        return await _activityRepository.GetById(id, q => q.Include(q => q.Files));
    }

    public async Task Add(Activity activity)
    {
        if (activity.Id != 0)
            await _activityRepository.Insert(activity);
        else
            await _activityRepository.Update(activity);
    }
}