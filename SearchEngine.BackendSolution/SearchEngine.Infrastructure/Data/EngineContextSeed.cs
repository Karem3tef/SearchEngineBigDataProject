using Microsoft.EntityFrameworkCore;
using SearchEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SearchEngine.Infrastructure.Data
{
    public static class EngineContextSeed
    {
        public async static Task SeedAsync(EngineContext _dbcontext)
        {
            if (!_dbcontext.WordsToURLs.Any())
            {
                var lines = await File.ReadAllLinesAsync("../SearchEngine.Infrastructure/Data/DataSeed/output.txt");
                var entries = new List<WordsToUrl>();

                foreach (var line in lines)
                {
                    var parts = line.Split(' ', 2);
                    if (parts.Length != 2) continue;

                    entries.Add(new WordsToUrl
                    {
                        Word = parts[0].Trim(),
                        Urls = parts[1].Trim()
                    });
                }

                if (entries.Any())
                {
                    await _dbcontext.WordsToURLs.AddRangeAsync(entries);
                    await _dbcontext.SaveChangesAsync();
                }
            }
            //if (!_dbcontext.UrlPageRanks.Any())
            //{
            //    var pageRankData = await File.ReadAllTextAsync("../SearchEngine.Infrastructure/Data/DataSeed/pageRankResults.json");

            //    // Deserialize into a dictionary to ensure uniqueness
            //    var pageRankDict = JsonSerializer.Deserialize<Dictionary<string, string>>(pageRankData);

            //    if (pageRankDict?.Count > 0)
            //    {
            //        var pageRanks = new List<UrlPageRank>();

            //        foreach (var pair in pageRankDict)
            //        {
            //            if (!string.IsNullOrWhiteSpace(pair.Key) && double.TryParse(pair.Value, out var score))
            //            {
            //                pageRanks.Add(new UrlPageRank
            //                {
            //                    Url = pair.Key.Trim(),
            //                    Rank = score
            //                });
            //            }
            //        }

            //        await _dbcontext.UrlPageRanks.AddRangeAsync(pageRanks);
            //        await _dbcontext.SaveChangesAsync();
            //    }
            //}
        }
    }
}

