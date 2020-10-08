namespace ByteWebConnector.Model.Configuration
{
    public class ServiceDiscovery
    {
        public ServiceDiscoveryItem DocumentManagement { get; set; }
        public ServiceDiscoveryItem RainMaker { get; set; }
        public ServiceDiscoveryItem KeyStore { get; set; }
        public ServiceDiscoveryItem LosIntegration { get; set; }
        public ServiceDiscoveryItem ByteWebConnector { get; set; }
        public ServiceDiscoveryItem ElasticConfiguration { get; set; }
    }

    public class ServiceDiscoveryItem
    {
        public string Url { get; set; }
    }
}