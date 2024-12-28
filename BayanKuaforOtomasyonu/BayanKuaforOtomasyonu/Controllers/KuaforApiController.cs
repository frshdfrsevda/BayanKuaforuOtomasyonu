using BayanKuaforOtomasyonu.Models;
using BayanKuaforOtomasyonu.Models.ViewModels.AiQuery;
using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace BayanKuaforOtomasyonu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KuaforApiController : ControllerBase
    {
        private readonly IAiQueryService _aiQueryService;

        public KuaforApiController(IAiQueryService aiQueryService)
        {
            _aiQueryService = aiQueryService;
        }

        [HttpPost]
        [Route("AskToGpt")]
        public IActionResult AskToGpt([FromBody] QueryDataModel queryDataModel)
        {
            string apiKey = "";
            string model = "gpt-4o";
            ChatClient client = new ChatClient(model, apiKey);
            ChatCompletion completion = client.CompleteChat(queryDataModel.Query);
            var response = completion.Content[0].Text;
            var aiQueryModel = new AddAiQueryViewModel()
            {
                Query = queryDataModel.Query,
                Response = response,
                AppUserId = queryDataModel.AppUserName
            };
            // DataAnnotations ile doğrulama
            var validationContext = new ValidationContext(aiQueryModel);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(aiQueryModel, validationContext, validationResults, true))
            {
                // Hataları döndür
                return BadRequest(validationResults);
            }
            return Ok(aiQueryModel);
        }
        [Route("CreateAiQuery")]
        [HttpPost]
        public async Task<IActionResult> CreateAiQuery([FromBody] AddAiQueryViewModel model)
        {
            // Model doğrulaması
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var idString = await _aiQueryService.CreateAiQueryAsync(model);
            var idModel = new CreatedAiQueryModel() { Id = idString };

            // Başarılı yanıt döndür
            return Ok(idModel);
        }
        [HttpGet("GetAiQueryById/{id}")]
        [Route("GetAiQueryById")]
        public IActionResult GetAiQueryById(int id)
        {
            var model = _aiQueryService.GetAiQueryById(id);
            if (model == null)
                return BadRequest("Belirtilen yapay zeka sorgusu bulunamadı");
            return Ok(model);
        }
        [HttpGet("GetAllAiQueries/{appUserName}")]
        [Route("GetAllAiQueries")]
        public IActionResult GetAllAiQueries(string appUserName)
        {
            var model = _aiQueryService.GetAllAiQueries(appUserName);
            if (model == null || !model.Any())
                return BadRequest("Kullanıcıya ait yapay zeka sorgusu bulunamadı");
            return Ok(model);
        }
    }
}
