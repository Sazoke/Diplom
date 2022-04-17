using Infrastructure.Models;
using Infrastructure.Models.Application;

namespace Infrastructure.Repositories;

public class TagRepository : BaseRepository<Tag>
{
    public TagRepository(ApplicationDbContext context, ApplicationContext applicationContext) : base(context, applicationContext)
    {
    }

    public async Task<IEnumerable<Tag>> GetBySchoolArea(long schoolAreaId) =>
        await GetAll(q => q.Where(t => t.SchoolAreaId == schoolAreaId));
}