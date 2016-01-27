using ExternalServiceAdapter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExternalServiceAdapter.Classes
{
    public class IntegrationDefinition : IIntegrationDefinition
    {
        public string ClientName { get; set; }
        public int ID { get; set; }
        public string IntegrationIdentifier { get; set; }
        public string IntegrationParams { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        public IntegrationDefinition() { }
    }
}
