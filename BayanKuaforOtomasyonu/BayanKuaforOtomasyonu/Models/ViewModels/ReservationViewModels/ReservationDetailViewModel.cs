namespace BayanKuaforOtomasyonu.Models.ViewModels.ReservationViewModels
{
    public class ReservationDetailViewModel
    {
        public ReservationDetailViewModel(string employmentName, string userEmploymentName, int duration, int price)
        {
            EmploymentName = employmentName;
            UserEmploymentName = userEmploymentName;
            Duration = duration;
            Price = price;
        }

        public string EmploymentName { get; set; }
        public string UserEmploymentName { get; set; }
        public int Duration { get; set; }
        public int Price { get; set; }
    }
}
