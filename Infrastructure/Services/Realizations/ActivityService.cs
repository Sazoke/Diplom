using Infrastructure.Models;
using Infrastructure.Models.Application;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Realizations;

public class ActivityService : BaseComponentService<Activity>, IActivityService
{
    private readonly BaseRepository<Activity> _activityRepository;
    private readonly ApplicationContext _context;

    public ActivityService(BaseRepository<Activity> repository, ApplicationContext applicationContext, TagRepository tagRepository, ApplicationContext context, BaseRepository<Activity> activityRepository) : base(repository, applicationContext, tagRepository)
    {
        _context = context;
        _activityRepository = activityRepository;
    }

    public async Task<IEnumerable<Activity>> GetActivitiesPagedList(int page, int pageSize)
    {
        var activitiesQuery = await _activityRepository.GetAll();
        return activitiesQuery.OrderByDescending(a => a.Date).Skip((page - 1) * pageSize).Take(pageSize);
    }

    public async Task<Activity> GetById(long id)
    {
        return await _activityRepository.GetById(id);
    }

    public async Task Add(Activity activity)
    {
        activity.TeacherId = _context.CurrentUserId;
        await _activityRepository.AddAsync(activity);
    }
}