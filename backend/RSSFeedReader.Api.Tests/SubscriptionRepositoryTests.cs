using System.Linq;
using Services;
using Xunit;

public class SubscriptionRepositoryTests
{
    [Fact]
    public void AddAndGetSubscription()
    {
        var repo = new InMemorySubscriptionRepository();
        var created = repo.Add("https://example.com/feed");
        var all = repo.GetAll().ToList();
        Assert.Single(all);
        Assert.Equal(created.Id, all[0].Id);
        Assert.Equal("https://example.com/feed", all[0].Url);
    }
}
