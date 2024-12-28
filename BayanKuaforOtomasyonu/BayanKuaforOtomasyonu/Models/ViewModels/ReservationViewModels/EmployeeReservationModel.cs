namespace BayanKuaforOtomasyonu.Models.ViewModels.ReservationViewModels
{
    public class EmployeeReservationModel
    {
        public string EmploymentName { get; set; }
        public int EmploymentPrice { get; set; }
        public int EmploymentDuration { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan ReservationStart { get; set; }
        public TimeSpan ReservationEnd { get; set; }
        public int ReservationTotalPrice { get; set; }
        public string ReservationTotalCustomerName { get; set; }
    }
}
