using Azure;
using BayanKuaforOtomasyonu.Models;
using BayanKuaforOtomasyonu.Models.Enums;
using BayanKuaforOtomasyonu.Models.ViewModels.AiQuery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Security.AccessControl;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BayanKuaforOtomasyonu.Controllers
{
    public class AskToAiController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string _apiUrl => "https://localhost:5001/api/KuaforApi/";

        public AskToAiController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        private SelectList GetEnumList(Type enumType)
        {
            if (!enumType.IsEnum)
                throw new ArgumentException("enumType must be an enum type");

            var list = Enum.GetValues(enumType)
                           .Cast<Enum>()
                           .Select(e => new
                           {
                               Value = e,
                               Text = e.GetType()
                                       .GetField(e.ToString())
                                       .GetCustomAttributes(typeof(DisplayAttribute), false)
                                       .Cast<DisplayAttribute>()
                                       .SingleOrDefault()
                                       ?.Name ?? e.ToString() // Display attribute yoksa, enum ismi kullanılır
                           }).ToList();

            return new SelectList(list, "Value", "Text");
        }
        public IActionResult Index()
        {
            // Enum'ları View'e gönderiyoruz
            ViewBag.FaceTypes = GetEnumList(typeof(FaceType));
            ViewBag.HairTypes = GetEnumList(typeof(HairType));
            ViewBag.HairLengths = GetEnumList(typeof(HairLength));
            ViewBag.ForeheadJaws = GetEnumList(typeof(ForeheadJaw));
            ViewBag.Lifestyles = GetEnumList(typeof(Lifestyle));

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                // Enum'ları tekrar gönderiyoruz, eğer form geçerli değilse
                ViewBag.FaceTypes = Enum.GetValues(typeof(FaceType));
                ViewBag.HairTypes = Enum.GetValues(typeof(HairType));
                ViewBag.HairLengths = Enum.GetValues(typeof(HairLength));
                ViewBag.ForeheadJaws = Enum.GetValues(typeof(ForeheadJaw));
                ViewBag.Lifestyles = Enum.GetValues(typeof(Lifestyle));

                return View(model);
            }


            // 1. Adım: KuaforApiController'daki AskToGpt metoduna GET isteği gönder
            string query = GetQuery(model);
            string appUserName = User.Identity.Name;
            var queryDataModel = new QueryDataModel
            {
                Query = query,
                AppUserName = appUserName
            };
            var client = _httpClientFactory.CreateClient();
            var askToGptResponse = await client.PostAsJsonAsync($"https://localhost:7143/api/KuaforApi/AskToGpt", queryDataModel);

            if (!askToGptResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)askToGptResponse.StatusCode, "GPT isteği sırasında bir hata oluştu.");
            }

            // 2. Adım: Cevabı al ve JSON olarak deseralize et
            var aiQueryModel = await askToGptResponse.Content.ReadFromJsonAsync<AddAiQueryViewModel>();

            // 3. Adım: KuaforApiController'daki CreateAiQuery metoduna POST isteği gönder
            var createResponse = await client.PostAsJsonAsync("https://localhost:7143/api/KuaforApi/CreateAiQuery", aiQueryModel);

            if (createResponse.IsSuccessStatusCode)
            {
                var resultModel = await createResponse.Content.ReadFromJsonAsync<CreatedAiQueryModel>();
                return RedirectToAction(nameof(AiQuery), "AskToAi", new { area = "", aiQueryId = resultModel.Id });
            }
            else
            {
                return StatusCode((int)createResponse.StatusCode, "Veri kaydetme sırasında bir hata oluştu.");
            }
        }
        public async Task<IActionResult> AiQuery(string aiQueryId)
        {
            int id = int.Parse(aiQueryId);
            var client = _httpClientFactory.CreateClient();
            var getAiQueryByIdResponse = await client.GetAsync($"https://localhost:7143/api/KuaforApi/GetAiQueryById/{id}");
            if (!getAiQueryByIdResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)getAiQueryByIdResponse.StatusCode, "Veritabanı isteği sırasında bir hata oluştu.");
            }

            // 2. Adım: Cevabı al ve JSON olarak deseralize et
            var aiQueryModel = await getAiQueryByIdResponse.Content.ReadFromJsonAsync<AiQueryViewModel>();
            aiQueryModel.Response = Regex.Replace(aiQueryModel.Response, @"\*\*(.*?)\*\*", "<strong>$1</strong>");
            aiQueryModel.Response = aiQueryModel.Response.Replace("\\n", "<br>");
            aiQueryModel.Response = aiQueryModel.Response.Replace("\n\n", "</p><p>").Replace("\n", "<br />");
            aiQueryModel.Response = $"<p>{aiQueryModel.Response}</p>";
            return View(aiQueryModel);
        }
        public async Task<IActionResult> AiQueries()
        {
            var client = _httpClientFactory.CreateClient();
            var getAiQueryByIdResponse = await client.GetAsync($"https://localhost:7143/api/KuaforApi/GetAllAiQueries/{User.Identity.Name}");
            if (!getAiQueryByIdResponse.IsSuccessStatusCode)
            {
                return StatusCode((int)getAiQueryByIdResponse.StatusCode, "Veritabanı isteği sırasında bir hata oluştu.");
            }

            // 2. Adım: Cevabı al ve JSON olarak deseralize et
            var aiQueryListModel = await getAiQueryByIdResponse.Content.ReadFromJsonAsync<List<AiQueryViewModel>>();
            if (aiQueryListModel != null && aiQueryListModel.Any())
            {
                foreach (var item in aiQueryListModel)
                {
                    item.Response = Regex.Replace(item.Response, @"\*\*(.*?)\*\*", "<strong>$1</strong>");
                    item.Response = item.Response.Replace("\\n", "<br>");
                    item.Response = item.Response.Replace("\n\n", "</p><p>").Replace("\n", "<br />");
                    item.Response = $"<p>{item.Response}</p>";
                }
            }
            return View(aiQueryListModel.OrderByDescending(m => m.CreationDate).ToList());
        }
        private string GetQuery(ProfileModel model)
        {
            var query = $"Face type : {model.Face} - " +
                $"Hair type : {model.Hair} - " +
                $"Hair length : {model.HairLength} - " +
                $"Forehead jaw structure : {model.ForeheadJaw} - " +
                $"life style : {model.Lifestyle} -> " +
                $"Can you suggest the three most suitable hairstyles based on the information given? Please answer in Turkish.";
            return query;
        }
    }
}
