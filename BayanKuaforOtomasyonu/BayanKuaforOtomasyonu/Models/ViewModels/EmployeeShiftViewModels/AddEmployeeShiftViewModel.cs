namespace BayanKuaforOtomasyonu.Models.ViewModels.EmployeeShiftViewModels
{
    public class AddEmployeeShiftViewModel
    {
        public string AppUserId { get; set; }
        public DayOfWeek ShiftDay { get; set; }
        public TimeSpan FirstTİme { get; set; }
        public TimeSpan LastTime { get; set; }
    }
}
