using Infrastructure.Models;

namespace Infrastructure.Repositories;

public class TagRepository : BaseRepository<Tag>
{
    public TagRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IEnumerable<Tag> GetBySchoolArea(long schoolAreaId) =>
        _context.Tags.Where(t => t.SchoolAreaId == schoolAreaId);
}