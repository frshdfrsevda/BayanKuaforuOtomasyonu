using BayanKuaforOtomasyonu.Models.ViewModels.AiQuery;

namespace BayanKuaforOtomasyonu.Services.Abstracts
{
    public interface IAiQueryService
    {
        Task<string> CreateAiQueryAsync(AddAiQueryViewModel model);
        List<AiQueryViewModel> GetAllAiQueries(string appUserName);
        AiQueryViewModel GetAiQueryById(int id);
    }
}
