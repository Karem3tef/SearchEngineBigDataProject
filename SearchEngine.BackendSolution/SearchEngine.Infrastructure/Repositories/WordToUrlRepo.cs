using Microsoft.EntityFrameworkCore;
using SearchEngine.Domain.Entities;
using SearchEngine.Domain.Repository.Core;
using SearchEngine.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Infrastructure.Repositories
{
    public class WordToUrlRepo : IWordToUrlRepo
    {
        private readonly EngineContext _dbcontext;

        public WordToUrlRepo(EngineContext dbcontext) 
        {
            _dbcontext = dbcontext;
        }
        public async Task<string?> GetUrlByWordAsync(string word)
        {
            var result= await  _dbcontext.Set<WordsToUrl>().FirstOrDefaultAsync(x => x.Word == word);
            return result?.Urls;

        }
    }
}
