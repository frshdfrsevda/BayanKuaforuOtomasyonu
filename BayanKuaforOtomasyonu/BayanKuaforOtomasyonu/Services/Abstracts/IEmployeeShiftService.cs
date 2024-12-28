using BayanKuaforOtomasyonu.Models.ViewModels.EmployeeShiftViewModels;

namespace BayanKuaforOtomasyonu.Services.Abstracts
{
    public interface IEmployeeShiftService
    {
        void CreateEmployeeShift(AddEmployeeShiftViewModel addModel);
        void UpdateEmployeeShift(EmployeeShiftViewModel updateModel);
        void DeleteEmployeeShift(int shiftId);
        List<EmployeeShiftViewModel> GetAllEmployeeShiftsById(string appUserId);
        EmployeeShiftViewModel GetEmployeeShiftById(int shiftId);
    }
}
