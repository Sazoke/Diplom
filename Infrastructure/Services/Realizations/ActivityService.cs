using Infrastructure.Dtos.Base;
using Infrastructure.Models;
using Infrastructure.Models.Application;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Realizations;

public class ActivityService : BaseComponentService<Activity>, IActivityService
{
    private readonly BaseRepository<Activity?> _repository;

    public ActivityService(BaseRepository<Activity?> repository, TagRepository tagRepository, ApplicationContext applicationContext) : base(repository,
        applicationContext, tagRepository)
    {
        _repository = repository;
    }

    public async Task<Activity?> GetByIdAsync(long id) =>
        await _repository.GetById(id, q => q.Include(a => a.Tags));

    public async Task EditDate(long id, DateTime date)
    {
        var activity = await GetByIdAsync(id);
        activity.Date = date;
        await _repository.UpdateAsync(activity);
    }
}