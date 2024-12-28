namespace BayanKuaforOtomasyonu.Models.ViewModels.EmployeeShiftViewModels
{
    public class EmployeeShiftViewModel
    {
        public int Id { get; set; }
        public string? AppUserId { get; set; }
        public DayOfWeek ShiftDay { get; set; }
        public TimeSpan FirstTİme { get; set; }
        public TimeSpan LastTime { get; set; }
    }
}
