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

        public void UpdateEmployement(UpdateEmploymentViewMedel updateEmploymentViewMedel)
        {
            var employment = _employmentRepository.GetById(updateEmploymentViewMedel.Id);
            employment.Name = updateEmploymentViewMedel.Name;
            _employmentRepository.Update(employment);
            _employmentRepository.Save();
        }
    }
}
