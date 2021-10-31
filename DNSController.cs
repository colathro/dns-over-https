[ApiController]
public class DNSController : ControllerBase
{
    [HttpGet("dns-query")]
    public async Task Get()
    {
        var dns = this.HttpContext.Request.Query["dns"];

        if (!ValidInputs(dns))
        {
            await WriteError(Constants.Errors.MissingDNSMessage);
            return;
        }

        return;
    }

    [HttpPost("dns-query")]
    public async Task Post()
    {
        StreamReader bodyStream = new StreamReader(this.HttpContext.Request.Body);
        string dns = await bodyStream.ReadToEndAsync();

        if (!ValidInputs(dns))
        {
            await WriteError(Constants.Errors.MissingDNSMessage);
            return;
        }
        return;
    }

    private bool ValidInputs(string dnsQuery)
    {
        if (string.IsNullOrWhiteSpace(dnsQuery))
        {
            return false;
        }
        return true;
    }

    private async Task WriteError(string error)
    {
        this.HttpContext.Response.StatusCode = 400;
        await this.HttpContext.Response.WriteAsync(error);
    }
}