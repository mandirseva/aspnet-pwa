namespace WebApplication1.Models
{
    public class SAMP
    {
        public int Id { get; set; }
        public string? UniqueId { get; set; }
        public string? Title { get; set; }
        public byte[]? Image { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string? Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedById { get; set; }
    }

}
