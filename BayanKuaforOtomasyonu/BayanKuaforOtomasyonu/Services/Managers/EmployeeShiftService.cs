using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using BayanKuaforOtomasyonu.Models.ViewModels.EmployeeShiftViewModels;
using BayanKuaforOtomasyonu.Services.Abstracts;

namespace BayanKuaforOtomasyonu.Services.Managers
{
    public class EmployeeShiftService : IEmployeeShiftService
    {
        private readonly IEmployeeShiftRepository _employeeShiftRepository;

        public EmployeeShiftService(IEmployeeShiftRepository employeeShiftRepository)
        {
            _employeeShiftRepository = employeeShiftRepository;
        }

        public void CreateEmployeeShift(AddEmployeeShiftViewModel addModel)
        {
            var table = new EmployeeShift()
            {
                ShiftDay = addModel.ShiftDay,
                FirstTİme = addModel.FirstTİme,
                LastTime = addModel.LastTime,
                AppUserId = addModel.AppUserId
            };
            _employeeShiftRepository.Add(table);
            _employeeShiftRepository.Save();
        }

        public void DeleteEmployeeShift(int shiftId)
        {
            var table = _employeeShiftRepository.GetById(shiftId);
            _employeeShiftRepository.Delete(table);
            _employeeShiftRepository.Save();
        }

        public List<EmployeeShiftViewModel> GetAllEmployeeShiftsById(string appUserId)
        {
            var listModel = new List<EmployeeShiftViewModel>();
            var tables = _employeeShiftRepository.GetAllByCondition(es => es.AppUserId == appUserId);
            foreach (var table in tables)
            {
                var model = new EmployeeShiftViewModel()
                {
                    Id = table.Id,
                    ShiftDay = table.ShiftDay,
                    FirstTİme = table.FirstTİme,
                    LastTime = table.LastTime
                };
                listModel.Add(model);
            }
            return listModel;
        }

        public EmployeeShiftViewModel GetEmployeeShiftById(int shiftId)
        {
            var table = _employeeShiftRepository.GetById(shiftId);
            return new EmployeeShiftViewModel() {
                Id = table.Id,
                ShiftDay = table.ShiftDay,
                FirstTİme = table.FirstTİme,
                LastTime = table.LastTime,
                AppUserId = table.AppUserId
            };
        }

        public void UpdateEmployeeShift(EmployeeShiftViewModel updateModel)
        {
            var table = _employeeShiftRepository.GetById(updateModel.Id);

            table.FirstTİme = updateModel.FirstTİme;
            table.LastTime = updateModel.LastTime;
            
            _employeeShiftRepository.Update(table);
            _employeeShiftRepository.Save();
        }
    }
}
