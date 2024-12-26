namespace BayanKuaforOtomasyonu.Models.ViewModels.ReservationViewModels
{
    public class ReservationViewModel
    {
        public int ResId { get; set; }
        public int TotalDuration { get; set; }
        public int TotalPrice { get; set; }
        public DateTime ResDate { get; set; }
        public TimeSpan ResTime { get; set; }
        public string UserNameSurname { get; set; }
        public string UserPhoneNumber { get; set; }
        public string ResStatement { get; set; }
        public bool? ResStatus { get; set; }
        public List<ReservationDetailViewModel> ResDetails { get; set; }
    }
}
