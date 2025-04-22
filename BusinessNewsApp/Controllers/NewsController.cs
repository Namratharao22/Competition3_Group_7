using Microsoft.AspNetCore.Mvc;
using BusinessNewsApp.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace BusinessNewsApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly HttpClient _httpClient;

        public NewsController()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "BusinessNewsApp");
        }

        public async Task<IActionResult> Index()
        {
            string apiKey = "6a1f970293b64b169387af85f7cb5ae0";
            string url = $"https://newsapi.org/v2/top-headlines?country=us&category=business&apiKey={apiKey}";

            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine("🔍 Raw JSON:\n" + json);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("❌ API Call Failed: " + response.StatusCode);
                return View(new List<NewsArticle>());
            }

            var apiData = JsonSerializer.Deserialize<NewsApiResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (apiData?.Articles == null || !apiData.Articles.Any())
            {
                Console.WriteLine("❌ No articles found after deserialization.");
                return View(new List<NewsArticle>());
            }

            Console.WriteLine($"✅ Articles found: {apiData.Articles.Count}");

            var newsArticles = apiData.Articles.Select(a => new NewsArticle
            {
                SourceName = a.Source?.Name ?? "Unknown",
                Title = a.Title ?? "No Title",
                Url = a.Url ?? "#"
            }).ToList();

            return View(newsArticles);
        }
    }
}
