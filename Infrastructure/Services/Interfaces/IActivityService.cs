using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface IActivityService
{
    Task<IEnumerable<Activity>> GetActivitiesPagedList(int page, int pageSize);

    Task<Activity> GetById(long id);

    Task Add(Activity activity);
}