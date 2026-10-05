using AssignmentStorer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AssignmentStorer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Assignment> Assignments { get; set; }
    }
}