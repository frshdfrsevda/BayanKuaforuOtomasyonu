namespace BayanKuaforOtomasyonu.Models.ViewModels.EmploymentViewModel
{
    public class EmploymentWithIncludesViewModel
    {
        public int EmploymentId { get; set; }
        public string EmploymentName { get; set; }
        public List<EmploymentInfoViewModel> EmploymentInfos { get; set; }
    }
}
