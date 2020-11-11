using System.Collections.Generic;

namespace Shared.Helpers
{
    public class RabbitmqConfig
    {
        public string Host { get; set; }
        public ushort Port { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool PublisherConfirmation { get; set; }
        public IEnumerable<string> ClusterMembers { get; set; }
        public bool PurgeOnStartup { get; set; }
        public ushort PrefetchCount { get; set; }
        public string Endpoint { get; set; }
        public bool DurableQueue { get; set; }
    }
}