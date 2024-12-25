
using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using BayanKuaforOtomasyonu.Models.ViewModels.AppUserEmploymentViewModel;
using BayanKuaforOtomasyonu.Services.Abstracts;

namespace BayanKuaforOtomasyonu.Services.Managers
{
    public class UserEmploymentService : IUserEmploymentService
    {
        private readonly IUserEmploymentRepository _userEmploymentRepository;

        public UserEmploymentService(IUserEmploymentRepository userEmploymentRepository)
        {
            _userEmploymentRepository = userEmploymentRepository;
        }

        public void CreateOrUpdateUserEmployment(UserEmploymentViewModel model)
        {
            var userEmployment = _userEmploymentRepository.GetByIdWithProps(x=>x.AppUserId==model.AppUserId && x.EmploymentId==model.EmploymentId);
            if (userEmployment ==null)
            {
                _userEmploymentRepository.Add(new AppUserEmployement() { AppUserId=model.AppUserId,EmploymentId=model.EmploymentId,Duration=model.Duration,Price=model.Price});
                _userEmploymentRepository.Save();
            }
            else
            {
                userEmployment.Duration = model.Duration;
                userEmployment.Price = model.Price;
                _userEmploymentRepository.Update(userEmployment);
                _userEmploymentRepository.Save();
            }
        }

        public void DeleteUserEmployment(int id)
        {
            _userEmploymentRepository.Delete(_userEmploymentRepository.GetById(id));
            _userEmploymentRepository.Save();
        }

        public List<UserEmploymentsViewModel> GetAllUserEmployments(string userId)
        {
            var userEmployments = _userEmploymentRepository.GetAllByCondition(x=>x.AppUserId == userId, "AppUser,Employment");
            var listModel = new List<UserEmploymentsViewModel>();
            foreach (var item in userEmployments)
            {
                var model = new UserEmploymentsViewModel()
                {
                    Id = item.Id,
                    EmploymentName = item.Employment.Name,
                    Price = item.Price,
                    Duration = item.Duration,
                    UserName = item.AppUser.NameSurname
                };
                listModel.Add(model);
            }
            return listModel;
        }

        public UserEmploymentsViewModel GetUserEmploymentById(int id)
        {
            var value = _userEmploymentRepository.GetByIdWithProps(x=>x.Id== id, "Employment,AppUser");
            return new UserEmploymentsViewModel() {
                Id = value.Id,
                EmploymentName = value.Employment.Name,
                UserName = value.AppUser.NameSurname,
                Duration = value.Duration,
                Price = value.Price
            };
        }
    }
}
