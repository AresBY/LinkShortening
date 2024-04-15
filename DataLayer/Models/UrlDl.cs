namespace DataLayer.Models
{
    public class UrlDl
    {
        public int Id { get; set; }
        public string? LongUrl { get; set; }
        public string? ShortUrl { get; set; }
        public DateTime? Creation { get; set; }
        public int TransitionCount { get; set; }
    }
}
