using Infrastructure.Models;

namespace Infrastructure.Repositories;

public class MaterialRepository : BaseRepository<Material>
{
    public MaterialRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    
}