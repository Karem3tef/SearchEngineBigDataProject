using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SearchEngine.Domain.Services.Core;

namespace SearchEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        [HttpGet]
        public async Task<ActionResult<List<String>>> GetLinksAsync(string word)
        {
            if (string.IsNullOrEmpty(word))
                BadRequest("String must be not empty");
            var URLs = await _searchService.SearchAsync(word);
            return Ok(URLs);
        }
    }
}
