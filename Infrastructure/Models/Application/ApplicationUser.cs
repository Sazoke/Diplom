using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Models.Application
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;
    
        public string Description { get; set; } = string.Empty;

        public List<Material> Materials { get; set; } = new ();
    
        public List<Activity> Activities { get; set; } = new ();

        public List<EducationalMaterial> EducationalMaterials { get; set; } = new();

        public List<Test.Test> Tests { get; set; } = new ();
    }
}