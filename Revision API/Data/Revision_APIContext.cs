using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Revision_API.Models;

namespace Revision_API.Data
{
    public class Revision_APIContext : DbContext
    {
        public Revision_APIContext (DbContextOptions<Revision_APIContext> options)
            : base(options)
        {
        }

        public DbSet<Revision_API.Models.User> User { get; set; } = default!;
    }
}
