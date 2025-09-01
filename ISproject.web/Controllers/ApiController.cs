using ISproject.Domain.Models;
using ISproject.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ISproject.web.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ApiController : Controller
    {
        private readonly IApiService apiService;
        private readonly INewsArticleService newsArticleService;
        private readonly INewsSourceService newsSourceService;

        public ApiController(IApiService apiService, INewsArticleService newsArticleService, INewsSourceService newsSourceService)
        {
            this.apiService = apiService;
            this.newsArticleService = newsArticleService;
            this.newsSourceService = newsSourceService;
        }

        // GET: Api/GetNewsArticles
        [HttpGet("GetNewsArticles")]
        public async Task<IActionResult> GetNewsArticles(string category, string country, string language, string sort)
        {
            var articles = await apiService.GetNewsArticles(country, category, language, null, sort, DateTime.Now, 100);

            if (articles == null) return BadRequest();

            var savedArticles = newsArticleService.AddAll(articles);

            return RedirectToAction("Index", "NewsArticles");
        }

        // GET: Api/GetNewsSources
        [HttpGet("GetNewsSources")]
        public async Task<IActionResult> GetNewsSources(string sourceKeyword)
        {
            var sources = await apiService.GetNewsSources(sourceKeyword, null, null, null,100, 0);

            if (sources == null) return BadRequest();

            var savedSources = newsSourceService.AddAll(sources);

            return RedirectToAction("Index", "NewsSources");
        }



        //// GET api/<ApiController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ApiController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ApiController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ApiController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
