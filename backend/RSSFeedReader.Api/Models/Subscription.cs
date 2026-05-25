namespace Models
{
    public class Subscription
    {
        public System.Guid Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public System.DateTimeOffset CreatedAt { get; set; }
    }
}
