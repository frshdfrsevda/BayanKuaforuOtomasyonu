using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using BayanKuaforOtomasyonu.Models.ViewModels.EmploymentViewModel;
using BayanKuaforOtomasyonu.Services.Abstracts;

namespace BayanKuaforOtomasyonu.Services.Managers
{
    public class EmploymentService : IEmploymentService
    {
        private readonly IEmploymentRepository _employmentRepository;

        public EmploymentService(IEmploymentRepository employmentRepository)
        {
            _employmentRepository = employmentRepository;
        }

        public void CreateEmployement(AddEmploymentViewModel addEmploymentViewModel)
        {
            _employmentRepository.Add(new Employment() { Name = addEmploymentViewModel.Name});
            _employmentRepository.Save();
        }

        public void DeleteEmployement(int id)
        {
            _employmentRepository.Delete(_employmentRepository.GetById(id));
            _employmentRepository.Save();
        }

        public List<UpdateEmploymentViewMedel> GetAllEmployments()
        {
            var employments = _employmentRepository.GetAll();
            var listModel = new List<UpdateEmploymentViewMedel>();
            foreach(var employment in employments)
            {
                var model = new UpdateEmploymentViewMedel()
                {
                    Id = employment.Id,
                    Name = employment.Name
                };
                listModel.Add(model);
            }
            return listModel;
        }

        public List<EmploymentWithIncludesViewModel> GetAllEmploymentsWithIncludes()
        {
            var employmentsWithIncludes = _employmentRepository.GetAll("AppUserEmployements,AppUserEmployements.AppUser");
            var listModel = new List<EmploymentWithIncludesViewModel>();
            foreach(var item in employmentsWithIncludes)
            {
                var employmentInfos = new List<EmploymentInfoViewModel>();
                foreach(var info in item.AppUserEmployements)
                {
                    var infoModel = new EmploymentInfoViewModel()
                    {
                        Duration = info.Duration,
                        Price = info.Price,
                        UserEmploymentId = info.Id,
                        UserName = info.AppUser.NameSurname
                    };
                    employmentInfos.Add(infoModel);
                }
                var model = new EmploymentWithIncludesViewModel()
                {
                    EmploymentId = item.Id,
                    EmploymentName = item.Name,
                    EmploymentInfos = employmentInfos
                };
                listModel.Add(model);
            }
            return listModel;
        }

        public void UpdateEmployement(UpdateEmploymentViewMedel updateEmploymentViewMedel)
        {
            var employment = _employmentRepository.GetById(updateEmploymentViewMedel.Id);
            employment.Name = updateEmploymentViewMedel.Name;
            _employmentRepository.Update(employment);
            _employmentRepository.Save();
        }
    }
}
