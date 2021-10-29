[ApiController]
public class DNSController : ControllerBase
{
    [HttpGet("")]
    public ActionResult Resolve(
        [FromQuery] DNSQuery dnsQuery
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(200);
    }
}