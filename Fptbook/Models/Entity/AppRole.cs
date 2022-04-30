using Microsoft.AspNetCore.Identity;

namespace Fptbook.Models.Entity
{
    public class AppRole :IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
