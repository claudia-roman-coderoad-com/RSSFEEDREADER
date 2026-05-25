using System.Collections.Concurrent;

namespace Services
{
    public class InMemorySubscriptionRepository
    {
        private readonly ConcurrentDictionary<System.Guid, Models.Subscription> _store = new();

        public IEnumerable<Models.Subscription> GetAll() => _store.Values.OrderBy(s => s.CreatedAt);

        public Models.Subscription Add(string url)
        {
            var sub = new Models.Subscription
            {
                Id = System.Guid.NewGuid(),
                Url = url,
                CreatedAt = System.DateTimeOffset.UtcNow
            };
            _store[sub.Id] = sub;
            return sub;
        }
    }
}
