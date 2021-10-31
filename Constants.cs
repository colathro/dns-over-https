public static class Constants
{
    public static readonly string AcceptedContentType = "application/dns-message";
    public static readonly StringComparison IgnoreCase = StringComparison.InvariantCultureIgnoreCase;
    public static readonly string GET = HttpMethod.Get.ToString();
    public static readonly string POST = HttpMethod.Post.ToString();

    public static class Errors
    {
        public static readonly string BadContentType = "bad content-type";
        public static readonly string BadHTTPMethod = "bad http method";
        public static readonly string MissingAcceptHeader = "missing accept header application/dns-messgae";
        public static readonly string MissingDNSMessage = "missing dns message";
    }
}