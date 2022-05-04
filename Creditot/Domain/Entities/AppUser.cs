namespace Creditot.Domain.Entities
{
    public class AppUser:BaseEntity
    {
        public long ChatId { get; set; }
        public string? Username { get; set; }
    }
}
