using Microsoft.EntityFrameworkCore;
using SearchEngine.Domain.Entities;
using SearchEngine.Domain.Repository.Core;
using SearchEngine.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Infrastructure.Repositories
{
    public class PageRankRepo : IPageRankRepo
    {
        private readonly EngineContext _dbcontext;

        public PageRankRepo(EngineContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<double?> GetPageRankAsync(string url)
        {
            var entry = await _dbcontext.Set<UrlPageRank>().FirstOrDefaultAsync(r => r.Url == url);
            return entry?.Rank;
        }
    }
}
