using System.Net.Http.Json;
using DotNetEnv;
using ISproject.Domain.Models;
using ISproject.Domain.Models.Responses;
using ISproject.Service.Interface;
using Microsoft.AspNetCore.WebUtilities;


namespace ISproject.Service.Implementation
{
    public class ApiService : IApiService
    {
        private string? ApiKey;
        private readonly ICountryService countryService;
        private readonly ICategoryService categoryService;
        private readonly INewsSourceService newsSourceService;
        private readonly ILanguageService languageService;
        private readonly HttpClient client;

        public ApiService(
            ICountryService countryService,
            ICategoryService categoryService,
            INewsSourceService newsSourceService,
            ILanguageService languageService)
        {
            this.countryService = countryService;
            this.categoryService = categoryService;
            this.newsSourceService = newsSourceService;
            this.languageService = languageService;

            //Console.WriteLine($"Current dir: {Directory.GetCurrentDirectory()}");
            var envPath = Path.Combine(Directory.GetCurrentDirectory(), "..", ".env");
            Env.Load(envPath);
            Console.WriteLine($"Env dir: {envPath}");
            ApiKey = Environment.GetEnvironmentVariable("API_KEY") ?? throw new Exception("API_KEY not found");

            this.client = new HttpClient();
        }

        public async Task<IEnumerable<NewsArticle>?> GetNewsArticles(string? countries, string? categories, string? languages, string? sources, string? sort, DateTime? dateTime, int? limit = 25, int? offset = 0)
        {


            var baseUrl = "http://api.mediastack.com/v1/news";

            var queryParams = new Dictionary<string, string?>()
            {
                ["access_key"] = ApiKey,
                ["sources"] = sources,
                ["categories"] = categories != null && categories.Length != 0 ? categories : null,
                ["countries"] = countries != null && countries.Length != 0 ? countries : null,
                ["languages"] = languages != null && languages.Length != 0 ? languages : null,
                ["limit"] = limit != null ? limit.ToString() : "25",
                ["sort"] = sort != null ? sort : "published_desc",
                ["offset"] = offset != null ? offset.ToString() : "0",
            };

            var url = QueryHelpers.AddQueryString(baseUrl, queryParams);

            var response = await client.GetAsync(url);


            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();

                Console.WriteLine($"Error occured while trying to fetch News Article data: {error!.Error!.Code} : {error!.Error!.Message}");
                return null;
            }
            else
            {
                var respCont = await response.Content.ReadFromJsonAsync<NewsArticleResponse>();

                if (respCont?.Data == null)
                    return [];

                List<NewsArticle> newsArticles = [];

                foreach (var dto in respCont.Data)
                {
                    if (dto.Source == null || dto.Published_at == null
                        || dto.Title == null || dto.Author == null
                        || dto.Category == null || dto.Country == null
                        || dto.Description == null || dto.Language == null)
                    {
                        continue;
                    }
                    else
                    {
                        var article = new NewsArticle
                        {
                            Author = dto.Author,
                            Title = dto.Title,
                            Description = dto.Description,
                            Url = dto.Url,
                            Language = dto.Language != null ? languageService.GetByCode(dto.Language) : null,
                            Source = dto.Source,
                            Country = dto.Country != null ? countryService.GetByCode(dto.Country) : null,
                            Category = dto.Category != null ? categoryService.GetByCode(dto.Category) : null,
                            PublishedAt = dto.Published_at,
                            ImageUrl = dto.ImageUrl
                        };

                        if (article.Language == null) Console.WriteLine($"Language - {dto.Language} -|- Length - {dto.Language.Length}");
                        if (article.Country == null) Console.WriteLine($"Country - {dto.Country} -|- Length - {dto.Country.Length}");
                        if (article.Category == null) Console.WriteLine($"Category - {dto.Category} -|- Length - {dto.Category.Length}");

                        //if (article.Language == null || article.Country == null || article.Category == null) continue;

                        newsArticles.Add(article);


                    }


                }
                return newsArticles;
            }
        }

        public async Task<IEnumerable<NewsSource>?> GetNewsSources(string searchKeyword, string? countries, string? categories, string? languages, int? limit = 25, int? offset = 0)
        {

            var baseUrl = "http://api.mediastack.com/v1/sources";

            var queryParams = new Dictionary<string, string?>()
            {
                ["access_key"] = ApiKey,
                ["search"] = searchKeyword,
                ["countries"] = countries,
                ["categories"] = categories,
                ["languages"] = languages,
                ["limit"] = limit != null ? limit.ToString() : "25",
                ["offset"] = offset != null ? offset.ToString() : "0",
            };

            var url = QueryHelpers.AddQueryString(baseUrl, queryParams);

            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                Console.WriteLine($"Error occured while trying to fetch News Article data: {error!.Error!.Code} : {error!.Error!.Message}");
                return null;
            }

            else
            {
                var srcResponse = await response.Content.ReadFromJsonAsync<NewsSourceResponse>();

                Console.WriteLine(srcResponse);

                List<NewsSource> newsSources = srcResponse!.data!
                    .Select(dto =>
                        new NewsSource
                        {
                            Code = dto.code,
                            Name = dto.name,
                            Url = dto.url,
                            Category = dto.category != null ? categoryService.GetByCode(dto.category!) : null,
                            Country = dto.country != null ? countryService.GetByCode(dto.country!) : null,
                            Language = dto.language != null ? languageService.GetByCode(dto.language!) : null,
                        }
                    ).ToList();
                return newsSources;
            }

        }



        //private async Task<NewsSource?> GetAndSaveSource(string keyword)
        //{
        //    var candidateSources = await GetNewsSources(keyword, null, null, null, 100, 0);

        //    if (candidateSources == null) return null;
        //    var source = candidateSources.ToList().Find(cs => cs.Code != null && cs.Code.Equals(keyword));

        //    return source != null ? newsSourceService.Add(source) : null;

        //}
    }
}
