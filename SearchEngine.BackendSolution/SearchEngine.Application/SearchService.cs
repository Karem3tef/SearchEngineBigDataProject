using SearchEngine.Domain.Entities;
using SearchEngine.Domain.Repository.Core;
using SearchEngine.Domain.Services.Core;
using SearchEngine.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine.Application
{
    public class SearchService : ISearchService
    {
        private readonly IWordToUrlRepo _wordToUrlRepo;
        private readonly IPageRankRepo _pageRankRepo;

        public SearchService(IWordToUrlRepo wordToUrlRepo, IPageRankRepo pageRankRepo)
        {
            _wordToUrlRepo = wordToUrlRepo;
            _pageRankRepo = pageRankRepo;
        }
        public async Task<IReadOnlyList<string>> SearchAsync(string word)
        {
            var URLs = await _wordToUrlRepo.GetUrlByWordAsync(word);
            if (string.IsNullOrWhiteSpace(URLs))
                return new List<string>();
            var URLsList = URLs
             .Split(',', StringSplitOptions.RemoveEmptyEntries)
             .Select(s =>
             {
               var lastColonIndex = s.LastIndexOf(':');
               return lastColonIndex > 0 ? s[..lastColonIndex].Trim() : s.Trim();
             })
             .Distinct()
             .ToList();
            var URLsWithRanks = new List<(string Url, double? Rank)>();
            foreach (var URL in URLsList)
            {
                var rank = await _pageRankRepo.GetPageRankAsync(URL);
                URLsWithRanks.Add((URL, rank));
            }
            return URLsWithRanks
            .OrderByDescending(r => r.Rank)
            .Select(r => r.Url)
            .ToList();
        }
    }
}
