namespace BayanKuaforOtomasyonu.Models.ViewModels.AiQuery
{
    public class AiQueryViewModel
    {
        public int Id { get; set; }
        public string Query { get; set; }
        public string Response { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
