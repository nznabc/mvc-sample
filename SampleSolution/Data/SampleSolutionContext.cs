using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Models;

namespace SampleSolution.Data
{
    public class SampleSolutionContext : DbContext
    {
        public SampleSolutionContext (DbContextOptions<SampleSolutionContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
    }
}
