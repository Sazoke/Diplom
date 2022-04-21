using Infrastructure.Models.Application;
using Infrastructure.Models.Test;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TestRepository : BaseRepository<Test>
{
    public TestRepository(DbContext context, ApplicationContext applicationContext) : base(context, applicationContext)
    {
    }

    public IEnumerable<Test> GetByTeacherId(string teacherId, int page, int pageSize) =>
        GetAll().Where(t => t.CreatedById == teacherId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
}