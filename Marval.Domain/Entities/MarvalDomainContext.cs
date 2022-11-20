using Marval.Domain.Abstracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marval.Domain.Entities
{
    public class MarvalDomainContext : DbContext, IDbContext
    {
        public MarvalDomainContext()
        {
        }

        public MarvalDomainContext(DbContextOptions<MarvalDomainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        public override int SaveChanges()
        {
            throw new InvalidOperationException("User ID must be provided");
        }

        public async Task<int> SaveChanges(string userId, string Ip)
        {
            try
            {
                //TODO
                //you track who is making the changes from here using the userid and the system ip address

                // Call the original SaveChanges()
                return await base.SaveChangesAsync();
            }
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}
