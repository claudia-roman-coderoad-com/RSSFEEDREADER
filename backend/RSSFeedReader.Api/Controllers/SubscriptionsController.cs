using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly Services.InMemorySubscriptionRepository _repo;

    public SubscriptionsController(Services.InMemorySubscriptionRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Models.Subscription>> Get()
    {
        return Ok(_repo.GetAll());
    }

    public class CreateRequest { public string Url { get; set; } = string.Empty; }

    [HttpPost]
    public ActionResult<Models.Subscription> Post([FromBody] CreateRequest req)
    {
        if (string.IsNullOrWhiteSpace(req?.Url)) return BadRequest();
        var created = _repo.Add(req.Url);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }
}
