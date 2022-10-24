using Microsoft.EntityFrameworkCore;
using Revision_API.Models;

namespace Revision_API.Data
{
    public class Revision_APIContext : DbContext
    {
        public Revision_APIContext (DbContextOptions<Revision_APIContext> options)
            : base(options) { }

        public DbSet<Revision_API.Models.User> Users { get; set; } = default!;
        public DbSet<Revision_API.Models.Topic> Topics{ get; set; } = default!;
    }
}
