using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Domain.Repository.Core
{
    public interface IPageRankRepo
    {
        Task<double?> GetPageRankAsync(string url);
    }
}
