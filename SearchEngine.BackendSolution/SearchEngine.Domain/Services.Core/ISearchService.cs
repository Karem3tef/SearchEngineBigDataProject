using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Domain.Services.Core
{
    public interface ISearchService
    {
        public Task<IReadOnlyList<String>> SearchAsync(string word);
    }
}
