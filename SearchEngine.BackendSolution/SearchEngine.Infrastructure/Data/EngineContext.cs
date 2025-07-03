using Microsoft.EntityFrameworkCore;
using SearchEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Infrastructure.Data
{
    public class EngineContext : DbContext
    {
        public EngineContext(DbContextOptions<EngineContext> options)
            : base(options) { }
        public DbSet<WordsToUrl> WordsToURLs { get; set; }
        public DbSet<UrlPageRank> UrlPageRanks { get; set; }
    }
}
