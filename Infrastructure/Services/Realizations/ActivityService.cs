using Infrastructure.Models;
using Infrastructure.Models.Application;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Realizations;

public class ActivityService : BaseTeacherComponentService<Activity>, IActivityService
{
    private readonly BaseRepository<Activity> _activityRepository;
    private readonly ApplicationContext _context;

    public ActivityService(BaseRepository<Activity> activityRepository, IBucket bucket, ApplicationContext context) : base(activityRepository, bucket)
    {
        _activityRepository = activityRepository;
        _context = context;
    }

    public async Task<IEnumerable<Activity>> GetActivitiesPagedList(int page, int pageSize)
    {
        var activitiesQuery = await _activityRepository.GetAll(q => q.Include(a => a.Content));
        return activitiesQuery.OrderByDescending(a => a.Date).Skip((page - 1) * pageSize).Take(pageSize);
    }

    public async Task<Activity> GetById(long id)
    {
        return await _activityRepository.GetById(id, q => q.Include(q => q.Content));
    }

    public async Task Add(Activity activity)
    {
        activity.TeacherId = _context.CurrentUserId;
        if (activity.Id != 0)
            await _activityRepository.AddAsync(activity);
        else
            await _activityRepository.UpdateAsync(activity);
    }
}