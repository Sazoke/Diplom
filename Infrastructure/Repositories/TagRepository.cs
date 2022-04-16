using Infrastructure.Models;

namespace Infrastructure.Repositories;

public class TagRepository : BaseRepository<Tag>
{
    public TagRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Tag?>> GetBySchoolArea(long schoolAreaId) =>
        await GetAll(q => q.Where(t => t.SchoolAreaId == schoolAreaId));
}