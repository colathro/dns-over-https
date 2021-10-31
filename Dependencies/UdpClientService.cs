public class UdpClientService
{
    private ConcurrentQueue<UdpClient> UdpClientQueue = new ConcurrentQueue<UdpClient>();
    private IPAddress DNSServerIP = GetDNSServer();

    public UdpClientService()
    {
        this.AddToClientQueue();
    }

    public UdpClient Dequeue()
    {
        UdpClient? client = null;
        bool found = false;
        do
        {
            if (this.UdpClientQueue.TryDequeue(out UdpClient? selectedClient))
            {
                client = selectedClient;
                found = true;
            }
            else
            {
                Thread.Sleep(1); // sleep for a milisecond
            }
        } while (found == false);

        return client!;
    }

    public void ReturnToQueue(UdpClient udpClient)
    {
        this.UdpClientQueue.Enqueue(udpClient);
    }

    private static IPAddress GetDNSServer()
    {
        var firstInterface = NetworkInterface.GetAllNetworkInterfaces()
            .Where(i => i.OperationalStatus == OperationalStatus.Up)
            .First();
        var firstIPProperties = firstInterface.GetIPProperties();
        return firstIPProperties.DnsAddresses.First();
    }

    private void AddToClientQueue()
    {
        for (int i = 0; i < 10; i++)
        {
            var client = new UdpClient(this.DNSServerIP.ToString(), 53);
            this.UdpClientQueue.Enqueue(client);
        }
    }
}