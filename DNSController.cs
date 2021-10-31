[ApiController]
public class DNSController : ControllerBase
{
    private readonly UdpClientService udpClientService;
    public DNSController(UdpClientService _udpClientService)
    {
        udpClientService = _udpClientService;
    }

    [HttpGet("dns-query")]
    public async Task Get()
    {
        var udpClient = udpClientService.Dequeue();
        try
        {
            var dns = this.HttpContext.Request.Query["dns"];
            var urlEncodedRequest = dns.First();
            var message = WebEncoders.Base64UrlDecode(urlEncodedRequest);

            await udpClient.Client.SendAsync(message, SocketFlags.None);

            var res = await udpClient.ReceiveAsync();
            this.HttpContext.Response.Headers.ContentType = Constants.AcceptedContentType;
            await this.HttpContext.Response.Body.WriteAsync(res.Buffer);
            return;
        }
        finally
        {
            udpClientService.ReturnToQueue(udpClient);
        }
    }

    [HttpPost("dns-query")]
    public async Task Post()
    {
        var udpClient = udpClientService.Dequeue();

        try
        {
            using (var reqMs = new MemoryStream(1048))
            {
                await this.HttpContext.Request.Body.CopyToAsync(reqMs);
                await udpClient.Client.SendAsync(reqMs.GetBuffer(), SocketFlags.None);
            }

            var res = await udpClient.ReceiveAsync();
            this.HttpContext.Response.Headers.ContentType = Constants.AcceptedContentType;
            await this.HttpContext.Response.Body.WriteAsync(res.Buffer);

            return;
        }
        finally
        {
            udpClientService.ReturnToQueue(udpClient);
        }
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