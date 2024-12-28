using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using BayanKuaforOtomasyonu.Models.ViewModels.AiQuery;
using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace BayanKuaforOtomasyonu.Services.Managers
{
    public class AiQueryService : IAiQueryService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAiQueryRepository _queryRepository;

        public AiQueryService(UserManager<AppUser> userManager, IAiQueryRepository queryRepository)
        {
            _userManager = userManager;
            _queryRepository = queryRepository;
        }

        public async Task<string> CreateAiQueryAsync(AddAiQueryViewModel model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.AppUserId);
            var aiQuery = new AiQuery()
            {
                Query = model.Query,
                Response = model.Response,
                AppUserId = appUser.Id
            };
            _queryRepository.Add(aiQuery);
            _queryRepository.Save();
            return aiQuery.Id.ToString();
        }


        public AiQueryViewModel GetAiQueryById(int id)
        {
            var aiQuery = _queryRepository.GetById(id);
            return new AiQueryViewModel()
            {
                Id = aiQuery.Id,
                Query = aiQuery.Query,
                Response = aiQuery.Response,
                CreationDate = aiQuery.CreationDate
            };
        }

        public List<AiQueryViewModel> GetAllAiQueries(string appUserName)
        {
            var aiQueries = _queryRepository.GetAllByCondition(aq=>aq.AppUser.Email == appUserName);
            var listModel = new List<AiQueryViewModel>();
            foreach (var aiQuery in aiQueries)
            {
                var model = new AiQueryViewModel()
                {
                    Id = aiQuery.Id,
                    Query = aiQuery.Query,
                    Response = aiQuery.Response,
                    CreationDate = aiQuery.CreationDate,
                };
                listModel.Add(model);
            }
            return listModel;
        }
    }
}
