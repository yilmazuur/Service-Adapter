using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExternalServiceAdapter.Interfaces
{
    public interface IIntegrationDefinition
    {
        string ClientName { get; set; }
        int ID { get; set; }
        string IntegrationIdentifier { get; set; }
        string IntegrationParams { get; set; }
        string Password { get; set; }
        string UserName { get; set; }
    }
}
